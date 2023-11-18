using System.Data;
using Dapper;
using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class SeriesRepository : RepositoryBase<Series>, ISeriesRepository
{
    public SeriesRepository(SqlTransaction transaction) : base(transaction) { }
    
    public override IEnumerable<Series> GetAll()
    {
        const string query = "SELECT * FROM [dbo].[series]";

        return Transaction.Query<Series>(query) ?? new List<Series>();
    }
    
    public override Series? Find(int id)
    {
        const string query = "SELECT * FROM [dbo].[car] WHERE id=@id";
        var parameters = new { id = id };

        return Transaction.Query<Series>(query, parameters).SingleOrDefault();
    }

    public override int Insert(Series entity)
    {
        throw new NotImplementedException();
    }

    public override int InsertRange(IEnumerable<Series> entities)
    {
        throw new NotImplementedException();
    }

    public override bool Update(Series entity)
    {
        throw new NotImplementedException();
    }

    public override int Delete(int id)
    {
        throw new NotImplementedException();
    }
}