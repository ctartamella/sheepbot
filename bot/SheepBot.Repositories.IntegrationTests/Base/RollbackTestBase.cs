using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories.Tests.Base;

public abstract class RollbackTestBase : IDisposable
{
    private static readonly Random Random = new();
    private const int MinSize = 20;
    private const int MaxSize = 100;
    
    protected static int NextRandom (int minSize = MinSize, int maxSize = MaxSize) => Random.Next(minSize, maxSize);
    
    private readonly string _connectionString = Environment.GetEnvironmentVariable("INTEGRATION_DB") ??
                           throw new InvalidProgramException("You must define the INTEGRATION_DB environment variable");


    protected RollbackTestBase()
    { 
        UnitOfWork = new UnitOfWork(_connectionString);
    }

    protected IUnitOfWork UnitOfWork { get; }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing) return;
        
        UnitOfWork.Rollback();
        UnitOfWork.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

