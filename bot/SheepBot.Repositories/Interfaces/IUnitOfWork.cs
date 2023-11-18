namespace SheepBot.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICarClassRepository CarClassRepository { get; }
    ICarRepository CarRepository { get; }
    ICarCarClassRepository CarCarClassRepository { get; }
    IRaceRepository RaceRepository { get; }
    IRoleRepository RoleRepository { get; }
    ISeriesRepository SeriesRepository { get; }
    ITrackRepository TrackRepository { get; }

    void Commit();
    void Rollback();
    Task CommitAsync();
    Task RollbackAsync();
}