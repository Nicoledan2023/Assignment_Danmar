using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Zoo.Models;

namespace zoo.Pages_Users
{
    public class DeleteModel : PageModel
    {
        private readonly Zoo.Models.ZooDbContext _context;

        public DeleteModel(Zoo.Models.ZooDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public UserModel UserModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(uint? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var usermodel = await _context.Users.FirstOrDefaultAsync(m => m.UserModelId == id);

            if (usermodel == null)
            {
                return NotFound();
            }
            else 
            {
                UserModel = usermodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(uint? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            var usermodel = await _context.Users.FindAsync(id);

            if (usermodel != null)
            {
                UserModel = usermodel;
                _context.Users.Remove(UserModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
