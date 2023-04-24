
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zoo.Models;

namespace Zoo.Models
{
  public class ZooDbContext : IdentityDbContext<ApplicationUser>
  {
    public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options)
    {
    }

    public DbSet<AnimalModel> Animals { get; set; } = default!;
    public DbSet<EventModel> Zooevents { get; set; } = default!;
    public DbSet<PersonModel> Persons { get; set; } = default!;

    public object PersonModel { get; internal set; }
  }

}

