var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//Middleware Code
//// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
//app.Use(
//    async (context, next) =>
//    {
//        await context.Response.WriteAsync("AOA Pakistan...\n");
//        await next();
//        await context.Response.WriteAsync("AOA Pakistan again 1nd Middleware...\n");
//    }
//    ); 
//app.Use(
//    async (context, next) =>
//    {
//        await context.Response.WriteAsync("AOA Pakistan again...\n");
//        //await next();
//        await context.Response.WriteAsync("AOA Pakistan again 2nd Middleware...\n");
//    }
//    );

//app.Run(async (context)=>
//{
//    await context.Response.WriteAsync("I am in the last ones...");

//});
