using Microsoft.EntityFrameworkCore;
using Web3.ProductManager;
using Microsoft.Extensions.Configuration;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ProductDbContext>(options =>
           options.UseSqlServer("Server=localhost,1433;Database=SEM3_WDA_1st;User Id=sa;Password=Abcd@1234;TrustServerCertificate=true"));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
/*
 * Convention Routing
 */
/*
 * Routing Attribute https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-7.0
 */
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
