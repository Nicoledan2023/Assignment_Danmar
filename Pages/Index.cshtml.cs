using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Zoo.Models;
namespace zoo.Pages;

public class IndexModel : PageModel
{
  private readonly ILogger<IndexModel> _logger;
  private readonly ZooDbContext _context;

  public IndexModel(ILogger<IndexModel> logger, ZooDbContext context)
  {
    _logger = logger;
    _context = context;
  }
  public List<UserModel> Users { get; set; }

  public List<EventModel> Events { get; set; }


  public async Task<IActionResult> OnGetAsync()
  {
    Events = await _context.Zooevents.ToListAsync();
    Users = await _context.Users.ToListAsync();
    return Page();
  }

}
