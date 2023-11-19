using SheepBot.Models;

namespace SheepBot.Repositories.Interfaces;

public interface ICarClassRepository : IRepositoryBase<CarClass>
{
    Task<IEnumerable<CarClass>> GetClassesForIds(IEnumerable<int> ids);
}