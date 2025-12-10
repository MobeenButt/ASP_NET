using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Travel_Booking_System_with_Identity.Data;
using Travel_Booking_System_with_Identity.Models;
using Travel_Booking_System_with_Identity.Seed;
using Travel_Booking_System_with_Identity.Services;

var builder = WebApplication.CreateBuilder(args);

//Configure DB
builder.Services.AddDbContext<ApplicationDbContext>(options=>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
    );

//Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//Register Services
builder.Services.AddScoped<ITravelService, TravelService>();


//Add MVC
builder.Services.AddControllersWithViews();


var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();

//Authentication before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name:"default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

//Role seeding
using(var scope=app.Services.CreateScope())
{
    await RoleSeeder.SeedRoles(scope.ServiceProvider);
}
app.Run();



//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseMigrationsEndPoint();
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//}
//app.UseRouting();

//app.UseAuthorization();

//app.MapStaticAssets();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}")
//    .WithStaticAssets();

//app.MapRazorPages()
//   .WithStaticAssets();