using SheepBot.Models;
using SheepBot.Repositories.Tests.Base;

namespace SheepBot.Repositories.Tests;

public class CarClassRepositoryTests : ModelTestBase<CarClass>
{
    public CarClassRepositoryTests() : base(unitOfWork => unitOfWork.CarClassRepository)
    {
        
    }

    protected override IEnumerable<CarClass> CreateEntityList(int dataSize)
    {
        return Enumerable.Range(1, dataSize)
            .Select(i => new CarClass
            {
                Name = $"Test Class {i}"
            });
    }

    protected override CarClass CreateEntity()
    {
        var idx = NextRandom();
        return new CarClass
        {
            Name = $"Test Class {idx}"
        };
    }
}