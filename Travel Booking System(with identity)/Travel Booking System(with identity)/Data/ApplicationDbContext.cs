using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Travel_Booking_System_with_identity_.Models;

namespace Travel_Booking_System_with_identity_.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<TravelPackage> TravelPackages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Additional model configuration can go here
            builder.Entity<TravelPackage>(entity =>
            {
                entity.HasKey(e => e.PackageID);
                //entity.Property(e => e.PackageName).IsRequired().HasMaxLength(100);
                //entity.Property(e => e.Destination).IsRequired().HasMaxLength(100);
                //entity.Property(e => e.DurationDays).IsRequired();
                //entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
                //entity.Property(e => e.AvailableSeats).IsRequired();
                //entity.Property(e => e.DepartureDate).IsRequired();
            });

            // Configure relationships
            builder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookingID);
                entity.HasOne(e => e.Package)
                      .WithMany()
                      .HasForeignKey(e => e.PackageID);
                entity.HasOne(e => e.Customer)
                      .WithMany()
                      .HasForeignKey(e => e.CustomerId);
            });
        
        }
    }
}