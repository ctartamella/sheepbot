using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class RaceRepository : RepositoryBase<Race>, IRaceRepository
{
    public RaceRepository(SqlTransaction transaction) : base(transaction) { }

    public override async Task<IEnumerable<Race>> GetAllAsync()
    {
        const string query = "SELECT * FROM [dbo].[race]";

        var result = await Transaction.QueryAsync<Race>(query).ConfigureAwait(false);
        return result ?? new List<Race>();
    }
    
    public override async Task<Race?> FindAsync(int id)
    {
        const string query = "SELECT * FROM [dbo].[race] WHERE id=@id";
        var parameters = new { id };

        var result = await Transaction.QueryAsync<Race>(query, parameters).ConfigureAwait(false);
        return result.SingleOrDefault();
    }

    public override Task<int> InsertAsync(Race entity)
    {
        throw new NotImplementedException();
    }

    public override Task<int> InsertRangeAsync(IEnumerable<Race> entities)
    {
        throw new NotImplementedException();
    }

    public override Task<bool> UpdateAsync(Race entity)
    {
        throw new NotImplementedException();
    }

    public override Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}