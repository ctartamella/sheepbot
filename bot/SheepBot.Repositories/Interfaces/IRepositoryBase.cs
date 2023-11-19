namespace SheepBot.Repositories.Interfaces;

public interface IRepositoryBase<TModel>
{
    Task<IEnumerable<TModel>> GetAllAsync();
    Task<TModel?> FindAsync(long id);
    Task<IEnumerable<TModel>> FindAsync(Predicate<TModel> predicate);
    Task<long> InsertAsync(TModel entity);
    Task<long> InsertRangeAsync(IEnumerable<TModel> entities);
    Task<int> UpdateAsync(TModel entity);
    Task<int> DeleteAsync(long id);
    Task<int> DeleteAsync(TModel entity);
}