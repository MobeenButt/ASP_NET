using Identity_Authorization___AJAX_Interaction_in_MVC.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity_Authorization___AJAX_Interaction_in_MVC.Data
{
    public class DbSeeder
    {
        public static async Task SeedRoles(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            
            string[] roles = { "Admin", "User" };
            foreach(var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            
            // Create default Admin user
            string adminUsername = "admin";
            string adminPassword = "Admin@123";
            
            if(await userManager.FindByNameAsync(adminUsername) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminUsername,
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
            
            // Create default regular User
            string userName = "user";
            string userPassword = "User@123";
            
            if(await userManager.FindByNameAsync(userName) == null)
            {
                var regularUser = new ApplicationUser
                {
                    UserName = userName,
                    Email = "user@example.com",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(regularUser, userPassword);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(regularUser, "User");
                }
            }
        }
    }
}


/*
 * Automatically creates Admin and User roles in the database if they do not already exist.
 * Prevents Manual DB work for role creation.
*/