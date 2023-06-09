﻿using Microsoft.AspNetCore.Mvc;
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
  public List<PersonModel> Users { get; set; }

  public List<EventModel> Events { get; set; }

  public List<AnimalModel> Animals { get; set; }


  public async Task<IActionResult> OnGetAsync()
  {
    Events = await _context.Zooevents.ToListAsync();
    Users = await _context.Persons.ToListAsync();
    Animals = await _context.Animals.ToListAsync();
    return Page();
  }

}
