using SheepBot.Models;

namespace SheepBot.Repositories.Interfaces;

public interface ICarClassRepository : IRepositoryBase<Class>
{
    Task<IEnumerable<Class>> GetClassesForIds(IEnumerable<long> ids);
}