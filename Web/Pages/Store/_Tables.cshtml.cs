using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Domain.Entities;

namespace Web.Pages.Table
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.DBContext _context;

        public IndexModel(DataAccess.DBContext context)
        {
            _context = context;
        }

        public IList<Domain.Entities.Table> Table { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Table = await _context.Tables
                .Include(t => t.Store).ToListAsync();
        }
    }
}
