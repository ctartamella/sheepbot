using System.Data;

namespace SheepBot.Application.Interfaces;

public interface IConnectionFactory
{
    string ConnectionString { get; }
    IDbConnection GetConnection();
}