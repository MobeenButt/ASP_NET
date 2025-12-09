using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Travel_Booking_System_with_identity_.Data;
using Travel_Booking_System_with_identity_.Models;
using Travel_Booking_System_with_identity_.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options=> options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization(options=>
{
    //Admin Policy
    options.AddPolicy("RequireAdminRole",policy=>policy.RequireRole("Admin"));
    //User Policy
    options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));

    //Admin or User Policy
    options.AddPolicy("RequireAdminUserRole", policy => policy.RequireRole("Admin","User"));


});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Home/AccessDenied"; 
    options.LoginPath = "/Identity/Account/Login";
});

var app = builder.Build();

// Initialize database with roles and admin user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var admin = await userManager.FindByEmailAsync("admin@example.com");
    if (admin == null)
    {
        admin = new IdentityUser
        {
            UserName = "admin@example.com",
            Email = "admin@example.com",
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(admin, "Admin@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }

    var employee = await userManager.FindByEmailAsync("employee@example.com");
    if (employee == null)
    {
        employee = new IdentityUser
        {
            UserName = "employee@example.com",
            Email = "employee@example.com",
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(employee, "Employee@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(employee, "User");
        }
    }
}   

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
