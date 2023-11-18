using System.Data;
using Dapper;
using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
    public RoleRepository(SqlTransaction transaction) : base(transaction) { }
    
    public override IEnumerable<Role> GetAll()
    {
        const string query = "SELECT * FROM [dbo].[role]";

        return Transaction.Query<Role>(query) ?? new List<Role>();
    }
    
    public override Role? Find(int id)
    {
        const string query = "SELECT * FROM [dbo].[role] WHERE id=@id";
        var parameters = new { id = id };

        return Transaction.Query<Role>(query, parameters).SingleOrDefault();
    }

    public override int Insert(Role entity)
    {
        throw new NotImplementedException();
    }

    public override int InsertRange(IEnumerable<Role> entities)
    {
        throw new NotImplementedException();
    }

    public override bool Update(Role entity)
    {
        throw new NotImplementedException();
    }

    public override int Delete(int id)
    {
        throw new NotImplementedException();
    }
}