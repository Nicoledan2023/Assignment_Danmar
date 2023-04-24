using Microsoft.EntityFrameworkCore;
using Zoo.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

var dbmsVersion = new MariaDbServerVersion(builder.Configuration.GetValue<string>("MariaDbVersion"));
var connectionString = builder.Configuration.GetConnectionString("ZooDb");


// Add services to the container.
builder.Services.AddRazorPages(opt =>
{
  //opt.Conventions.AuthorizePage("/Games/Delete");
  opt.Conventions.AuthorizeFolder("/Users");
  opt.Conventions.AllowAnonymousToPage("/Users/Index");
  opt.Conventions.AllowAnonymousToPage("/Users/Details");
});




builder.Services.AddDbContext<ZooDbContext>(opt => opt.UseMySql(connectionString, dbmsVersion));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ZooDbContext>();


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
