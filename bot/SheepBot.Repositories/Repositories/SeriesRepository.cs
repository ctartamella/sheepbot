using System.Data;
using Dapper;
using Dapper.Transaction;
using Microsoft.Data.SqlClient;
using SheepBot.Models;
using SheepBot.Repositories.Helpers;
using SheepBot.Repositories.Interfaces;

namespace SheepBot.Repositories;

public class SeriesRepository : RepositoryBase<Series>, ISeriesRepository
{
    public SeriesRepository(SqlTransaction transaction) : base(transaction) { }

    public override async Task<IEnumerable<Series>> GetAllAsync()
    {
        var result = await Transaction
            .QueryAsync<Series>(GetAllQuery)
            .ConfigureAwait(false);
        
        return result ?? new List<Series>();
    }

    public override async Task<Series?> FindAsync(long id)
    {
        var parameters = new { id };

        var series = await Transaction
            .QueryAsync<Series>(FindQuery, parameters)
            .ConfigureAwait(false);
        
        return series.SingleOrDefault();
    }

    public override async Task<long> InsertAsync(Series entity)
    {
        var carParameters = new DynamicParameters();
        carParameters.Add("@Name", entity.Name);
        carParameters.Add("@RoleId", entity.RoleId);
        carParameters.Add("@Type", entity.Type);
        carParameters.Add("@DiscordServer", entity.DiscordServer);
        carParameters.Add("@IracingUrl", entity.IracingUrl);
        carParameters.Add("@Website", entity.Website);
        carParameters.Add("@Id", null, DbType.Int32, direction: ParameterDirection.Output);
        
        await Transaction.ExecuteAsync(InsertQuery, carParameters).ConfigureAwait(false);

        return carParameters.Get<int>("@Id");
    }

    public override async Task<long> InsertRangeAsync(IEnumerable<Series> entities)
    {
        var table = entities.PopulateTable();
        
        using var bulkInsert = new SqlBulkCopy(Transaction.Connection, SqlBulkCopyOptions.Default, Transaction);
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Series.Name), "name"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Series.RoleId), "role_id"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Series.Type), "series_type_id"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Series.DiscordServer), "discord_server"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Series.IracingUrl), "iracing_url"));
        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping(nameof(Series.Website), "website"));
        bulkInsert.DestinationTableName = table.TableName;
        await bulkInsert.WriteToServerAsync(table).ConfigureAwait(false);
        
        return bulkInsert.RowsCopied;
    }

    public override async Task<int> UpdateAsync(Series entity)
    {
        var parameters = new
        {
            entity.Id, 
            entity.Name, 
            entity.Type, 
            entity.RoleId, 
            entity.DiscordServer, 
            entity.IracingUrl, 
            entity.Website
        };
        
        return await Transaction.ExecuteAsync(UpdateQuery, parameters).ConfigureAwait(false);
    }

    public override async Task<int> DeleteAsync(long id)
    {
        var parameters = new { id };
        return await Transaction.ExecuteAsync(DeleteQuery, parameters).ConfigureAwait(false);
    }
    
    private const string GetAllQuery = """
                                       SELECT id, 
                                              name, 
                                              role_id AS RoleId, 
                                              series_type_id AS Type, 
                                              discord_server AS DiscordServer, 
                                              iracing_url AS IracingUrl, 
                                              website
                                       FROM [dbo].[series] WITH (NOLOCK)
                                       """;
    
    private const string FindQuery = """
                                     SELECT id, 
                                            name, 
                                            role_id AS RoleId, 
                                            series_type_id AS Type, 
                                            discord_server AS DiscordServer, 
                                            iracing_url AS IracingUrl, 
                                            website
                                     FROM [dbo].[series] WITH (NOLOCK)
                                     WHERE id=@Id
                                     """;
    
    private const string InsertQuery = """
                                       INSERT INTO [dbo].[series] (name, role_id, series_type_id, discord_server, iracing_url, website)
                                          VALUES (@Name, @RoleId, @Type, @DiscordServer, @IracingUrl, @Website);
                                          SELECT @Id = SCOPE_IDENTITY()
                                       """;
    
    private const string UpdateQuery = """
                                       UPDATE [dbo].[series]
                                       SET name=@Name, 
                                           role_id=@RoleId, 
                                           series_type_id=@Type, 
                                           discord_server=@DiscordServer, 
                                           iracing_url=@IracingUrl, 
                                           website=@Website
                                       WHERE id=@Id
                                       """;
    
    private const string DeleteQuery = "DELETE FROM [dbo].[series] WHERE id=@Id";
}