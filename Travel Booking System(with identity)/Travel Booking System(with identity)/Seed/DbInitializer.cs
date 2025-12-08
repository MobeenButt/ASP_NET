using Travel_Booking_System_with_identity_.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Travel_Booking_System_with_identity_.Models;
namespace Travel_Booking_System_with_identity_.Seed
{
    public class DbInitializer : IDbIntializer
    {
        private readonly IServiceProvider provider;
        private readonly IConfiguration config;
        public DbInitializer(IServiceProvider provider, IConfiguration config)
        {
            this.provider = provider;
            this.config = config;
        }
        public async Task InitializeAsync()
        {
            using var scope = provider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //ensuring  db configuration
            await context.Database.MigrateAsync();

            var roles = new[] { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            //default admin credentials
            var adminEmail = config["SeedAdmin:Email"] ?? "admin@gmail.com";
            var adminPassword = config["SeedAdmin:Password"] ?? "Admin@123";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new ApplicationUser { UserName = "admin", Email = adminEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
            // optional: seed sample packages if none
            if (!context.TravelPackages.Any())
            {
                context.TravelPackages.AddRange(
                    new TravelPackage { PackageName = "Beach Bliss", Destination = "Maldives", DurationDays = 5, Price = 1200M, AvailableSeats = 20, DepartureDate = DateTime.Parse("2026-02-10") },
                    new TravelPackage { PackageName = "City Explorer", Destination = "Istanbul", DurationDays = 4, Price = 600M, AvailableSeats = 15, DepartureDate = DateTime.Parse("2026-03-05") }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
