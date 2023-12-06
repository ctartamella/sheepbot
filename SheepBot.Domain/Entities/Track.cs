using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using SheepBot.Domain.Base;

namespace SheepBot.Domain.Entities;

[Table("track")]
[Index("Name", Name = "Index_track_name", IsUnique = true)]
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public record Track : ModelBase
{
    [Column("iracing_id")]
    public int IracingId { get; init; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; init; } = null!;

    [Column("is_free")]
    public bool IsFree { get; init; }

    [Column("is_legacy")]
    public bool IsLegacy { get; init; }

    [Column("ai_enabled")]
    public bool AiEnabled { get; init; }

    [Column("fully_lit")]
    public bool FullyLit { get; init; }

    [Column("location")]
    [StringLength(256)]
    public string? Location { get; init; }

    [Column("geo_location")]
    public Point GeoLocation { get; init; } = null!;

    [Column("price")]
    [StringLength(50)]
    public string? Price { get; init; }

    [Column("is_purchasable")]
    public bool IsPurchasable { get; init; }

    [Column("is_retired")]
    public bool IsRetired { get; init; }

    [InverseProperty("Track")]
    public virtual ICollection<Race> Races { get; set; } = new List<Race>();

    public virtual ICollection<TrackConfig> TrackConfigs { get; set; } = new List<TrackConfig>();
    
    public static DataTable CreateDataTable(IEnumerable<Track> tracks)
    {
        var table = new DataTable();
        table.Columns.Add("iracing_id", typeof(int));
        table.Columns.Add("name", typeof(string));
        table.Columns.Add("is_free", typeof(bool));
        table.Columns.Add("is_legacy", typeof(bool));
        table.Columns.Add("ai_enabled", typeof(bool));
        table.Columns.Add("fully_lit", typeof(bool));
        table.Columns.Add("location", typeof(string));
        table.Columns.Add("geo_location", typeof(string));
        table.Columns.Add("price", typeof(string));
        table.Columns.Add("is_purchasable", typeof(bool));
        table.Columns.Add("is_retired", typeof(bool));

        foreach (var track in tracks)
        {
            table.Rows.Add(
                track.IracingId,
                track.Name,
                track.IsFree,
                track.IsLegacy,
                track.AiEnabled,
                track.FullyLit,
                track.Location,
                $"POINT({track.GeoLocation.X} {track.GeoLocation.Y} 4326)",
                track.Price,
                track.IsPurchasable,
                track.IsRetired);
        }

        return table;
    }
}
