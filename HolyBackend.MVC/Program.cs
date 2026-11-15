using FluentValidation;
using HolyBackend.Core.Interfaces;
using HolyBackend.Core.Models;
using HolyBackend.Core.Patterns.Factory;
using HolyBackend.Core.Patterns.Observer;
using HolyBackend.Core.Patterns.Strategy;
using HolyBackend.Core.Services;
using HolyBackend.Core.Validators;
using HolyBackend.DAL;
using HolyBackend.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<HolyDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("HolyDatabase"),
        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorCodesToAdd: null)));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSingleton<IOrderFactory, OrderFactory>();
builder.Services.AddTransient<IOrderObserver, EmailNotificationObserver>();
builder.Services.AddTransient<IOrderObserver, AuditLogObserver>();
builder.Services.AddSingleton<IClientRankStrategy, MasterRankStrategy>();
builder.Services.AddSingleton<IClientRankStrategy, RegularClientRankStrategy>();
builder.Services.AddSingleton<IClientRankStrategy, NewbieRankStrategy>();
builder.Services.AddSingleton<ClientRankContext>();

builder.Services.AddValidatorsFromAssemblyContaining<OrderValidator>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<HolyDbContext>();
    db.Database.Migrate();

    var defaultAdminHash = Convert.ToBase64String(
        System.Security.Cryptography.SHA256.HashData(
            System.Text.Encoding.UTF8.GetBytes("HolyInquisitor666!")));

    if (!db.Users.Any(u => u.Email == "eugene@prayanddeploy.com"))
    {
        db.Users.Add(new ApplicationUser
        {
            Id = "inquisitor-eugene-01",
            UserName = "Євгеній Дроздовський",
            Email = "eugene@prayanddeploy.com",
            PasswordHash = defaultAdminHash,
            Role = "Admin",
            CustomTitle = "Верховний Жрець Коду",
            CompletedOrdersCount = 99,
            RegisteredAt = DateTime.UtcNow
        });
    }

    if (!db.Users.Any(u => u.Email == "anastasia@prayanddeploy.com"))
    {
        db.Users.Add(new ApplicationUser
        {
            Id = "inquisitor-anastasia-02",
            UserName = "Анастасія Гольц",
            Email = "anastasia@prayanddeploy.com",
            PasswordHash = defaultAdminHash,
            Role = "Admin",
            CustomTitle = "Інквізиторка Дизайну",
            CompletedOrdersCount = 99,
            RegisteredAt = DateTime.UtcNow
        });
    }

    db.SaveChanges();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();