using System.Data;
using Dapper;
using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Helpers;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
    public RoleRepository(SqlTransaction transaction) : base(transaction) { }

    public override async Task<IEnumerable<Role>> GetAllAsync()
    {
        var result = await Transaction
            .QueryAsync<Role>(GetAllQuery)
            .ConfigureAwait(false);
        
        return result ?? new List<Role>();
    }

    public override async Task<Role?> FindAsync(int id)
    {
        var parameters = new { id };

        var role = await Transaction
            .QueryAsync<Role>(FindQuery, parameters)
            .ConfigureAwait(false);
        
        return role.SingleOrDefault();
    }

    public override async Task<int> InsertAsync(Role entity)
    {
        var carParameters = new DynamicParameters();
        carParameters.Add("@DiscordId", entity.DiscordId);
        carParameters.Add("@RoleName", entity.RoleName);
        carParameters.Add("@Id", null, DbType.Int32, direction: ParameterDirection.Output);

        await Transaction.ExecuteAsync(InsertQuery, carParameters).ConfigureAwait(false);

        return carParameters.Get<int>("@Id");
    }

    public override async Task<int> InsertRangeAsync(IEnumerable<Role> entities)
    {
        var table = entities.PopulateTable();
        
        using var bulkInsert = new SqlBulkCopy(Transaction.Connection, SqlBulkCopyOptions.Default, Transaction);
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Role.DiscordId), "discord_id"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Role.RoleName), "role_name"));
        bulkInsert.DestinationTableName = table.TableName;
        
        await bulkInsert.WriteToServerAsync(table).ConfigureAwait(false);
        return bulkInsert.RowsCopied;
    }

    public override async Task<int> UpdateAsync(Role entity)
    {
        var parameters = new { entity.Id, entity.DiscordId, entity.RoleName };
        return await Transaction.ExecuteAsync(UpdateQuery, parameters).ConfigureAwait(false);
    }
    
    public override async Task<int> DeleteAsync(int id)
    {
        var parameters = new { id };
        return await Transaction.ExecuteAsync(DeleteQuery, parameters).ConfigureAwait(false);
    }
    
    private const string GetAllQuery = """
                                       SELECT id, 
                                              discord_id AS DiscordId, 
                                              role_name AS RoleName
                                       FROM [dbo].[role] WITH (NOLOCK)
                                       """;
    
    private const string FindQuery = """
                                     SELECT id, 
                                            discord_id AS DiscordId, 
                                            role_name AS RoleName
                                     FROM [dbo].[role] WITH (NOLOCK)
                                     WHERE role.id=@Id
                                     """;
    
    private const string InsertQuery = """
                                       INSERT INTO [dbo].[role] (discord_id, role_name)
                                          VALUES (@DiscordId, @RoleName);
                                          SELECT @Id = SCOPE_IDENTITY()
                                       """;
    
    private const string UpdateQuery = """
                                       UPDATE [dbo].[role]
                                       SET discord_id=@DiscordId, role_name=@RoleName
                                       WHERE id=@Id
                                       """;
    
    private const string DeleteQuery = "DELETE FROM [dbo].[role] WHERE id=@id";
}