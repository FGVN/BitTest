using BitTest.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using BitTest.Core.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var migrationsAssembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddDbContext<CsvDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CsvDatabase"),
        sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly))
);

builder.Services.AddScoped<CsvLoader>();

builder.Services.AddValidatorsFromAssemblyContaining<CsvRecord>();

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
    pattern: "{controller=Csv}/{action=Index}/{id?}");


app.Run();
