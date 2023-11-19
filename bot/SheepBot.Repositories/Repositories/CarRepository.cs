using System.Data;
using Dapper;
using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class CarRepository : RepositoryBase<Car>, ICarRepository
{
    public CarRepository(SqlTransaction transaction) : base(transaction)
    {
    }

    public override async Task<IEnumerable<Car>> GetAllAsync()
    {
        const string query = """
                             SELECT car.id, car.name, car.is_free as IsFree, car.is_legacy as IsLegacy
                             FROM [dbo].[car] car WITH (NOLOCK)
                             """;

        var result = await Transaction
            .QueryAsync<Car>(query)
            .ConfigureAwait(false);
        
        return result ?? new List<Car>();
    }

    public override async Task<Car?> FindAsync(int id)
    {
        const string query = """
                             SELECT car.id, car.name, car.is_free as IsFree, car.is_legacy as IsLegacy
                             FROM [dbo].[car] car WITH (NOLOCK)
                             WHERE car.id=@Id
                             """;
        
        var parameters = new { id };

        var car = await Transaction
            .QueryAsync<Car>(query, parameters)
            .ConfigureAwait(false);
        
        return car.SingleOrDefault();
    }

    public override async Task<int> InsertAsync(Car entity)
    {
        const string query = """
                             INSERT INTO [dbo].[car] (name, is_free, is_legacy)
                                VALUES (@Name, @IsFree, @IsLegacy);
                                SELECT @Id = SCOPE_IDENTITY()
                             """;
        
        var carParameters = new DynamicParameters();
        carParameters.Add("@Name", entity.Name);
        carParameters.Add("@IsFree", entity.IsFree);
        carParameters.Add("@IsLegacy", entity.IsLegacy);
        carParameters.Add("@Id", null, DbType.Int32, direction: ParameterDirection.Output);

        await Transaction.ExecuteAsync(query, carParameters).ConfigureAwait(false);

        return carParameters.Get<int>("@Id");
    }

    public override async Task<int> InsertRangeAsync(IEnumerable<Car> entities)
    {
        var carTable = new DataTable();
        carTable.TableName = "[dbo].[car]";
        carTable.Columns.Add("name", typeof(string));
        carTable.Columns.Add("is_free", typeof(bool));
        carTable.Columns.Add("is_legacy", typeof(bool));
        
        foreach (var car in entities)
        {
            var row = carTable.NewRow();
            row["name"] = car.Name;
            row["is_free"] =  car.IsFree;
            row["is_legacy"] = car.IsLegacy;

            carTable.Rows.Add(row);
        }
        
        using var bulkInsert = new SqlBulkCopy(Transaction.Connection, SqlBulkCopyOptions.Default, Transaction);
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("name", "name"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("is_legacy", "is_legacy"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("is_free", "is_free"));
        bulkInsert.DestinationTableName = carTable.TableName;
        await bulkInsert.WriteToServerAsync(carTable).ConfigureAwait(false);
        
        return bulkInsert.RowsCopied;
    }

    public override Task<bool> UpdateAsync(Car entity)
    {
        throw new NotImplementedException();
    }

    public override async Task<int> DeleteAsync(int id)
    {
        const string query = "DELETE FROM [dbo].[car] WHERE id=@id";

        var parameters = new { id };
        return await Transaction.ExecuteAsync(query, parameters).ConfigureAwait(false);
    }
}