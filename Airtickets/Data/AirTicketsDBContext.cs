using Airtickets.Entities;
using Airtickets.Models;
using Npgsql;

namespace Airtickets.Data;

public class AirTicketsDBContext : DbContext
{
    public DbSet<Segment> Segments { get; set; }

    public AirTicketsDBContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Segment>()
            .HasKey(p => new { p.TicketNumber, p.FlightNum });
        modelBuilder.Entity<Segment>()
            .HasIndex(p => new { p.TicketNumber, p.SerialNumber })
            .IsUnique();
        modelBuilder.HasPostgresEnum<SegmentStatus>();
    }

    static AirTicketsDBContext() => NpgsqlConnection.GlobalTypeMapper.MapEnum<SegmentStatus>();
}