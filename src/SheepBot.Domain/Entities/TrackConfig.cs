using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Microsoft.EntityFrameworkCore;
using SheepBot.Domain.Base;
using SheepBot.Domain.Enums;

namespace SheepBot.Domain.Entities;

[Table("track_config")]
[Index("TrackId", Name = "Index_track_config_trackid", IsUnique = false, AllDescending = false)]
[Index("ConfigId", Name = "Index_track_config_configid", IsUnique = true, AllDescending = false)]
public record TrackConfig : ModelBase
{
    [Column("track_id")]
    public int TrackId { get; init; }
    
    [Column("config_id")]
    public int ConfigId { get; init; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; init; } = null!;

    [Column("corners")]
    public int Corners { get; init; }

    [Column("length")]
    public double Length { get; init; }
    
    [Column("is_dirt")]
    public bool IsDirt { get; init; }

    [Column("is_oval")]
    public bool IsOval { get; init; }
    
    [Column("max_cars")]
    public byte MaxCars { get; init; }
    
    [Column("pit_speed_limit")]
    public byte PitSpeedLimit { get; init; }
    
    [Column("track_type")]
    public TrackType TrackType { get; init; }
    
    [ForeignKey("TrackId")]
    [InverseProperty("TrackConfigs")]
    public virtual Track Track { get; set; } = null!;
    
    public static DataTable CreateDataTable(IEnumerable<TrackConfig> tracks)
    {
        var table = new DataTable();
        table.Columns.Add("track_id", typeof(int));
        table.Columns.Add("config_id", typeof(int));
        table.Columns.Add("name", typeof(string));
        table.Columns.Add("corners", typeof(short));
        table.Columns.Add("length", typeof(double));
        table.Columns.Add("is_dirt", typeof(bool));
        table.Columns.Add("is_oval", typeof(bool));
        table.Columns.Add("max_cars", typeof(short));
        table.Columns.Add("pit_speed_limit", typeof(short));
        table.Columns.Add("track_type", typeof(short));

        foreach (var track in tracks)
        {
            table.Rows.Add(
                track.TrackId,
                track.ConfigId,
                track.Name,
                track.Corners,
                track.Length,
                track.IsDirt,
                track.IsOval,
                track.MaxCars,
                track.PitSpeedLimit,
                track.TrackType);
        }
        
        return table;
    }
}