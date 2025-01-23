using BitTest.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<CsvDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CsvDatabase"))
);

builder.Services.AddScoped<CsvLoader>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();



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
