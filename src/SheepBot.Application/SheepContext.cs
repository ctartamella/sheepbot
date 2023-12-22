using Microsoft.EntityFrameworkCore;
using SheepBot.Application.Interfaces;
using SheepBot.Domain.Entities;
using SheepBot.Domain.Enums;

namespace SheepBot.Infrastructure.Context;

public partial class SheepContext : DbContext
{
    public SheepContext()
    {
    }

    public SheepContext(DbContextOptions<SheepContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; } = null!;

    public virtual DbSet<CarClass> CarClasses { get; set; } = null!;

    public virtual DbSet<Class> Classes { get; set; } = null!;

    public virtual DbSet<Event> Events { get; set; } = null!;

    public virtual DbSet<EventClass> EventClasses { get; set; } = null!;

    public virtual DbSet<Race> Races { get; set; } = null!;

    public virtual DbSet<Role> Roles { get; set; } = null!;

    public virtual DbSet<SingletonEvent> SingletonEvents { get; set; } = null!;
    
    public virtual DbSet<Track> Tracks { get; set; } = null!;

    public virtual DbSet<TrackConfig> TrackConfigs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_cars");
        });

        modelBuilder.Entity<CarClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_class_cars");

            entity.HasOne(d => d.Car).WithMany(p => p.CarClasses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_class_cars_cars");

            entity.HasOne(d => d.Class).WithMany(p => p.CarClasses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_class_cars_class");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_series");

            entity.HasOne(d => d.Role).WithMany(p => p.Events)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_series_role");

            entity.HasMany(d => d.RacesNavigation).WithMany(p => p.Events)
                .UsingEntity<Dictionary<string, object>>(
                    "SeriesEvent",
                    r => r.HasOne<Race>().WithMany()
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_series_event_race"),
                    l => l.HasOne<Event>().WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_series_event_event"),
                    j =>
                    {
                        j.HasKey("EventId", "RaceId");
                        j.ToTable("series_event");
                        j.HasIndex(new[] { "RaceId" }, "Index_series_event_1");
                        j.IndexerProperty<long>("EventId").HasColumnName("event_id");
                        j.IndexerProperty<long>("RaceId").HasColumnName("race_id");
                    });
        });

        modelBuilder.Entity<EventClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_series_class");

            entity.HasOne(d => d.Class).WithMany(p => p.EventClasses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_series_class_class");

            entity.HasOne(d => d.Event).WithMany(p => p.EventClasses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_series_class_series");
        });

        modelBuilder.Entity<Race>(entity =>
        {
            entity.HasOne(d => d.Event).WithMany(p => p.Races)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_race_event");

            entity.HasOne(d => d.Track).WithMany(p => p.Races)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_race_track");
        });

        modelBuilder.Entity<SingletonEvent>(entity =>
        {
            entity.Property(e => e.EventId).ValueGeneratedNever();

            entity.HasOne(d => d.Event).WithOne(p => p.SingletonEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_singleton_event");

            entity.HasOne(d => d.Race).WithOne(p => p.SingletonEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_singleton_race");
        });
        
        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.TrackId, "Index_track_id")
                .IsUnique()
                .IsClustered();
        });

        modelBuilder.Entity<TrackConfig>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Track)
                .WithMany(p => p.TrackConfigs)
                .HasPrincipalKey(p => p.TrackId)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_track_config_track");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
