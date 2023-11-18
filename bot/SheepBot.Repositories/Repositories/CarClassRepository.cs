using System.Data;
using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class CarClassRepository : RepositoryBase<CarClass>, ICarClassRepository
{
    public CarClassRepository(SqlTransaction transaction) : base(transaction) { }

    public override IEnumerable<CarClass> GetAll()
    {
        const string query = """
                             SELECT class.*, car.id as CarId, car.name, car.is_free, car.is_legacy
                             FROM [dbo].[class]
                             LEFT JOIN dbo.class_car cc on class.id = cc.class_id
                             LEFT JOIN dbo.car car on car.id = cc.car_id
                             """;

        var carClasses= Transaction.Query(query, SetChildren, splitOn: "CarId");

        return carClasses ?? new List<CarClass>();
    }
    
    public override CarClass? Find(int id)
    {
        const string query = """
                             SELECT class.*, car.id as CarId, car.name
                             FROM [dbo].[class]
                             INNER JOIN dbo.class_car cc on class.id = cc.class_id
                             INNER JOIN dbo.car car on car.id = cc.car_id
                             WHERE class.id=@id
                             """;
        
        var parameters = new { id };
        var carClasses = Transaction.Query(query, SetChildren , parameters, splitOn: "CarId");

        return carClasses.SingleOrDefault();
    }

    public override int Insert(CarClass entity)
    {
        const string query = """
                             INSERT INTO [dbo].[class] 
                                 (name)
                                 VALUES (@Name)
                             """;

        var carParameters = new { entity.Name };

        return Transaction.Execute(query, carParameters);
    }

    public override int InsertRange(IEnumerable<CarClass> entities)
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
        bulkInsert.WriteToServer(table);

        return bulkInsert.RowsCopied;
    }

    public override bool Update(CarClass entity)
    {
        throw new NotImplementedException();
    }

    public override int Delete(int id)
    {
        const string query = "DELETE FROM [dbo].[class] WHERE id=@id";

        var parameters = new { id };
        return Transaction.Execute(query, parameters);
    }
    
    private static readonly Func<CarClass, Car, CarClass> SetChildren = (carClass, car) =>
    {
        if (car.Id != 0)
        {
            carClass.Cars.Add(car);
        }
            
        return carClass;
    };
}