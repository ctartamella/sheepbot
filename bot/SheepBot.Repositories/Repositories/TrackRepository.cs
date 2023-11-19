using System.Data;
using Dapper;
using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Helpers;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class TrackRepository : RepositoryBase<Track>, ITrackRepository
{
    public TrackRepository(SqlTransaction transaction) : base(transaction) { }

    public override async Task<IEnumerable<Track>> GetAllAsync()
    {
        var result = await Transaction
            .QueryAsync<Track>(GetAllQuery)
            .ConfigureAwait(false);

        return result ?? new List<Track>();
    }

    public override async Task<Track?> FindAsync(int id)
    {
        var parameters = new { id };

        var result = await Transaction
            .QueryAsync<Track>(FindQuery, parameters)
            .ConfigureAwait(false);
        
        return result.SingleOrDefault();
    }

    public override async Task<int> InsertAsync(Track entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Name", entity.Name);
        parameters.Add("@IsFree", entity.IsFree);
        parameters.Add("@IsLegacy", entity.IsLegacy);
        parameters.Add("@Id", null, DbType.Int32, direction: ParameterDirection.Output);

        await Transaction.ExecuteAsync(InsertQuery, parameters).ConfigureAwait(false);

        return parameters.Get<int>("@Id");
    }

    public override async Task<int> InsertRangeAsync(IEnumerable<Track> entities)
    {
        var table = entities.PopulateTable();
        
        using var bulkInsert = new SqlBulkCopy(Transaction.Connection, SqlBulkCopyOptions.Default, Transaction);
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Track.Name), "name"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Track.IsFree), "is_free"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Track.IsLegacy), "is_legacy"));
        bulkInsert.DestinationTableName = table.TableName;
        await bulkInsert.WriteToServerAsync(table).ConfigureAwait(false);
        
        return bulkInsert.RowsCopied;
    }

    public override async Task<int> UpdateAsync(Track entity)
    {
        var parameters = new { entity.Id, entity.Name, entity.IsFree, entity.IsLegacy };
        return await Transaction.ExecuteAsync(UpdateQuery, parameters).ConfigureAwait(false);
    }

    public override async Task<int> DeleteAsync(int id)
    {
        var parameters = new { id };
        return await Transaction.ExecuteAsync(DeleteQuery, parameters).ConfigureAwait(false);
    }
    
    private const string GetAllQuery = """
                                       SELECT id, 
                                              name, 
                                              is_free AS IsFree, 
                                              is_legacy AS IsLegacy
                                       FROM [dbo].[track] WITH (NOLOCK)
                                       """;
    
    private const string FindQuery = """
                                     SELECT id, 
                                            name, 
                                            is_free AS IsFree, 
                                            is_legacy AS IsLegacy
                                     FROM [dbo].[track] WITH (NOLOCK)
                                     WHERE id=@Id
                                     """;
    
    private const string InsertQuery = """
                                       INSERT INTO [dbo].[track] (name, is_free, is_legacy)
                                          VALUES (@Name, @IsFree, @IsLegacy);
                                          SELECT @Id = SCOPE_IDENTITY()
                                       """;
    
    private const string UpdateQuery = """
                                       UPDATE [dbo].[track]
                                       SET name=@Name, is_free=@IsFree, is_legacy=@IsLegacy
                                       WHERE id=@Id
                                       """;
    
    private const string DeleteQuery = "DELETE FROM [dbo].[track] WHERE id=@id";
}