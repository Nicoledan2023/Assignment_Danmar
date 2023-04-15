using Microsoft.EntityFrameworkCore;
using Zoo.Models;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

var dbmsVersion = new MariaDbServerVersion(builder.Configuration.GetValue<string>("MariaDbVersion"));
var connectionString = builder.Configuration.GetConnectionString("ZooDb");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ZooDbContext>(opt => opt.UseMySql(connectionString, dbmsVersion));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
