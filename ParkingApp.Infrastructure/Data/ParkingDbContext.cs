using Microsoft.EntityFrameworkCore;
using ParkingApp.Core.Entities;


namespace ParkingApp.Infrastructure.Data;

public sealed class ParkingDbContext : DbContext
{
    public ParkingDbContext(DbContextOptions<ParkingDbContext> options) : base(options) { }

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<Motorcycle> Motorcycles => Set<Motorcycle>();
    public DbSet<Bicycle> Bicycles => Set<Bicycle>();

    public DbSet<ParkingSpot> ParkingSpots => Set<ParkingSpot>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Complaint> Complaints => Set<Complaint>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Dziedziczenie Vehicle -> TPH (jedna tabela z "VehicleType")
        modelBuilder.Entity<Vehicle>()
            .HasDiscriminator<string>("VehicleType")
            .HasValue<Car>("Car")
            .HasValue<Motorcycle>("Motorcycle")
            .HasValue<Bicycle>("Bicycle");
    }
}
