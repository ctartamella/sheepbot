using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
    public RoleRepository(SqlTransaction transaction) : base(transaction) { }
    
    public override async Task<IEnumerable<Role>> GetAllAsync()
    {
        const string query = "SELECT * FROM [dbo].[role]";

        var result = await Transaction.QueryAsync<Role>(query).ConfigureAwait(false);
        
        return result ?? new List<Role>();
    }
    
    public override async Task<Role?> FindAsync(int id)
    {
        const string query = "SELECT * FROM [dbo].[role] WHERE id=@id";
        var parameters = new { id };

        var result = await Transaction.QueryAsync<Role>(query, parameters).ConfigureAwait(false);
        
        return result.SingleOrDefault();
    }

    public override Task<int> InsertAsync(Role entity)
    {
        throw new NotImplementedException();
    }

    public override Task<int> InsertRangeAsync(IEnumerable<Role> entities)
    {
        throw new NotImplementedException();
    }

    public override Task<bool> UpdateAsync(Role entity)
    {
        throw new NotImplementedException();
    }

    public override Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}