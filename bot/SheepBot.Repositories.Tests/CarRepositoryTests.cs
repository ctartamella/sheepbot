using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Tests.Base;

namespace SheepBot.Repositories.Tests;

public sealed class CarRepositoryTests : ModelTestBase<Car>
{
    public CarRepositoryTests() : base(unit => unit.CarRepository)
    {
        
    }
    
    [Fact]
    public void ValidatePricingConstraintBothTrue()
    {
        // Arrange
        var data = CreateEntity();
        
        // Act/Assert
        Assert.ThrowsAsync<SqlException>(async () => await Repository.InsertAsync(data));
    }
    
    protected override IEnumerable<Car> CreateEntityList(int dataSize)
    {
        return Enumerable.Range(1, dataSize)
            .Select(i => new Car
            {
                Name = $"Test Car {i}",
                IsFree = Convert.ToBoolean(i % 2),
                IsLegacy = !Convert.ToBoolean(i % 2)
            }).ToList();
    }

    protected override Car CreateEntity()
    { 
        var baseBool = Convert.ToBoolean(NextRandom() % 2);
        return new Car
        {
            Name = "Test Car",
            IsFree = baseBool,
            IsLegacy = !baseBool
        };
    }
}