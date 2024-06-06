using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Domain.Entities;

namespace Web.Pages.Store
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.DBContext _context;

        public IndexModel(DataAccess.DBContext context)
        {
            _context = context;
        }

        public IList<Domain.Entities.Store> Stores { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Stores = await _context.Stores
                .Include(s => s.User).ToListAsync();
        }
    }
}
