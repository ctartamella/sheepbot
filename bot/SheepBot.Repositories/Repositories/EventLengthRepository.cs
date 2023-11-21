using Microsoft.Data.SqlClient;
using SheepBot.Models;

namespace SheepBot.Repositories;

public class EventLengthRepository : RepositoryBase<EventLength>
{
    public EventLengthRepository(SqlTransaction transaction) : base(transaction)
    {
    }

    public override Task<IEnumerable<EventLength>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public override Task<EventLength?> FindAsync(long id)
    {
        throw new NotImplementedException();
    }

    public override Task<long> InsertAsync(EventLength entity)
    {
        throw new NotImplementedException();
    }

    public override Task<long> InsertRangeAsync(IEnumerable<EventLength> entities)
    {
        throw new NotImplementedException();
    }

    public override Task<int> UpdateAsync(EventLength entity)
    {
        throw new NotImplementedException();
    }

    public override Task<int> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }
}