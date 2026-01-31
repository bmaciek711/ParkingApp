using Microsoft.EntityFrameworkCore;
using ParkingApp.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ParkingApp.Infrastructure.Data;

public sealed class ParkingDbContext : IdentityDbContext<IdentityUser>
{
    public ParkingDbContext(DbContextOptions<ParkingDbContext> options) : base(options) { }

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<Motorcycle> Motorcycles => Set<Motorcycle>();
    public DbSet<Bicycle> Bicycles => Set<Bicycle>();


    public DbSet<ParkingSpot> ParkingSpots => Set<ParkingSpot>();
    public DbSet<Reservation> Reservations => Set<Reservation>(); 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

   
        modelBuilder.Entity<Vehicle>()
            .HasDiscriminator<string>("VehicleType")
            .HasValue<Car>("Car")
            .HasValue<Motorcycle>("Motorcycle")
            .HasValue<Bicycle>("Bicycle");

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasOne(r => r.ParkingSpot)
                  .WithMany() 
                  .HasForeignKey(r => r.ParkingSpotId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}