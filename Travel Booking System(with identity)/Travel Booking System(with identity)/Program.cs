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
    var initializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();
    await initializer.InitializeAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
