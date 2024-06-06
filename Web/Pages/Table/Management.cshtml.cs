using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Web.Pages.Table
{
    public class ManagementModel : PageModel
    {
        private readonly DataAccess.DBContext _context;

        public ManagementModel(DataAccess.DBContext context)
        {
            _context = context;
        }

        public IList<Domain.Entities.Table> Tables { get;set; } = default!;

        public async Task OnGetAsync(int storeId)
        {
            var itens = await this.GetStoreTables(storeId);
            Tables = itens;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);

            if(table != null)
            {
                table.IsAvailable = !table.IsAvailable;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Management", new { storeId = table!.StoreId });
        }

        public async Task<IList<Domain.Entities.Table>> GetStoreTables(int storeId)
        {
            return await _context.Tables
                .Where(t => t.StoreId == storeId)
                .ToListAsync();
        }
    }
}
