using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Tests.Base;

namespace SheepBot.Repositories.Tests.Repositories;

[Collection("Transactional")]
public sealed class CarTests : ModelTestBase<Car>
{
    public CarTests() : base(unit => unit.CarRepository)
    {
        
    }
    
    [Fact]
    public async Task ValidatePricingConstraintBothTrue()
    {
        // Arrange
        var data = new Car
        {
            Name = "Test Car",
            IsFree = true,
            IsLegacy = true
        };
        
        // Act/Assert
        await Assert.ThrowsAsync<SqlException>(async () => await Repository.InsertAsync(data));
    }
    
    protected override Task<IEnumerable<Car>> CreateEntityListAsync()
    {
        var dataSize = NextRandom();
        var result = Enumerable.Range(1, dataSize)
            .Select(i => new Car
            {
                Name = $"Test Car {i}",
                IsFree = Convert.ToBoolean(i % 2),
                IsLegacy = !Convert.ToBoolean(i % 2)
            });

        return Task.FromResult(result);
    }

    protected override Task<Car> CreateEntityAsync()
    { 
        var baseBool = Convert.ToBoolean(NextRandom() % 2);
        var result = new Car
        {
            Name = "Test Car",
            IsFree = baseBool,
            IsLegacy = !baseBool
        };

        return Task.FromResult(result);
    }
    
    protected override Task<Car> UpdateEntity(Car entity)
    {
        entity.Name = "New Car Class";
        
        // Swap bools to avoid triggering constraints.
        (entity.IsFree, entity.IsLegacy) = (entity.IsLegacy, entity.IsFree);

        return Task.FromResult(entity);
    }
}