using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Zoo.Models;

namespace zoo.Pages_EventsZoo
{
    public class DeleteModel : PageModel
    {
        private readonly Zoo.Models.ZooDbContext _context;

        public DeleteModel(Zoo.Models.ZooDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public EventModel EventModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(uint? id)
        {
            if (id == null || _context.Zooevents == null)
            {
                return NotFound();
            }

            var eventmodel = await _context.Zooevents.FirstOrDefaultAsync(m => m.EventModelId == id);

            if (eventmodel == null)
            {
                return NotFound();
            }
            else 
            {
                EventModel = eventmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(uint? id)
        {
            if (id == null || _context.Zooevents == null)
            {
                return NotFound();
            }
            var eventmodel = await _context.Zooevents.FindAsync(id);

            if (eventmodel != null)
            {
                EventModel = eventmodel;
                _context.Zooevents.Remove(EventModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
