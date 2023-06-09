using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zoo.Models;

namespace zoo.Pages_Users
{
  public class EditModel : PageModel
  {
    private readonly Zoo.Models.ZooDbContext _context;

    public EditModel(Zoo.Models.ZooDbContext context)
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
      PersonModel = usermodel;
      return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      _context.Attach(PersonModel).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!UserModelExists(PersonModel.UserModelId))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return RedirectToPage("./Index");
    }

    private bool UserModelExists(uint id)
    {
      return (_context.Persons?.Any(e => e.UserModelId == id)).GetValueOrDefault();
    }
  }
}
