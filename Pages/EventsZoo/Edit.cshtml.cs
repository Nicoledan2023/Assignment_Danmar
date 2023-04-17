using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zoo.Models;

namespace zoo.Pages_EventsZoo
{
    public class EditModel : PageModel
    {
        private readonly Zoo.Models.ZooDbContext _context;

        public EditModel(Zoo.Models.ZooDbContext context)
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

            var eventmodel =  await _context.Zooevents.FirstOrDefaultAsync(m => m.EventModelId == id);
            if (eventmodel == null)
            {
                return NotFound();
            }
            EventModel = eventmodel;
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

            _context.Attach(EventModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventModelExists(EventModel.EventModelId))
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

        private bool EventModelExists(uint id)
        {
          return (_context.Zooevents?.Any(e => e.EventModelId == id)).GetValueOrDefault();
        }
    }
}
