using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Zoo.Models;

namespace zoo.Pages_Users
{
  public class DeleteModel : PageModel
  {
    private readonly Zoo.Models.ZooDbContext _context;

    public DeleteModel(Zoo.Models.ZooDbContext context)
    {
      _context = context;
    }

    [BindProperty]
    public PersonModel PersonModel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(uint? id)
    {
      if (id == null || _context.Persons == null)
      {
        return NotFound();
      }

      var usermodel = await _context.Persons.FirstOrDefaultAsync(m => m.UserModelId == id);

      if (usermodel == null)
      {
        return NotFound();
      }
      else
      {
        PersonModel = usermodel;
      }
      return Page();
    }

    public async Task<IActionResult> OnPostAsync(uint? id)
    {
      if (id == null || _context.Persons == null)
      {
        return NotFound();
      }
      var usermodel = await _context.Persons.FindAsync(id);

      if (usermodel != null)
      {
        PersonModel = usermodel;
        _context.Persons.Remove(PersonModel);
        await _context.SaveChangesAsync();
      }

      return RedirectToPage("./Index");
    }
  }
}
