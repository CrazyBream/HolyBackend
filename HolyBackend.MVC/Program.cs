using FluentValidation;
using HolyBackend.Core.Interfaces;
using HolyBackend.Core.Patterns.Factory;
using HolyBackend.Core.Patterns.Observer;
using HolyBackend.Core.Services;
using HolyBackend.Core.Validators;
using HolyBackend.DAL;
using HolyBackend.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<HolyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("HolyDatabase")));



builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddSingleton<IOrderFactory, OrderFactory>();

builder.Services.AddTransient<IOrderObserver, EmailNotificationObserver>();
builder.Services.AddTransient<IOrderObserver, AuditLogObserver>();

builder.Services.AddValidatorsFromAssemblyContaining<OrderValidator>();

var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();