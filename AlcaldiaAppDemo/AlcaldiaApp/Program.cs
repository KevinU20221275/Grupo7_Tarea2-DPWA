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

// dependency injection for the Resident table
builder.Services.AddScoped<IResidentRepository, ResidentRepository>();
builder.Services.AddScoped<IValidator<ResidentModel>, ResidentValidator>();

// dependency injection for the MunicipalServices table
builder.Services.AddScoped<IMunicipalServiceRepository, MunicipalServiceRepository>();
builder.Services.AddScoped<IValidator<MunicipalServiceModel>, MunicipalServiceValidator>();

// dependency injection for the ServiceRequests table
builder.Services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();
builder.Services.AddScoped<IValidator<ServiceRequestModel>, ServiceRequestValidator>();



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
