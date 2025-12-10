using Microsoft.AspNetCore.Identity;
using Travel_Booking_System_with_Identity.Models;
namespace Travel_Booking_System_with_Identity.Seed
{
    public static class RoleSeeder
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager=serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager=serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roles = {"Admin","User"};
            foreach(var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            //Create a default admin user
            string adminEmail= "admin@example.com";
            string adminPassword= "Admin@123";

            if(await userManager.FindByEmailAsync(adminEmail)==null)
            {
                var adminUser=new ApplicationUser
                {
                    UserName=adminEmail,
                    Email=adminEmail,
                    EmailConfirmed=true
                };
                var result=await userManager.CreateAsync(adminUser,adminPassword);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser,"Admin");
                }
            }

        }
    }
}
