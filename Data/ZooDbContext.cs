
using Microsoft.EntityFrameworkCore;
namespace Zoo.Models;

public class ZooDbContext : DbContext
{
    public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options)
    {
    }

    public DbSet<AnimalModel> Animals { get; set; } = default!;

}

