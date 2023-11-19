using System.Data;
using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class CarClassRepository : RepositoryBase<CarClass>, ICarClassRepository
{
    public CarClassRepository(SqlTransaction transaction) : base(transaction) { }

    public override async Task<IEnumerable<CarClass>> GetAllAsync()
    {
        const string query = """
                             SELECT class.id, class.name
                             FROM [dbo].[class] WITH (NOLOCK)
                             """;

        var carClasses= await Transaction
            .QueryAsync<CarClass>(query)
            .ConfigureAwait(false);

        return carClasses ?? new List<CarClass>();
    }
    
    public override async Task<CarClass?> FindAsync(int id)
    {
        const string query = """
                             SELECT class.id, class.name
                             FROM [dbo].[class] WITH (NOLOCK)
                             WHERE class.id=@id
                             """;
        
        var parameters = new { id };
        var carClasses = await Transaction
            .QueryAsync<CarClass>(query , parameters)
            .ConfigureAwait(false);

        return carClasses.SingleOrDefault();
    }

    public override async Task<int> InsertAsync(CarClass entity)
    {
        const string query = """
                             INSERT INTO [dbo].[class] 
                                 (name)
                                 VALUES (@Name)
                             """;

        var carParameters = new { entity.Name };

        return await Transaction.ExecuteAsync(query, carParameters).ConfigureAwait(false);
    }

    public override async Task<int> InsertRangeAsync(IEnumerable<CarClass> entities)
    {
        var table = new DataTable();
        table.TableName = "[dbo].[class]";
        table.Columns.Add("name", typeof(string));
        
        foreach (var carClass in entities)
        {
            var row = table.NewRow();
            row["name"] = carClass.Name;

            table.Rows.Add(row);
        }
        
        using var bulkInsert = new SqlBulkCopy(Transaction.Connection, SqlBulkCopyOptions.Default, Transaction);
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("name", "name"));
        bulkInsert.DestinationTableName = table.TableName;
        
        await bulkInsert.WriteToServerAsync(table).ConfigureAwait(false);
        return bulkInsert.RowsCopied;
    }

    public override Task<bool> UpdateAsync(CarClass entity)
    {
        throw new NotImplementedException();
    }

    public override async Task<int> DeleteAsync(int id)
    {
        const string query = "DELETE FROM [dbo].[class] WHERE id=@id";

        var parameters = new { id };
        return await Transaction.ExecuteAsync(query, parameters).ConfigureAwait(false);
    }

    public async Task<IEnumerable<CarClass>> GetClassesForIds(IEnumerable<int> ids)
    {
        const string query = """
                             SELECT c.* FROM [dbo].[class] c
                             WHERE c.id IN @Ids
                             """;
        var results = await Transaction.QueryAsync<CarClass>(query, new { ids }).ConfigureAwait(false);
        return results ?? new List<CarClass>();
    }
}