using AlcaldiaApp.Data;
using AlcaldiaApp.Models;
using AlcaldiaApp.Repositories;
using AlcaldiaApp.Validations;
using FluentValidation;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<SqlDataAccess>();

// dependency injection for the Positions table
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IValidator<PositionModel>, PositionValidator>();

// dependency injection for the Employees table
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IValidator<EmployeeModel>, EmployeeValidator>();



ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
