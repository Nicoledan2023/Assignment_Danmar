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
    public class DetailsModel : PageModel
    {
        private readonly Zoo.Models.ZooDbContext _context;

        public DetailsModel(Zoo.Models.ZooDbContext context)
        {
            _context = context;
        }

      public UserModel UserModel { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(uint? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var usermodel = await _context.User.FirstOrDefaultAsync(m => m.UserModelId == id);
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
    }
}
