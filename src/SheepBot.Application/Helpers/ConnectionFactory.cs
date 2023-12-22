using System.Data;
using Microsoft.Data.SqlClient;
using SheepBot.Application.Interfaces;

namespace SheepBot.Application.Helpers;

public class ConnectionFactory(string connectionString) : IConnectionFactory
{
    public string ConnectionString { get; } = connectionString;

    public IDbConnection GetConnection()
    {
        return new SqlConnection(ConnectionString);
    }
}