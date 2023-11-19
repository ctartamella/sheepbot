using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using SheepBot.Models;

namespace SheepBot.Repositories.Helpers;

public static class MappingExtensions
{

    public static DataTable PopulateTable(this IEnumerable<CarCarClass> entities)
    {
        var table = new DataTable();
        table.TableName = GetTableName<CarCarClass>();
        table.Columns.Add("car_id", typeof(int));
        table.Columns.Add("class_id", typeof(int));

        foreach (var entity in entities)
        {
            var row = table.NewRow();
            row["car_id"] = entity.CarId;
            row["class_id"] = entity.ClassId;

            table.Rows.Add(row);
        }

        return table;
    }
    
    public static DataTable PopulateTable(this IEnumerable<CarClass> entities)
    {
        var table = new DataTable();
        table.TableName = GetTableName<CarClass>();
        table.Columns.Add("name", typeof(string));
        
        foreach (var carClass in entities)
        {
            var row = table.NewRow();
            row["name"] = carClass.Name;

            table.Rows.Add(row);
        }

        return table;
    }
    
    public static DataTable PopulateTable(this IEnumerable<Car> entities)
    {
        var table = new DataTable();
        table.TableName = GetTableName<Car>();
        table.Columns.Add(nameof(Car.Name), typeof(string));
        table.Columns.Add(nameof(Car.IsFree), typeof(bool));
        table.Columns.Add(nameof(Car.IsLegacy), typeof(bool));
        
        foreach (var car in entities)
        {
            var row = table.NewRow();
            row[nameof(Car.Name)] = car.Name;
            row[nameof(Car.IsFree)] =  car.IsFree;
            row[nameof(Car.IsLegacy)] = car.IsLegacy;

            table.Rows.Add(row);
        }

        return table;
    }

    public static DataTable PopulateTable(this IEnumerable<Race> entities)
    {
        var table = new DataTable();
        table.TableName = GetTableName<Race>();
        table.Columns.Add(nameof(Race.SeriesId), typeof(int));
        table.Columns.Add(nameof(Race.TrackId), typeof(int));
        table.Columns.Add(nameof(Race.PracticeTime), typeof(DateTimeOffset));
        table.Columns.Add(nameof(Race.QualiTime), typeof(DateTimeOffset));
        table.Columns.Add(nameof(Race.Length), typeof(int));
        table.Columns.Add(nameof(Race.Unit), typeof(int));
        
        foreach (var car in entities)
        {
            var row = table.NewRow();
            row[nameof(Race.SeriesId)] = car.SeriesId;
            row[nameof(Race.TrackId)] =  car.TrackId;
            row[nameof(Race.PracticeTime)] = car.PracticeTime;
            row[nameof(Race.QualiTime)] = car.QualiTime;
            row[nameof(Race.Length)] =  car.Length;
            row[nameof(Race.Unit)] = car.Unit;

            table.Rows.Add(row);
        }

        return table;
    }

    public static DataTable PopulateTable(this IEnumerable<Role> entities)
    {
        var table = new DataTable();
        table.TableName = GetTableName<Role>();
        table.Columns.Add(nameof(Role.DiscordId), typeof(long));
        table.Columns.Add(nameof(Role.RoleName), typeof(string));
        
        foreach (var role in entities)
        {
            var row = table.NewRow();
            row[nameof(Role.DiscordId)] = role.DiscordId;
            row[nameof(Role.RoleName)] =  role.RoleName;

            table.Rows.Add(row);
        }

        return table;
    }

    public static DataTable PopulateTable(this IEnumerable<Series> entities)
    {
        var table = new DataTable();
        table.TableName = GetTableName<Series>();
        table.Columns.Add(nameof(Series.Name), typeof(string));
        table.Columns.Add(nameof(Series.RoleId), typeof(int));
        table.Columns.Add(nameof(Series.Type), typeof(int));
        table.Columns.Add(nameof(Series.DiscordServer), typeof(string));
        table.Columns.Add(nameof(Series.IracingUrl), typeof(string));
        table.Columns.Add(nameof(Series.Website), typeof(string));        

        foreach (var series in entities)
        {
            var row = table.NewRow();
            row[nameof(Series.Name)] = series.Name;
            row[nameof(Series.RoleId)] =  series.RoleId;
            row[nameof(Series.Type)] = series.Type;
            row[nameof(Series.DiscordServer)] = series.DiscordServer;
            row[nameof(Series.IracingUrl)] = series.IracingUrl;
            row[nameof(Series.Website)] = series.Website;
            
            table.Rows.Add(row);
        }

        return table;
    }
    
    public static DataTable PopulateTable(this IEnumerable<Track> entities)
    {
        var table = new DataTable();
        table.TableName = GetTableName<Track>();
        table.Columns.Add(nameof(Track.Name), typeof(string));
        table.Columns.Add(nameof(Track.IsFree), typeof(bool));
        table.Columns.Add(nameof(Track.IsLegacy), typeof(bool));
        
        foreach (var entity in entities)
        {
            var row = table.NewRow();
            row[nameof(Track.Name)] = entity.Name;
            row[nameof(Track.IsFree)] =  entity.IsFree;
            row[nameof(Track.IsLegacy)] = entity.IsLegacy;

            table.Rows.Add(row);
        }

        return table;
    }
    
    private static string GetTableName<T>()
    {
        var attr = typeof(T)
            .GetCustomAttributes(typeof(TableAttribute), true)
            .FirstOrDefault() as TableAttribute;

        return attr?.Name ?? throw new InvalidProgramException("Model types must be decorated with the Table attribute");
    }
}