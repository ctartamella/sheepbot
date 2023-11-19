using System.Data;
using Dapper;
using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Helpers;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class RaceRepository : RepositoryBase<Race>, IRaceRepository
{
    public RaceRepository(SqlTransaction transaction) : base(transaction) { }

    public override async Task<IEnumerable<Race>> GetAllAsync()
    {
        var results = await Transaction
            .QueryAsync<Race>(GetAllQuery)
            .ConfigureAwait(false);

        return results ?? new List<Race>();
    }

    public override async Task<Race?> FindAsync(long id)
    {
        var parameters = new { id };

        var result = await Transaction
            .QueryAsync<Race>(FindQuery, parameters)
            .ConfigureAwait(false);
        
        return result.SingleOrDefault();
    }

    public override async Task<long> InsertAsync(Race entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SeriesId", entity.SeriesId);
        parameters.Add("@TrackId", entity.TrackId);
        parameters.Add("@PracticeTime", entity.PracticeTime);
        parameters.Add("@QualiTime", entity.QualiTime);
        parameters.Add("@Length", entity.Length);
        parameters.Add("@LengthTypeId", (int) (entity.Unit ?? 0));
        parameters.Add("@Id", null, DbType.Int32, direction: ParameterDirection.Output);

        await Transaction.ExecuteAsync(InsertQuery, parameters).ConfigureAwait(false);

        return parameters.Get<int>("@Id");
    }

    public override async Task<long> InsertRangeAsync(IEnumerable<Race> entities)
    {
        var table = entities.PopulateTable();
        
        using var bulkInsert = new SqlBulkCopy(Transaction.Connection, SqlBulkCopyOptions.Default, Transaction);
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Race.SeriesId), "series_id"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Race.TrackId), "track_id"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Race.PracticeTime), "practice_time"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Race.QualiTime), "quali_time"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Race.Length), "length"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Race.Unit), "length_unit_id"));
        bulkInsert.DestinationTableName = table.TableName;
        
        await bulkInsert.WriteToServerAsync(table).ConfigureAwait(false);
        
        return bulkInsert.RowsCopied;
    }

    public override async Task<int> UpdateAsync(Race entity)
    {
        var parameters = new { 
            entity.Id, 
            entity.Length, 
            entity.Unit, 
            entity.PracticeTime, 
            entity.QualiTime, 
            entity.SeriesId, 
            entity.TrackId 
        };
        
        return await Transaction.ExecuteAsync(UpdateQuery, parameters).ConfigureAwait(false);
    }

    public override async Task<int> DeleteAsync(long id)
    {
        var parameters = new { id };
        return await Transaction.ExecuteAsync(DeleteQuery, parameters).ConfigureAwait(false);
    }
    
    private const string GetAllQuery = """
                                       SELECT id, 
                                              series_id, 
                                              track_id, 
                                              practice_time AS PracticeTime, 
                                              quali_time AS QualiTime, 
                                              length, 
                                              length_unit_id AS Unit
                                       FROM [dbo].[race] WITH (NOLOCK)
                                       """;
    
    private const string FindQuery = """
                                     SELECT id, 
                                            series_id AS SeriesId, 
                                            track_id AS TrackId, 
                                            practice_time AS PracticeTime, 
                                            quali_time AS QualiTime, 
                                            length, 
                                            length_unit_id AS Unit
                                     FROM [dbo].[race] WITH (NOLOCK)
                                     WHERE id=@Id
                                     """;
    
    private const string InsertQuery = """
                                       INSERT INTO [dbo].[race] (series_id, track_id, practice_time, quali_time, length, length_unit_id)
                                          VALUES (@SeriesId, @TrackId, @PracticeTime, @QualiTime, @Length, @LengthTypeId);
                                          SELECT @Id = SCOPE_IDENTITY()
                                       """;
    
    private const string UpdateQuery = """
                                       UPDATE [dbo].[race]
                                       SET length=@Length, 
                                           length_unit_id=@Unit, 
                                           practice_time=@PracticeTime,
                                           quali_time=@QualiTime,
                                           series_id=@SeriesId,
                                           track_id=@TrackId
                                       WHERE id=@Id
                                       """;
    
    private const string DeleteQuery = "DELETE FROM [dbo].[race] WHERE id=@id";
}