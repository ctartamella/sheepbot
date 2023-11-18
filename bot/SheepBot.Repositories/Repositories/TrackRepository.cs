using System.Data;
using Dapper;
using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class TrackRepository : RepositoryBase<Track>, ITrackRepository
{
    public TrackRepository(SqlTransaction transaction) : base(transaction) { }
    
    public override IEnumerable<Track> GetAll()
    {
        const string query = "SELECT * FROM [dbo].[track]";

        return Transaction.Query<Track>(query) ?? new List<Track>();
    }
    
    public override Track? Find(int id)
    {
        const string query = "SELECT * FROM [dbo].[track] WHERE id=@id";
        var parameters = new { id = id };

        return Transaction.Query<Track>(query, parameters).SingleOrDefault();
    }

    public override int Insert(Track entity)
    {
        throw new NotImplementedException();
    }

    public override int InsertRange(IEnumerable<Track> entities)
    {
        throw new NotImplementedException();
    }

    public override bool Update(Track entity)
    {
        throw new NotImplementedException();
    }

    public override int Delete(int id)
    {
        throw new NotImplementedException();
    }
}