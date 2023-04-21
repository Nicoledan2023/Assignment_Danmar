using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Zoo.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Pdf.Canvas;

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

        //add to export the file of animals

      public async Task<IActionResult> OnPostExportReportAsync()
        {         
            var animals = await _context.Animals.ToListAsync();           
            if (animals == null)
            {
                return NotFound();
            }

            var pdfDoc = new PdfDocument(new PdfWriter("AnimalReport.pdf"));
            var document = new Document(pdfDoc);
            var columnWidths = new float[] { 100f, 100f, 100f,100f, 100f, 100f, 100f };

            var table = new Table(UnitValue.CreatePercentArray(columnWidths));

            table.AddHeaderCell(new Cell().Add(new Paragraph("Animal Name")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Species")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Age")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Gender")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Location")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Quantity")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Description")));

            uint totalAnimals = 0;
            //group by species then count
            var animalCountsBySpecies = animals
            .GroupBy(a => a.Species)
            .Select(g => new { Species = g.Key, Count = g.Count() });

            foreach (var animal in animals)
            {
                table.AddCell(new Cell().Add(new Paragraph(animal.Name)));
                table.AddCell(new Cell().Add(new Paragraph(animal.Species)));
                table.AddCell(new Cell().Add(new Paragraph(animal.Age.ToString())));
                table.AddCell(new Cell().Add(new Paragraph(animal.Gender)));
                table.AddCell(new Cell().Add(new Paragraph(animal.Location)));
                table.AddCell(new Cell().Add(new Paragraph(animal.quantity.ToString())));
                table.AddCell(new Cell().Add(new Paragraph(animal.Description)));

                //add some calculated fields
                totalAnimals += animal.quantity;

            }

            document.Add(new Paragraph("Total number of animals: " + totalAnimals));
            foreach (var count in animalCountsBySpecies)
            {
                document.Add(new Paragraph($"Species: {count.Species}, Count: {count.Count}"));
            }
            document.Add(table);

         var fileName = "AnimalReport.pdf";
         var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
         var fileDirectory = Path.GetDirectoryName(filePath);

        if (!Directory.Exists(fileDirectory))
        {
            Directory.CreateDirectory(fileDirectory);
        }

            document.Close();
            return Page();

        }


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
