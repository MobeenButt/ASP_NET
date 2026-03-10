using Microsoft.AspNetCore.Identity;
namespace Lms.Data
{
    // This class is responsible for seeding initial roles into the database when the application starts. It checks if the "Admin" and "Student" roles exist, and if not, it creates them using the RoleManager<IdentityRole>. This ensures that the necessary roles are available for role-based authorization in the application.
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if(!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if(!await roleManager.RoleExistsAsync("Student"))
            {
                await roleManager.CreateAsync(new IdentityRole("Student"));
            }
        }

    }
}
