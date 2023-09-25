using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BTTH.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BTTHMVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BTTHMVCContext") ?? throw new InvalidOperationException("Connection string 'BTTHMVCContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

//add authorization 
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
