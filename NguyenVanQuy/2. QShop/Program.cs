using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QShop.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<QShopContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("QShopContext") ?? throw new InvalidOperationException("Connection string 'QShopContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// ----------Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
		.AddCookie();

// Lỗi vòng lặp
builder.Services.AddControllers().AddJsonOptions(x =>
								x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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

app.UseAuthentication();
app.UseAuthorization();

// ----------Authentication
app.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
		name: "areas",
		pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.Run();
