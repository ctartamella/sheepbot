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

    public override IEnumerable<Car> GetAll()
    {
        const string query = """
                             SELECT car.id, car.name, car.is_free, car.is_legacy, class.id as ClassId, class.name
                             FROM [dbo].[car] car WITH (NOLOCK)
                             LEFT JOIN dbo.class_car cc on car.id = cc.car_id
                             LEFT JOIN dbo.class class on cc.car_id = class.id
                             """;

        var result = Transaction.Query(query, SetChildren, splitOn: "ClassId");
        return result ?? new List<Car>();
    }

    public override Car? Find(int id)
    {
        const string query = """
                             SELECT car.*, carClass.id as ClassId, carClass.name 
                             FROM [dbo].[car] car
                             LEFT JOIN dbo.class_car cc on car.id = cc.car_id
                             LEFT JOIN dbo.class carClass on carClass.id = cc.car_id
                             WHERE car.id=@id
                             """;
        
        var parameters = new { id };

        var car = Transaction.Query(query, SetChildren, parameters, splitOn: "ClassId").SingleOrDefault();
        
        return car;
    }

    public override int Insert(Car entity)
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

        Transaction.Execute(query, carParameters);

        return carParameters.Get<int>("@Id");
    }

    public override int InsertRange(IEnumerable<Car> entities)
    {
        var carTable = new DataTable();
        carTable.TableName = "[dbo].[car]";
        carTable.Columns.Add("name", typeof(string));
        carTable.Columns.Add("is_free", typeof(bool));
        carTable.Columns.Add("is_legacy", typeof(bool));

        var joinTable = new DataTable();
        joinTable.TableName = "[dbo].[class_car]";
        joinTable.Columns.Add("car_id", typeof(int));
        joinTable.Columns.Add("class_id", typeof(int));
        
        foreach (var car in entities)
        {
            var row = carTable.NewRow();
            row["name"] = car.Name;
            row["is_free"] =  false;
            row["is_legacy"] = false;
            
            foreach (var carClass in car.Classes)
            {
                if (carClass.Id == 0) { continue; }
                
                var joinRow = joinTable.NewRow();
                joinRow["car_id"] = car.Id;
                joinRow["class_id"] = carClass.Id;

                joinTable.Rows.Add(joinRow);
            }

            carTable.Rows.Add(row);
        }
        
        using var bulkInsert = new SqlBulkCopy(Transaction.Connection, SqlBulkCopyOptions.Default, Transaction);
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("name", "name"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("is_legacy", "is_legacy"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("is_free", "is_free"));
        bulkInsert.DestinationTableName = carTable.TableName;
        bulkInsert.WriteToServer(carTable);

        using var classJoinInsert = new SqlBulkCopy(Transaction.Connection, SqlBulkCopyOptions.Default, Transaction);
        classJoinInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("car_id", "car_id"));
        classJoinInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("class_id", "class_id"));
        classJoinInsert.DestinationTableName = joinTable.TableName;
        classJoinInsert.WriteToServer(joinTable);
        
        return bulkInsert.RowsCopied;
    }

    public override bool Update(Car entity)
    {
        throw new NotImplementedException();
    }

    public override int Delete(int id)
    {
        const string query = "DELETE FROM [dbo].[car] WHERE id=@id";

        var parameters = new { id };
        return Transaction.Execute(query, parameters);
    }
    
    private static readonly Func<Car, CarClass, Car> SetChildren = (car, carClass) =>
    {
        if (car.Id != 0)
        {
            carClass.Cars.Add(car);
        }
            
        return car;
    };
}