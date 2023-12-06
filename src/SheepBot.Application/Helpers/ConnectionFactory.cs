using System.Data;
using Microsoft.Data.SqlClient;
using SheepBot.Application.Interfaces;

namespace SheepBot.Application.Helpers;

public class ConnectionFactory : IConnectionFactory
{
    public ConnectionFactory(string connectionString)
    {
        ConnectionString = connectionString;
    }
    
    public string ConnectionString { get; }

    public IDbConnection GetConnection()
    {
        return new SqlConnection(ConnectionString);
    }
}