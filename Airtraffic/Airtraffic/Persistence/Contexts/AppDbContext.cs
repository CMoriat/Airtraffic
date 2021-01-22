using Airtraffic.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Airtraffic.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Airport>().ToTable("Airports");
            builder.Entity<Airport>().HasKey(p => p.Id);
            builder.Entity<Airport>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Airport>().Property(p => p.Name).IsRequired();
            builder.Entity<Airport>().Property(p => p.Longitude).IsRequired();
            builder.Entity<Airport>().Property(p => p.Latitude).IsRequired();
            builder.Entity<Airport>().HasMany(p => p.Aircrafts).WithOne(p => p.CurrentAirport).HasForeignKey(p => p.AirportId);

            builder.Entity<Airport>().HasData
                (
                new Airport { Id = 99, Name = "Charles de Gaulle, Paris", Longitude = 2.547778, Latitude = 49.009724 },
                new Airport { Id = 100, Name = "Copenhagen Airport, Kastrup", Longitude = 12.641497434, Latitude = 55.623830838 }
                );

            builder.Entity<Aircraft>().ToTable("Aircrafts");
            builder.Entity<Glider>().HasBaseType<Aircraft>();
            builder.Entity<Airplane>().HasBaseType<Aircraft>();
            builder.Entity<Aircraft>().HasDiscriminator(a => a.AircraftType);
            builder.Entity<Aircraft>()
                .Property(a => a.AircraftType)
                .HasMaxLength(200)
                .HasColumnName("aircraft_type");
            builder.Entity<Aircraft>().HasKey(a => a.Id);
            builder.Entity<Aircraft>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Aircraft>().Property(a => a.Name).IsRequired();
            builder.Entity<Aircraft>().Property(a => a.AirspeedInKilometersPerHour).IsRequired();
            builder.Entity<Aircraft>().Property(a => a.AircraftType);
            builder.Entity<Aircraft>().Property(a => a.PilotCount).IsRequired();

            builder.Entity<Airplane>().HasData
                (
                new Airplane { Id = 49, Name = "Boeing 707", AirportId = 99, AirspeedInKilometersPerHour = 965.61, PilotCount = 2, FuelConsumptionInKilometerPerLitre = 0.08 },
                new Airplane { Id = 50, Name = "Cargoplane 1337", AirportId = 99, AirspeedInKilometersPerHour = 500.05, PilotCount = 3, FuelConsumptionInKilometerPerLitre = 0.05 }
                );
            builder.Entity<Glider>().HasData
                (
                new Glider { Id = 51, Name = "Smooth sailing glider", PilotCount = 1, AirportId = 100, AirspeedInKilometersPerHour = 100 }
                );

        }
    }
}
