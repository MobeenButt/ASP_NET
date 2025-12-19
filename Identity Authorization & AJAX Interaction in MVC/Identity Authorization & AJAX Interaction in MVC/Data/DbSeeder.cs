using Microsoft.AspNetCore.Identity;

namespace Identity_Authorization___AJAX_Interaction_in_MVC.Data
{
    public class DbSeeder
    {
        public static async Task SeedRoles(IServiceProvider service)
        {
            var roleManager=service.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "Admin", "User" };
            foreach(var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}


/*
 * Automatically creates Admin and User roles in the database if they do not already exist.
 * Prevents Manual DB work for role creation.
*/