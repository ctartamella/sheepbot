using SheepBot.Models;

namespace SheepBot.Repositories.Interfaces;

public interface ICarCarClassRepository : IRepositoryBase<CarCarClass>
{
    Task<IDictionary<long, IEnumerable<long>>> GetJoinsForCarIdsAsync(IEnumerable<long> ids);
}