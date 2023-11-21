using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Models.Joins;
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
    
    protected override async Task<IEnumerable<Car>> ArrangeArrayData()
    {
        var classListSize = NextRandom(10, 20);
        var classes = Enumerable.Range(1, classListSize)
            .Select(i => new Class { Name = $"Test Class {i}" })
            .ToList();

        await UnitOfWork.CarClassRepository.InsertRangeAsync(classes);
        
        var dataSize = NextRandom();
        var cars = Enumerable.Range(1, dataSize)
            .Select(i => new Car
            {
                Name = $"Test Car {i}",
                IsFree = Convert.ToBoolean(i % 2),
                IsLegacy = !Convert.ToBoolean(i % 2)
            }).ToList();
        
        await UnitOfWork.CarRepository.InsertRangeAsync(cars);

        var joins = cars.SelectMany(_ => classes,
            (car, @class) => new CarClassJoin { CarId = car.Id, ClassId = @class.Id });
        
        await U
        
        return cars;
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
        return Task.FromResult(entity with
        {
            Name = "New Car Class", 
            IsFree  = entity.IsLegacy, 
            IsLegacy = entity.IsFree
        });
    }
}