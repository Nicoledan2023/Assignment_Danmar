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
    public class IndexModel : PageModel
    {
        private readonly Zoo.Models.ZooDbContext _context;

        public IndexModel(Zoo.Models.ZooDbContext context)
        {
            _context = context;
        }

        public IList<AnimalModel> AnimalModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Animals != null)
            {
                AnimalModel = await _context.Animals.ToListAsync();
            }
        }
    }
}
