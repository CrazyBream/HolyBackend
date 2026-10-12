using HolyBackend.Core.Interfaces;
using HolyBackend.DAL;
using HolyBackend.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<HolyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("HolyDatabase")));

// Реєструємо Repository
builder.Services.AddScoped<IPrayerRepository, PrayerRepository>();

var app = builder.Build();

// ... (стандартний пайплайн ASP.NET)
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();