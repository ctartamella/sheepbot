using System.Data;
using Dapper;
using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class RaceRepository : RepositoryBase<Race>, IRaceRepository
{
    public RaceRepository(SqlTransaction transaction) : base(transaction) { }

    public override IEnumerable<Race> GetAll()
    {
        const string query = "SELECT * FROM [dbo].[race]";

        return Transaction.Query<Race>(query) ?? new List<Race>();
    }
    
    public override Race? Find(int id)
    {
        const string query = "SELECT * FROM [dbo].[race] WHERE id=@id";
        var parameters = new { id = id };

        return Transaction.Query<Race>(query, parameters).SingleOrDefault();
    }

    public override int Insert(Race entity)
    {
        throw new NotImplementedException();
    }

    public override int InsertRange(IEnumerable<Race> entities)
    {
        throw new NotImplementedException();
    }

    public override bool Update(Race entity)
    {
        throw new NotImplementedException();
    }

    public override int Delete(int id)
    {
        throw new NotImplementedException();
    }
}