using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zoo.Models;

namespace zoo.Pages_ZooAnimals
{
    public class CreateModel : PageModel
    {
        private readonly Zoo.Models.ZooDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(Zoo.Models.ZooDbContext context, IWebHostEnvironment environment, ILogger<CreateModel> logger)
        {
            _context = context;
            _environment = environment;
            _logger = logger;

        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AnimalModel AnimalModel { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile animalImage)
        {
            
            if (animalImage != null && animalImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(animalImage.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await animalImage.CopyToAsync(fileStream);
                }

AnimalModel.ImageName = Path.Combine("uploads", fileName);
            }
            if (!ModelState.IsValid || _context.Animals == null || AnimalModel == null)
            {
                _logger.LogWarning("ModelState is invalid");
                foreach (var val in ModelState.Values.SelectMany(v => v.Errors).Select(e => (e.ErrorMessage, e.Exception)))
				{

					_logger.LogWarning($"ModelState[{val.ErrorMessage}] : {val.Exception}");
				}
                return Page();
            }

            _context.Animals.Add(AnimalModel);
            await _context.SaveChangesAsync();
    
            return RedirectToPage("./Index");
        }
    }

}
