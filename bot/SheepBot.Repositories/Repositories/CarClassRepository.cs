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
public class CarClassRepository : RepositoryBase<CarClass>, ICarClassRepository
{
    public CarClassRepository(SqlTransaction transaction) : base(transaction) { }
    
    public override async Task<IEnumerable<CarClass>> GetAllAsync()
    {
        var results= await Transaction
            .QueryAsync<CarClass>(GetAllQuery)
            .ConfigureAwait(false);

        return results ?? new List<CarClass>();
    }

    public override async Task<CarClass?> FindAsync(int id)
    {
        var parameters = new { id };
        var results = await Transaction
            .QueryAsync<CarClass>(FindQuery , parameters)
            .ConfigureAwait(false);

        return results.SingleOrDefault();
    }

    public override async Task<int> InsertAsync(CarClass entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Name", entity.Name, DbType.String);
        parameters.Add("@Id", null, DbType.Int32, ParameterDirection.Output);

        await Transaction.ExecuteAsync(InsertQuery, parameters).ConfigureAwait(false);

        return parameters.Get<int>("@Id");
    }

    public override async Task<int> InsertRangeAsync(IEnumerable<CarClass> entities)
    {
        var table = entities.PopulateTable();
        
        using var bulkInsert = new SqlBulkCopy(Transaction.Connection, SqlBulkCopyOptions.Default, Transaction);
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("name", "name"));
        bulkInsert.DestinationTableName = table.TableName;
        
        await bulkInsert.WriteToServerAsync(table).ConfigureAwait(false);
        return bulkInsert.RowsCopied;
    }

    public override async Task<int> UpdateAsync(CarClass entity)
    {
        var parameters = new { entity.Id, entity.Name };
        return await Transaction.ExecuteAsync(UpdateQuery, parameters).ConfigureAwait(false);
    }
    
    public override async Task<int> DeleteAsync(int id)
    {
        var parameters = new { id };
        return await Transaction.ExecuteAsync(DeleteQuery, parameters).ConfigureAwait(false);
    }

    public async Task<IEnumerable<CarClass>> GetClassesForIds(IEnumerable<int> ids)
    {
        var results = await Transaction.QueryAsync<CarClass>(GetClassesForIdsQuery, new { ids }).ConfigureAwait(false);
        return results ?? new List<CarClass>();
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