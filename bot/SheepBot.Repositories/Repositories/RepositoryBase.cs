using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public abstract class RepositoryBase<TModel> : IRepositoryBase<TModel>
    where TModel : ModelBase
{
    protected RepositoryBase(SqlTransaction transaction)
    {
        Transaction = transaction;
    }
    
    protected SqlTransaction Transaction { get; }
    
    public abstract Task<IEnumerable<TModel>> GetAllAsync();
    public abstract Task<TModel?> FindAsync(long id);
    public abstract Task<long> InsertAsync(TModel entity);
    public abstract Task<long> InsertRangeAsync(IEnumerable<TModel> entities);
    public abstract Task<int> UpdateAsync(TModel entity);
    public abstract Task<int> DeleteAsync(long id);
    
    public virtual async Task<IEnumerable<TModel>> FindAsync(Predicate<TModel> predicate)
    {
        var results = await GetAllAsync().ConfigureAwait(false);
        return results.Where(e => predicate(e));
    }
    
    public virtual Task<int> DeleteAsync(TModel entity) => DeleteAsync(entity.Id);
}