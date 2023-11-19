using SheepBot.Models;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories.Tests.Base;

public abstract class ModelTestBase<TModel> : RollbackTestBase
    where TModel : ModelBase, IEquatable<TModel>
{
    protected readonly IRepositoryBase<TModel> Repository;
    protected ModelTestBase(Func<IUnitOfWork, IRepositoryBase<TModel>> repositoryDelegate)
    {
        Repository = repositoryDelegate(UnitOfWork);
    }
    
    [Fact]
    public virtual async Task GetEmptyTable()
    {
        var results = await Repository.GetAllAsync();
        Assert.NotNull(results);
        Assert.Empty(results);
    }

    [Fact]
    public virtual async Task GetSingleResult()
    {
        // Arrange
        var expected = CreateEntity();
        await Repository.InsertAsync(expected);
        
        // Act
        var actual = await Repository.FindAsync(expected.Id);
        
        // Assert
        Assert.NotNull(actual);
        Assert.True(actual.Id > 0);
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public async Task InsertSingle()
    {
        // Arrange
        var expected = CreateEntity();

        // Act
        var id = await Repository.InsertAsync(expected);
        
        // Assert
        var actual = await Repository.FindAsync(id);
        
        Assert.True(id > 0);
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public async Task InsertRange()
    {
        // Arrange
        var size = NextRandom();
        var expected = CreateEntityList(size).ToList();
        
        // Act
        var numRows = await Repository.InsertRangeAsync(expected);
        
        // Assert
        var records = (await Repository.GetAllAsync()).ToList();

        Assert.Equal(size, numRows);
        Assert.Equal(size, records.Count); 
        Assert.DoesNotContain(expected.Zip(records), ModelsAreDifferent);
    }
    
    [Fact]
    public async Task DeleteIsSuccessfulReturnsTrue()
    {
        var expected = CreateEntity();

        // Arrange
        var id = await Repository.InsertAsync(expected);
        var actual = await Repository.FindAsync(id);
        
        // Act
        var numDeleted = await Repository.DeleteAsync(id);
        var actualAfterDelete = await Repository.FindAsync(id);
        
        // Assert
        Assert.NotNull(actual);
        Assert.True(numDeleted == 1);
        Assert.Null(actualAfterDelete);
    }
    
    protected abstract IEnumerable<TModel> CreateEntityList(int dataSize);
    protected abstract TModel CreateEntity();

    private static Predicate<(TModel First, TModel Second)> ModelsAreDifferent 
        => tuple => !tuple.First.Equals(tuple.Second);
}