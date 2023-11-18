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

    public override IEnumerable<CarCarClass> GetAll()
    {
        const string query = "SELECT * FROM [dbo].[class_car]";
        return Transaction.Query<CarCarClass>(query);
    }

    public override CarCarClass? Find(int id)
    {
        const string query = """
                             SELECT * 
                             FROM [dbo].[class_car]
                             WHERE id=@Id
                             """;
        var parameters = new { id };
        
        return Transaction.Query<CarCarClass>(query, parameters).SingleOrDefault();
    }

    public override int Insert(CarCarClass entity)
    {
        throw new NotImplementedException();
    }

    public override int InsertRange(IEnumerable<CarCarClass> entities)
    {
        var table = new DataTable();
        table.TableName = "[dbo].[class_car]";
        table.Columns.Add("class_id", typeof(int));
        table.Columns.Add("car_id", typeof(int));

        foreach (var entity in entities)
        {
            var row = table.NewRow();
            row["class_id"] = entity.ClassId;
            row["car_id"] = entity.CarId;

            table.Rows.Add(row);
        }
        
        using var bulkInsert = new SqlBulkCopy(Transaction.Connection, SqlBulkCopyOptions.Default, Transaction);
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("car_id", "car_id"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("class_id", "class_id"));
        bulkInsert.DestinationTableName = table.TableName;
        bulkInsert.WriteToServer(table);

        return bulkInsert.RowsCopied;
    }

    public override bool Update(CarCarClass entity)
    {
        throw new NotImplementedException();
    }

    public override int Delete(int id)
    {
        throw new NotImplementedException();
    }
}