using SheepBot.Models;
using SheepBot.Repositories.Tests.Helpers;

namespace SheepBot.Repositories.Tests;

public class CarClassRepositoryTests : DatabaseTestBase
{
    [Fact]
    public void InsertRange()
    {
        // Arrange
        var size = NextRandom();
        var expected = Enumerable.Range(1, size)
            .Select(i => new CarClass
            {
                Name = $"Test Class {i}"
            }).ToList();
        
        // Act
        var numRows = UnitOfWork.CarClassRepository.InsertRange(expected);
        
        // Assert
        var records = UnitOfWork.CarClassRepository.GetAll().ToList();

        Assert.Equal(size, numRows);
        Assert.Equal(size, records.Count); 
        Assert.Contains(expected.Zip(records), CarClassComparison);
    }
    
    private static bool CarClassComparison((CarClass first, CarClass second) tuple) => !tuple.first.Name.Equals(tuple.second.Name);
}