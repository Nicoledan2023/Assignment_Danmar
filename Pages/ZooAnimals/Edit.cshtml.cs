using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zoo.Models;

namespace zoo.Pages_ZooAnimals
{
    public class EditModel : PageModel
    {
        private readonly Zoo.Models.ZooDbContext _context;

        public EditModel(Zoo.Models.ZooDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AnimalModel AnimalModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animalmodel =  await _context.Animals.FirstOrDefaultAsync(m => m.AnimalModelId == id);
            if (animalmodel == null)
            {
                return NotFound();
            }
            AnimalModel = animalmodel;
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

            _context.Attach(AnimalModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalModelExists(AnimalModel.AnimalModelId))
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

        private bool AnimalModelExists(int id)
        {
          return (_context.Animals?.Any(e => e.AnimalModelId == id)).GetValueOrDefault();
        }
    }
}
