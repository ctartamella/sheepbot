using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class SeriesRepository : RepositoryBase<Series>, ISeriesRepository
{
    public SeriesRepository(SqlTransaction transaction) : base(transaction) { }
    
    public override async Task<IEnumerable<Series>> GetAllAsync()
    {
        const string query = "SELECT * FROM [dbo].[series]";

        var result = await Transaction.QueryAsync<Series>(query).ConfigureAwait(false);
        
        return result ?? new List<Series>();
    }
    
    public override async Task<Series?> FindAsync(int id)
    {
        const string query = "SELECT * FROM [dbo].[car] WHERE id=@id";
        var parameters = new { id };

        var result = await Transaction.QueryAsync<Series>(query, parameters).ConfigureAwait(false);
        return result.SingleOrDefault();
    }

    public override Task<int> InsertAsync(Series entity)
    {
        throw new NotImplementedException();
    }

    public override Task<int> InsertRangeAsync(IEnumerable<Series> entities)
    {
        throw new NotImplementedException();
    }

    public override Task<bool> UpdateAsync(Series entity)
    {
        throw new NotImplementedException();
    }

    public override Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}