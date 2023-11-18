using Microsoft.Data.SqlClient;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly SqlConnection _connection;
    
    private SqlTransaction _transaction;
    private ICarClassRepository? _carClassRepository;
    private ICarCarClassRepository? _carCarClassRepository;
    private ICarRepository? _carRepository;
    private IRaceRepository? _raceRepository;
    private IRoleRepository? _roleRepository;
    private ISeriesRepository? _seriesRepository;
    private ITrackRepository? _trackRepository;
    
    public UnitOfWork(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
        _connection.Open();
        _transaction = _connection.BeginTransaction();
    }
    
    public ICarClassRepository CarClassRepository => _carClassRepository ??= new CarClassRepository(_transaction);
    public ICarRepository CarRepository => _carRepository ??= new CarRepository(_transaction);
    public ICarCarClassRepository CarCarClassRepository => _carCarClassRepository ??= new CarCarClassRepository(_transaction);
    public IRaceRepository RaceRepository => _raceRepository ??= new RaceRepository(_transaction);
    public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(_transaction);
    public ISeriesRepository SeriesRepository => _seriesRepository ??= new SeriesRepository(_transaction);
    public ITrackRepository TrackRepository => _trackRepository ??= new TrackRepository(_transaction);
    
    public void Commit()
    {
        try
        {
            _transaction.Commit();
        }
        catch
        {
            _transaction.Rollback();
            throw;
        }
        finally
        {
            _transaction.Dispose();
            _transaction = _connection.BeginTransaction();
            ClearRepositories();
        }
    }
    
    public async Task CommitAsync()
    {
        try
        {
            await _transaction.CommitAsync().ConfigureAwait(false);
        }
        catch
        {
            _transaction.Rollback();
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync().ConfigureAwait(false);
            
            _transaction = _connection.BeginTransaction();
            ClearRepositories();
        }
    }

    public void Rollback()
    {
        try
        {
            _transaction.Rollback();
        }
        finally
        {
            _transaction.Dispose();
            _transaction = _connection.BeginTransaction();
            ClearRepositories();
        }
    }
    
    public async Task RollbackAsync()
    {
        try
        {
            await _transaction.RollbackAsync().ConfigureAwait(false);
        }
        finally
        {
            await _transaction.DisposeAsync().ConfigureAwait(false);
            
            _transaction = _connection.BeginTransaction();
            ClearRepositories();
        }
    }

    public void Dispose()
    {
        _transaction.Dispose();
        _connection.Dispose();
    }
    
    private void ClearRepositories()
    {
        _carClassRepository = null;
        _carRepository = null;
        _raceRepository = null;
        _roleRepository = null;
        _seriesRepository = null;
        _trackRepository = null;
    }
}