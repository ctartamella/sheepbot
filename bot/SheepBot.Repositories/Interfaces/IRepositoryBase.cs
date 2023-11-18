namespace SheepBot.Repositories.Interfaces;

public interface IRepositoryBase<T>
{
    IEnumerable<T> GetAll();
    T? Find(int id);
    int Insert(T entity);
    int InsertRange(IEnumerable<T> entities);
    bool Update(T entity);
    int Delete(int id);
    int Delete(T entity);
}