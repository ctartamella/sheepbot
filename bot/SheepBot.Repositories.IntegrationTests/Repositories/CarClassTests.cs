using SheepBot.Models;
using SheepBot.Repositories.Tests.Base;

namespace SheepBot.Repositories.Tests.Repositories;

[Collection("Transactional")]
public class CarClassTests : ModelTestBase<CarClass>
{
    public CarClassTests() : base(unitOfWork => unitOfWork.CarClassRepository)
    {
        
    }

    protected override Task<IEnumerable<CarClass>> CreateEntityListAsync()
    {
        var dataSize = NextRandom();
        var result = Enumerable.Range(1, dataSize)
            .Select(i => new CarClass
            {
                Name = $"Test Class {i}"
            });
        return Task.FromResult(result);
    }

    protected override Task<CarClass> CreateEntityAsync()
    {
        var idx = NextRandom();
        var result = new CarClass
        {
            Name = $"Test Class {idx}"
        };

        return Task.FromResult(result);
    }
    
    protected override Task<CarClass> UpdateEntity(CarClass entity)
    {
        entity.Name = "New Car Class";

        return Task.FromResult(entity);
    }
}