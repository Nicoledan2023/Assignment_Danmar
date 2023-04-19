
using Microsoft.EntityFrameworkCore;
namespace Zoo.Models;

public class ZooDbContext : DbContext
{
  public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options)
  {
  }

  public DbSet<AnimalModel> Animals { get; set; } = default!;
  public DbSet<EventModel> Zooevents { get; set; } = default!;
  public DbSet<UserModel> Users { get; set; } = default!;


}

