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
        parameters.Add("@TrackId", entity.TrackId);
        parameters.Add("@Id", null, DbType.Int32, direction: ParameterDirection.Output);

        await Transaction.ExecuteAsync(InsertQuery, parameters).ConfigureAwait(false);

        return parameters.Get<int>("@Id");
    }

    public override async Task<long> InsertRangeAsync(IEnumerable<Race> entities)
    {
        var table = entities.PopulateTable();
        
        using var bulkInsert = new SqlBulkCopy(Transaction.Connection, SqlBulkCopyOptions.Default, Transaction);
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Race.TrackId), "track_id"));
        bulkInsert.DestinationTableName = table.TableName;
        
        await bulkInsert.WriteToServerAsync(table).ConfigureAwait(false);
        
        return bulkInsert.RowsCopied;
    }

    public override async Task<int> UpdateAsync(Race entity)
    {
        var parameters = new { 
            entity.Id, 
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
                                              track_id
                                       FROM [dbo].[race] WITH (NOLOCK)
                                       """;
    
    private const string FindQuery = """
                                     SELECT id, 
                                            track_id AS TrackId
                                     FROM [dbo].[race] WITH (NOLOCK)
                                     WHERE id=@Id
                                     """;
    
    private const string InsertQuery = """
                                       INSERT INTO [dbo].[race] (track_id)
                                          VALUES (@TrackId);
                                          SELECT @Id = SCOPE_IDENTITY()
                                       """;
    
    private const string UpdateQuery = """
                                       UPDATE [dbo].[race]
                                       SET track_id=@TrackId
                                       WHERE id=@Id
                                       """;
    
    private const string DeleteQuery = "DELETE FROM [dbo].[race] WHERE id=@id";
}