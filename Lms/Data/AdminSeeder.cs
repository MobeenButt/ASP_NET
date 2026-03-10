using Lms.Models;
using Microsoft.AspNetCore.Identity;

namespace Lms.Data
{
    public static class AdminSeeder
    {
        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            // Check if admin user already exists
            var adminEmail = "admin@lms.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                // Create admin user
                var admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "Admin@123");

                if (result.Succeeded)
                {
                    // Assign Admin role
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
