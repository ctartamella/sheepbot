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
    public abstract Task<TModel?> FindAsync(int id);
    public abstract Task<int> InsertAsync(TModel entity);
    public abstract Task<int> InsertRangeAsync(IEnumerable<TModel> entities);
    public abstract Task<bool> UpdateAsync(TModel entity);
    public abstract Task<int> DeleteAsync(int id);
    public virtual Task<int> DeleteAsync(TModel entity) => DeleteAsync(entity.Id);
}