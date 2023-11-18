using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Tests.Helpers;

namespace SheepBot.Repositories.Tests;

public sealed class CarRepositoryTests : DatabaseTestBase
{
    [Fact]
    public void GetEmptyTable()
    {
        var results = UnitOfWork.CarRepository.GetAll();

        Assert.NotNull(results);
        Assert.Empty(results);
    }

    [Fact]
    public void GetSingleResultWithChildren()
    {
        // Arrange
        var classSize = NextRandom(10, 50);
        var classes = Enumerable.Range(1, classSize)
            .Select(i => new CarClass { Name = $"Class {i}" })
            .ToArray();

        var carSize = NextRandom();
        var expected = Enumerable.Range(1, carSize)
            .Select(i => new Car
            {
                Name = $"Test Car {i}",
                IsFree = Convert.ToBoolean(i % 2),
                IsLegacy = !Convert.ToBoolean(i % 2),
                Classes =
                {
                    classes[i % classSize],
                    classes[(i+1) % classSize],
                    classes[(i+2) % classSize]
                }
            }).ToList();
        
        UnitOfWork.CarClassRepository.InsertRange(classes);
        var actualClasses = UnitOfWork.CarClassRepository.GetAll();
        
        UnitOfWork.CarRepository.InsertRange(expected);
        
        // Act
        var actual = UnitOfWork.CarRepository.GetAll().ToList();
        
        // Assert
        Assert.Equal(carSize, actual.Count);
        Assert.Contains(expected.Zip(actual), CarsAreDifferent);
        Assert.Contains(FlattenClasses(actual).Zip(FlattenClasses(expected)), CarClassComparison);
        Assert.DoesNotContain(actual, car => car.Id <= 0);
        Assert.DoesNotContain(FlattenClasses(actual), carClass => carClass.Id <= 0);
    }

    [Fact]
    public void ValidatePricingConstraintBothTrue()
    {
        // Arrange
        var expected = new Car
        {
            Name = $"Test Car",
            IsFree = true,
            IsLegacy = true
        };
        
        // Act/Assert
        Assert.Throws<SqlException>(() => UnitOfWork.CarRepository.Insert(expected));
    }

    [Fact]
    public void InsertSingle()
    {
        // Arrange
        var expected = new Car
        {
            Name = $"Test Car",
            IsFree = true,
            IsLegacy = false
        };

        // Act
        var id = UnitOfWork.CarRepository.Insert(expected);
        var actual = UnitOfWork.CarRepository.Find(id);
        
        // Assert
        Assert.NotNull(actual);
        Assert.True(CarsAreDifferent((expected, actual)));
    }

    [Fact]
    public void InsertRange()
    {
        // Arrange
        var size = NextRandom();
        var expected = Enumerable.Range(1, size)
            .Select(i => new Car
            {
                Name = $"Test Car {i}",
                IsFree = Convert.ToBoolean(i % 2),
                IsLegacy = !Convert.ToBoolean(i % 2)
            }).ToList();
        
        // Act
        var numRows = UnitOfWork.CarRepository.InsertRange(expected);
        
        // Assert
        var records = UnitOfWork.CarRepository.GetAll().ToList();

        Assert.Equal(size, numRows);
        Assert.Equal(size, records.Count); 
        Assert.DoesNotContain(expected.Zip(records), CarsAreDifferent);
    }

    [Fact]
    public void DeleteIsSuccessfulReturnsTrue()
    {
        var record = new Car
        {
            Name = "Test",
            IsLegacy = false,
            IsFree = false
        };

        // Arrange
        var id = UnitOfWork.CarRepository.Insert(record);
        var actual = UnitOfWork.CarRepository.Find(id);
        
        // Act
        var numDeleted = UnitOfWork.CarRepository.Delete(id);
        var actualAfterDelete = UnitOfWork.CarRepository.Find(id);
        
        // Assert
        Assert.NotNull(actual);
        Assert.True(numDeleted == 1);
        Assert.Null(actualAfterDelete);
    }
    
    private static IEnumerable<CarClass> FlattenClasses(IEnumerable<Car> cars) => cars.SelectMany(car => car.Classes);
    
    private static bool CarsAreDifferent((Car first, Car second) tuple) => 
        !tuple.first.Name.Equals(tuple.second.Name) 
        || tuple.first.IsFree != tuple.second.IsFree 
        || tuple.first.IsLegacy != tuple.second.IsLegacy;

    private static bool CarClassComparison((CarClass first, CarClass second) tuple) => !tuple.first.Name.Equals(tuple.second.Name);
}