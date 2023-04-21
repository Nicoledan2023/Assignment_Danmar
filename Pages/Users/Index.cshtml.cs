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
    public class IndexModel : PageModel
    {
        private readonly Zoo.Models.ZooDbContext _context;

        public IndexModel(Zoo.Models.ZooDbContext context)
        {
            _context = context;
        }

        public IList<UserModel> UserModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                UserModel = await _context.Users.ToListAsync();
            }
        }
    }
}
