using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zoo.Models;

namespace zoo.Pages_Users
{
  public class CreateModel : PageModel
  {
    private readonly Zoo.Models.ZooDbContext _context;

    public CreateModel(Zoo.Models.ZooDbContext context)
    {
      _context = context;
    }

    public IActionResult OnGet()
    {
      return Page();
    }

    [BindProperty]
    public PersonModel PersonModel { get; set; } = default!;


    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid || _context.Persons == null || PersonModel == null)
      {
        return Page();
      }

      _context.Persons.Add(PersonModel);
      await _context.SaveChangesAsync();

      return RedirectToPage("./Index");
    }
  }
}
