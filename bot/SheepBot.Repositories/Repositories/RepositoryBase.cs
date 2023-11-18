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
    
    public abstract IEnumerable<TModel> GetAll();
    public abstract TModel? Find(int id);
    public abstract int Insert(TModel entity);
    public abstract int InsertRange(IEnumerable<TModel> entities);
    public abstract bool Update(TModel entity);
    public abstract int Delete(int id);
    public virtual int Delete(TModel entity) => Delete(entity.Id);
}