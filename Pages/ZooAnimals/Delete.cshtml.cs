using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Zoo.Models;

namespace zoo.Pages_ZooAnimals
{
    public class DeleteModel : PageModel
    {
        private readonly Zoo.Models.ZooDbContext _context;

        public DeleteModel(Zoo.Models.ZooDbContext context)
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

            var animalmodel = await _context.Animals.FirstOrDefaultAsync(m => m.AnimalModelId == id);

            if (animalmodel == null)
            {
                return NotFound();
            }
            else 
            {
                AnimalModel = animalmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }
            var animalmodel = await _context.Animals.FindAsync(id);

            if (animalmodel != null)
            {
                AnimalModel = animalmodel;
                _context.Animals.Remove(AnimalModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
