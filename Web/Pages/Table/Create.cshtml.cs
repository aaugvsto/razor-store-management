using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess;
using Domain.Entities;

namespace Web.Pages.Table
{
    public class CreateModel : PageModel
    {
        private readonly DataAccess.DBContext _context;

        public CreateModel(DataAccess.DBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int storeId)
        {
            Table = new Domain.Entities.Table { 
                StoreId = storeId 
            };

            return Page();
        }

        [BindProperty]
        public Domain.Entities.Table Table { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Tables.Add(Table);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Store/Edit", new { id = Table.StoreId });
        }
    }
}
