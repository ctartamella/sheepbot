using System.Data;
using Dapper;
using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Helpers;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class CarCarClassRepository : RepositoryBase<CarCarClass>, ICarCarClassRepository
{
    public CarCarClassRepository(SqlTransaction transaction) : base(transaction) { }
    
    public override async Task<IEnumerable<CarCarClass>> GetAllAsync()
    {
        return await Transaction.QueryAsync<CarCarClass>(GetAllQuery).ConfigureAwait(false);
    }
    
    public override async Task<CarCarClass?> FindAsync(int id)
    {
        var parameters = new { id };
        var result = await Transaction
            .QueryAsync<CarCarClass>(FindQuery, parameters)
            .ConfigureAwait(false);
        
        return result.SingleOrDefault();
    }

    public override async Task<int> InsertAsync(CarCarClass entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CarId", entity.CarId);
        parameters.Add("@ClassId", entity.ClassId);
        parameters.Add("@Id", null, DbType.Int32, direction: ParameterDirection.Output);

        await Transaction.ExecuteAsync(InsertQuery, parameters).ConfigureAwait(false);

        return parameters.Get<int>("@Id");
    }

    public override async Task<int> InsertRangeAsync(IEnumerable<CarCarClass> entities)
    {
        var table = entities.PopulateTable();
        
        using var bulkInsert = new SqlBulkCopy(Transaction.Connection, SqlBulkCopyOptions.Default, Transaction);
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("car_id", "car_id"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("class_id", "class_id"));
        bulkInsert.DestinationTableName = table.TableName;
        await bulkInsert.WriteToServerAsync(table).ConfigureAwait(false);

        return bulkInsert.RowsCopied;
    }

    public override async Task<int> UpdateAsync(CarCarClass entity)
    {
        var parameters = new { entity.Id, entity.ClassId, entity.CarId };
        return await Transaction.ExecuteAsync(UpdateQuery, parameters).ConfigureAwait(false);
    }

    public override async Task<int> DeleteAsync(int id)
    {
        var parameters = new { id };
        return await Transaction.ExecuteAsync(DeleteQuery, parameters).ConfigureAwait(false);
    }
    
    public async Task<IDictionary<int, IEnumerable<int>>> GetJoinsForCarIdsAsync(IEnumerable<int> ids)
    {
        var results = await Transaction.QueryAsync<CarCarClass>(GetJoinsQuery, new { ids }).ConfigureAwait(false);

        var pairs = results.GroupBy(
            cc => cc.CarId,
            cc => cc.ClassId,
            (key, g) => new KeyValuePair<int, IEnumerable<int>>(key, g.ToList())
        );

        return new Dictionary<int, IEnumerable<int>>(pairs);
    }
    
    private const string GetAllQuery = """
                                       SELECT id, car_id AS CarId, class_id AS ClassId
                                       FROM [dbo].[class_car] WITH (NOLOCK)
                                       """;
    
    private const string FindQuery = """
                                     SELECT id, car_id AS CarId, class_id AS ClassId
                                     FROM [dbo].[class_car] WITH (NOLOCK)
                                     WHERE id=@Id
                                     """;

    private const string InsertQuery = """
                                       INSERT INTO [dbo].[class_car] (car_id, class_id)
                                          VALUES (@CarId, @ClassId);
                                          SELECT @Id = SCOPE_IDENTITY()
                                       """;

    private const string UpdateQuery = """
                                       UPDATE [dbo].[class_car]
                                       SET class_id=@ClassId, car_id=@CarId
                                       WHERE id=@Id
                                       """;
    
    private const string DeleteQuery = "DELETE FROM [dbo].[class_car] WHERE id=@Id";
    
    private const string GetJoinsQuery = """
                                         SELECT car_id, c.class_id FROM [dbo].[class_car] c
                                         WHERE car_id IN @Id
                                         """;
}