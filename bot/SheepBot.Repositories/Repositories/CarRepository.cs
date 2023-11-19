using System.Data;
using Dapper;
using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Helpers;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class CarRepository : RepositoryBase<Car>, ICarRepository
{
    public CarRepository(SqlTransaction transaction) : base(transaction)
    {
    }

    public override async Task<IEnumerable<Car>> GetAllAsync()
    {
        var result = await Transaction
            .QueryAsync<Car>(GetAllQuery)
            .ConfigureAwait(false);
        
        return result ?? new List<Car>();
    }

    public override async Task<Car?> FindAsync(int id)
    {
        var parameters = new { id };

        var result = await Transaction
            .QueryAsync<Car>(FindQuery, parameters)
            .ConfigureAwait(false);
        
        return result.SingleOrDefault();
    }

    public override async Task<int> InsertAsync(Car entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Name", entity.Name);
        parameters.Add("@IsFree", entity.IsFree);
        parameters.Add("@IsLegacy", entity.IsLegacy);
        parameters.Add("@Id", null, DbType.Int32, direction: ParameterDirection.Output);

        await Transaction.ExecuteAsync(InsertQuery, parameters).ConfigureAwait(false);

        return parameters.Get<int>("@Id");
    }

    public override async Task<int> InsertRangeAsync(IEnumerable<Car> entities)
    {
        var table = entities.PopulateTable();
        
        using var bulkInsert = new SqlBulkCopy(Transaction.Connection, SqlBulkCopyOptions.Default, Transaction);
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Car.Name), "name"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Car.IsFree), "is_free"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Car.IsLegacy), "is_legacy"));
        bulkInsert.DestinationTableName = table.TableName;
        
        await bulkInsert.WriteToServerAsync(table).ConfigureAwait(false);
        
        return bulkInsert.RowsCopied;
    }

    public override async Task<int> UpdateAsync(Car entity)
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
                                       SELECT car.id, car.name, car.is_free as IsFree, car.is_legacy as IsLegacy
                                       FROM [dbo].[car] car WITH (NOLOCK)
                                       """;
    
    private const string FindQuery = """
                                     SELECT car.id, car.name, car.is_free as IsFree, car.is_legacy as IsLegacy
                                     FROM [dbo].[car] car WITH (NOLOCK)
                                     WHERE car.id=@Id
                                     """;
    
    private const string InsertQuery = """
                                       INSERT INTO [dbo].[car] (name, is_free, is_legacy)
                                          VALUES (@Name, @IsFree, @IsLegacy);
                                          SELECT @Id = SCOPE_IDENTITY()
                                       """;
    
    private const string UpdateQuery = """
                                       UPDATE [dbo].[car]
                                       SET name=@Name, is_free=@IsFree, is_legacy=@IsLegacy
                                       WHERE id=@Id
                                       """;
    
    private const string DeleteQuery = "DELETE FROM [dbo].[car] WHERE id=@id";
}