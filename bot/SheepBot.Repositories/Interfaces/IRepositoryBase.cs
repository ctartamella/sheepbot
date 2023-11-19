namespace SheepBot.Repositories.Interfaces;

public interface IRepositoryBase<TModel>
{
    Task<IEnumerable<TModel>> GetAllAsync();
    Task<TModel?> FindAsync(int id);
    Task<IEnumerable<TModel>> FindAsync(Predicate<TModel> predicate);
    Task<int> InsertAsync(TModel entity);
    Task<int> InsertRangeAsync(IEnumerable<TModel> entities);
    Task<int> UpdateAsync(TModel entity);
    Task<int> DeleteAsync(int id);
    Task<int> DeleteAsync(TModel entity);
}