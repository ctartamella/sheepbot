using SheepBot.Models;
using SheepBot.Models.Joins;

namespace SheepBot.Repositories.Interfaces;

public interface ICarCarClassRepository : IRepositoryBase<CarClassJoin>
{
    Task<IDictionary<long, IEnumerable<long>>> GetJoinsForCarIdsAsync(IEnumerable<long> ids);
}