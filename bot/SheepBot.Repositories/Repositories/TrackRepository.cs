using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class TrackRepository : RepositoryBase<Track>, ITrackRepository
{
    public TrackRepository(SqlTransaction transaction) : base(transaction) { }
    
    public override async Task<IEnumerable<Track>> GetAllAsync()
    {
        const string query = "SELECT * FROM [dbo].[track]";

        var result = await Transaction.QueryAsync<Track>(query).ConfigureAwait(false);
        return result ?? new List<Track>();
    }
    
    public override async Task<Track?> FindAsync(int id)
    {
        const string query = "SELECT * FROM [dbo].[track] WHERE id=@id";
        var parameters = new { id };

        var result = await Transaction.QueryAsync<Track>(query, parameters).ConfigureAwait(false);
        
        return result.SingleOrDefault();
    }

    public override Task<int> InsertAsync(Track entity)
    {
        throw new NotImplementedException();
    }

    public override Task<int> InsertRangeAsync(IEnumerable<Track> entities)
    {
        throw new NotImplementedException();
    }

    public override Task<bool> UpdateAsync(Track entity)
    {
        throw new NotImplementedException();
    }

    public override Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}