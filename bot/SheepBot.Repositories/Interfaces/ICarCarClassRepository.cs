using SheepBot.Models;

namespace SheepBot.Repositories.Interfaces;

public interface ICarCarClassRepository : IRepositoryBase<CarCarClass>
{
    Task<IDictionary<int, IEnumerable<int>>> GetJoinsForCarIdsAsync(IEnumerable<int> ids);
}