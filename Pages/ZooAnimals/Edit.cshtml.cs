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
         private readonly IWebHostEnvironment _environment;
          private readonly ILogger<CreateModel> _logger;

        public EditModel(IWebHostEnvironment environment,  ILogger<CreateModel> logger,Zoo.Models.ZooDbContext context)
        {
            _context = context;
             _environment = environment;
            _logger = logger;
        }

        [BindProperty]
        public AnimalModel AnimalModel { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(uint? id)
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

            ViewData["OriginalImageName"] = AnimalModel.ImageName;
             


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync( IFormFile? animalImage,string? originalImageName)
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
            else {
               

                AnimalModel.ImageName = originalImageName;

                _logger.LogInformation("999999999 value: {@originalImageName}", originalImageName);


            }
            if (!ModelState.IsValid)
            {
               
                   _logger.LogWarning("ModelState is invalid");
                foreach (var val in ModelState.Values.SelectMany(v => v.Errors).Select(e => (e.ErrorMessage, e.Exception)))
				{

					_logger.LogWarning($"ModelState[{val.ErrorMessage}] : {val.Exception}");
				}
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

        private bool AnimalModelExists(uint id)
        {
          return (_context.Animals?.Any(e => e.AnimalModelId == id)).GetValueOrDefault();
        }
    }
}
