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
        private readonly ILogger<CreateModel> _logger;

        public IndexModel(Zoo.Models.ZooDbContext context,ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<AnimalModel> AnimalModel { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string Query { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string ASpecies { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string AGender { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string ALocation { get; set; } = default!;

        
        public string SortOrder { get; set; } = "Location_asc";
            public async Task OnGetAsync(string sortby)
            {
                
                IQueryable<AnimalModel> animals = _context.Animals;
                if (!string.IsNullOrEmpty(Query))
                {
                    animals = animals.Where(g => g.Name.Contains(Query));
                }
                if (!string.IsNullOrEmpty(ASpecies))
                {
                    animals = animals.Where(g => g.Species.Contains(ASpecies));
                }
                if (!string.IsNullOrEmpty(AGender))
                {
                    animals = animals.Where(g => g.Gender.Contains(AGender));
                }
                if (!string.IsNullOrEmpty(ALocation))
                {
                    animals = animals.Where(g => g.Location.Contains(ALocation));
                }
            _logger.LogInformation(SortOrder);

            switch (sortby)
                {
                    
                    case "Location_desc":
                        animals = animals.OrderByDescending(g => g.Location);
                        SortOrder = "Location_desc";
                        break;
                    case "Location_asc":
                        animals = animals.OrderBy(g => g.Location);
                        SortOrder = "Location_asc";
                        break;
                    case "Name_desc":
                        animals = animals.OrderByDescending(g => g.Name);
                        SortOrder = "Name_desc";
                        break;
                    case "Name_asc":
                        animals = animals.OrderBy(g => g.Name);
                        SortOrder = "Name_asc";
                        break;
                     case "Quan_desc":
                        animals = animals.OrderByDescending(g => g.quantity);
                        SortOrder = "Quan_desc";
                        break;
                    case "Quan_asc":
                        animals = animals.OrderBy(g => g.quantity);
                        SortOrder = "Quan_asc";
                        break;
                            
                    default: 
                        animals = animals.OrderBy(g => g.Name);
                        break;
                }

                AnimalModel = await animals.ToListAsync();
            
            }
        }
}
