using SheepBot.Models.Interfaces;
using SheepBot.Repositories.Interfaces;
using SheepBot.Repositories.Tests.Helpers;

namespace SheepBot.Repositories.Tests.Base;

public abstract class ModelTestBase<TModel> : RollbackTestBase
    where TModel : IModelBase, IEquatable<TModel>
{
    protected readonly IRepositoryBase<TModel> Repository;
    protected ModelTestBase(Func<IUnitOfWork, IRepositoryBase<TModel>> repositoryDelegate)
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
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
    public virtual async Task GetAllResult()
    {
        // Arrange
        var expected = await ArrangeArrayData();
        var numInserted = await Repository.InsertRangeAsync(expected);

        // Act
        var actual = await Repository.GetAllAsync();
        
        // Assert
        
    }
    
    //
    // [Fact]
    // public virtual async Task GetSingleResult()
    // {
    //     // Arrange
    //     var expected = await CreateEntityAsync();
    //     var id = await Repository.InsertAsync(expected);
    //     
    //     // Act
    //     var actual = await Repository.FindAsync(id);
    //     
    //     // Assert
    //     Assert.NotNull(actual);
    //     Assert.True(id > 0);
    //     Assert.Equal(id, actual.Id);
    //     Assert.Equal(expected, actual);
    // }
    //
    // [Fact]
    // public async Task InsertSingle()
    // {
    //     // Arrange
    //     var expected = await CreateEntityAsync();
    //
    //     // Act
    //     var id = await Repository.InsertAsync(expected);
    //     
    //     // Assert
    //     var actual = await Repository.FindAsync(id);
    //     
    //     Assert.NotNull(actual);
    //     Assert.True(id > 0);
    //     Assert.Equal(id, actual.Id);
    //     Assert.Equal(expected, actual);
    // }
    //
    // [Fact]
    // public async Task InsertRange()
    // {
    //     // Arrange
    //     var expected = (await CreateEntityListAsync()).ToList();
    //     
    //     // Act
    //     var numRows = await Repository.InsertRangeAsync(expected);
    //     
    //     // Assert
    //     var actual = (await Repository.GetAllAsync()).ToList();
    //
    //     Assert.Equal(expected.Count, numRows);
    //     Assert.Equal(expected.Count, actual.Count);
    //     Assert.DoesNotContain(actual, m => m.Id <= 0);
    //     Assert.True(expected.CompareUnordered(actual, m => m.Id));
    // }
    //
    // [Fact]
    // public async Task UpdateRecordSucceeds()
    // {
    //     // Arrange
    //     var expected = await CreateEntityAsync();
    //     var id = await Repository.InsertAsync(expected);
    //     var original = await Repository.FindAsync(id);
    //     Assert.NotNull(original);
    //     
    //     var updated = await UpdateEntity(original);
    //     
    //     // Act
    //     var updatedRecords = await Repository.UpdateAsync(updated);
    //     var actual = await Repository.FindAsync(id);
    //     
    //     // Assert
    //     Assert.True(id > 0);
    //     Assert.True(updatedRecords == 1);
    //     Assert.NotNull(actual);
    //     Assert.Equal(id, actual.Id);
    //     Assert.Equal(updated, actual);
    // }
    //
    // [Fact]
    // public async Task DeleteIsSuccessfulReturnsTrue()
    // {
    //     // Arrange
    //     var expected = await CreateEntityAsync();
    //     var id = await Repository.InsertAsync(expected);
    //     var actual = await Repository.FindAsync(id);
    //     
    //     // Act
    //     var numDeleted = await Repository.DeleteAsync(id);
    //     var actualAfterDelete = await Repository.FindAsync(id);
    //     
    //     // Assert
    //     Assert.True(id > 0);
    //     Assert.NotNull(actual);
    //     Assert.True(numDeleted == 1);
    //     Assert.Null(actualAfterDelete);
    // }
    //
    // [Fact]
    // public async Task DeleteFailsReturnsFalse()
    // {
    //     // Act
    //     var numDeleted = await Repository.DeleteAsync(1);
    //     
    //     // Assert
    //     Assert.True(numDeleted == 0);
    // }
    //
    // [Fact]
    // public async Task DeleteByModelIsSuccessful()
    // {
    //     // Arrange
    //     var expected = await CreateEntityAsync();
    //     var id = await Repository.InsertAsync(expected);
    //     var actual = await Repository.FindAsync(id);
    //     Assert.NotNull(actual);
    //     
    //     // Act
    //     var numDeleted = await Repository.DeleteAsync(actual);
    //     var actualAfterDelete = await Repository.FindAsync(id);
    //     
    //     // Assert
    //     Assert.True(id > 0);
    //     Assert.True(numDeleted == 1);
    //     Assert.Null(actualAfterDelete);
    // }
    
    protected abstract Task<IEnumerable<TModel>> ArrangeArrayData();
    protected abstract Task<TModel> CreateEntityAsync();
    protected abstract Task<TModel> UpdateEntity(TModel entity);
}