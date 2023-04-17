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
    public class DetailsModel : PageModel
    {
        private readonly Zoo.Models.ZooDbContext _context;

        public DetailsModel(Zoo.Models.ZooDbContext context)
        {
            _context = context;
        }

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
    }
}
