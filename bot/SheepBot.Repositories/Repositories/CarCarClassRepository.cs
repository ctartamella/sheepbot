using System.Data;
using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class CarCarClassRepository : RepositoryBase<CarCarClass>, ICarCarClassRepository
{
    public CarCarClassRepository(SqlTransaction transaction) : base(transaction)
    {
        
    }
    
    public override async Task<IEnumerable<CarCarClass>> GetAllAsync()
    {
        const string query = """
                             SELECT id, car_id, class_id
                             FROM [dbo].[class_car] WITH (NOLOCK)
                             """;
        return await Transaction.QueryAsync<CarCarClass>(query).ConfigureAwait(false);
    }

    public override async Task<CarCarClass?> FindAsync(int id)
    {
        const string query = """
                             SELECT id, car_id, class_id
                             FROM [dbo].[class_car] WITH (NOLOCK)
                             WHERE id=@Id
                             """;
        
        var parameters = new { id };
        var result = await Transaction
            .QueryAsync<CarCarClass>(query, parameters)
            .ConfigureAwait(false);
        
        return result.SingleOrDefault();
    }

    public override Task<int> InsertAsync(CarCarClass entity)
    {
        throw new NotImplementedException();
    }

    public override async Task<int> InsertRangeAsync(IEnumerable<CarCarClass> entities)
    {
        var table = new DataTable();
        table.TableName = "[dbo].[class_car]";
        table.Columns.Add("car_id", typeof(int));
        table.Columns.Add("class_id", typeof(int));

        foreach (var entity in entities)
        {
            var row = table.NewRow();
            row["car_id"] = entity.CarId;
            row["class_id"] = entity.ClassId;

            table.Rows.Add(row);
        }
        
        using var bulkInsert = new SqlBulkCopy(Transaction.Connection, SqlBulkCopyOptions.Default, Transaction);
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("car_id", "car_id"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("class_id", "class_id"));
        bulkInsert.DestinationTableName = table.TableName;
        await bulkInsert.WriteToServerAsync(table).ConfigureAwait(false);

        return bulkInsert.RowsCopied;
    }

    public override Task<bool> UpdateAsync(CarCarClass entity)
    {
        throw new NotImplementedException();
    }

    public override Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<IDictionary<int, IEnumerable<int>>> GetJoinsForCarIdsAsync(IEnumerable<int> ids)
    {
        const string query = """
                             SELECT car_id, c.class_id FROM [dbo].[class_car] c
                             WHERE car_id IN @Ids
                             """;
        var results = await Transaction.QueryAsync<CarCarClass>(query, new { ids }).ConfigureAwait(false);

        var pairs = results.GroupBy(
            cc => cc.CarId,
            cc => cc.ClassId,
            (key, g) => new KeyValuePair<int, IEnumerable<int>>(key, g.ToList())
        );

        return new Dictionary<int, IEnumerable<int>>(pairs);
    }
}