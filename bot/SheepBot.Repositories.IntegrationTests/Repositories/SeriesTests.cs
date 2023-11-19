using SheepBot.Models;
using SheepBot.Models.Enums;
using SheepBot.Repositories.Tests.Base;

namespace SheepBot.Repositories.Tests.Repositories;

[Collection("Transactional")]
public class SeriesTests : ModelTestBase<Series>
{
    public SeriesTests() : base(unitOfWork => unitOfWork.SeriesRepository)
    {
    }

    protected override Task<IEnumerable<Series>> CreateEntityListAsync()
    {
        var dataSize = NextRandom();
        var results = Enumerable.Range(1, dataSize)
            .Select(i => new Series
            {
                Name = $"Series {i}",
                Type = (SeriesType)(i % Enum.GetValues<SeriesType>().Length) + 1, 
                DiscordServer = $"Discord {i}",
                IracingUrl = $"Iracing {i}"
            });

        return Task.FromResult(results);
    }

    protected override async Task<Series> CreateEntityAsync()
    {
        var idx = NextRandom();
        var role = new Role
        {
            DiscordId = idx,
            RoleName = $"Test Role Name {idx}"
        };
        var id = await UnitOfWork.RoleRepository.InsertAsync(role);
        
        idx = NextRandom();
        var result = new Series
        {
            Name = $"Series {idx}",
            RoleId = id,
            Type = (SeriesType)(idx % Enum.GetValues<SeriesType>().Length) + 1,
            Website = $"Website {idx}",
            DiscordServer = $"Discord {idx}",
            IracingUrl = $"'Iracing {idx}"
        };

        return result;
    }
    
    protected override async Task<Series> UpdateEntity(Series entity)
    {
        var role = new Role { RoleName = "New Role" };
        var roleId = await UnitOfWork.RoleRepository.InsertAsync(role);
        
        entity.Name = "New Series";
        entity.DiscordServer = "New Discord Server";
        entity.IracingUrl = "New iracing Url";
        entity.Website = "New Website";
        entity.Type = (SeriesType)(((int)entity.Type + 1) % Enum.GetValues<SeriesType>().Length + 1);
        entity.RoleId = roleId;

        return entity;
    }
}