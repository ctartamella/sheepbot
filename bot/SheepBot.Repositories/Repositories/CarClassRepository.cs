using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Dapper;
using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Helpers;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

[Table("[dbo].[class_car")]
public class CarClassRepository : RepositoryBase<Class>, ICarClassRepository
{
    public CarClassRepository(SqlTransaction transaction) : base(transaction) { }
    
    public override async Task<IEnumerable<Class>> GetAllAsync()
    {
        var results= await Transaction
            .QueryAsync<Class>(GetAllQuery)
            .ConfigureAwait(false);

        return results ?? new List<Class>();
    }

    public override async Task<Class?> FindAsync(long id)
    {
        var parameters = new { id };
        var results = await Transaction
            .QueryAsync<Class>(FindQuery , parameters)
            .ConfigureAwait(false);

        return results.SingleOrDefault();
    }

    public override async Task<long> InsertAsync(Class entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Name", entity.Name, DbType.String);
        parameters.Add("@Id", null, DbType.Int64, ParameterDirection.Output);

        await Transaction.ExecuteAsync(InsertQuery, parameters).ConfigureAwait(false);

        return parameters.Get<long>("@Id");
    }

    public override async Task<long> InsertRangeAsync(IEnumerable<Class> entities)
    {
        var table = entities.PopulateTable();
        
        using var bulkInsert = new SqlBulkCopy(Transaction.Connection, SqlBulkCopyOptions.Default, Transaction);
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("name", "name"));
        bulkInsert.DestinationTableName = table.TableName;
        
        await bulkInsert.WriteToServerAsync(table).ConfigureAwait(false);
        return bulkInsert.RowsCopied;
    }

    public override async Task<int> UpdateAsync(Class entity)
    {
        var parameters = new { entity.Id, entity.Name };
        return await Transaction.ExecuteAsync(UpdateQuery, parameters).ConfigureAwait(false);
    }
    
    public override async Task<int> DeleteAsync(long id)
    {
        var parameters = new { id };
        return await Transaction.ExecuteAsync(DeleteQuery, parameters).ConfigureAwait(false);
    }

    public async Task<IEnumerable<Class>> GetClassesForIds(IEnumerable<long> ids)
    {
        var results = await Transaction.QueryAsync<Class>(GetClassesForIdsQuery, new { ids }).ConfigureAwait(false);
        return results ?? new List<Class>();
    }
    
    private const string GetAllQuery ="""
                                      SELECT class.id, class.name
                                      FROM [dbo].[class] WITH (NOLOCK)
                                      """;
    
    private const string FindQuery = """
                                     SELECT class.id, class.name
                                     FROM [dbo].[class] WITH (NOLOCK)
                                     WHERE class.id=@id
                                     """;
    
    private const string InsertQuery = """
                                       INSERT INTO [dbo].[class]
                                           (name)
                                           VALUES (@Name)
                                       SELECT @Id = SCOPE_IDENTITY()
                                       """;
    
    private const string UpdateQuery = """
                                       UPDATE [dbo].[class]
                                       SET name=@Name
                                       WHERE id=@Id
                                       """;
    
    private const string DeleteQuery = "DELETE FROM [dbo].[class] WHERE id=@id";
    
    private const string GetClassesForIdsQuery = """
                                                SELECT c.* FROM [dbo].[class] c
                                                WHERE c.id IN @Ids
                                                """;
}