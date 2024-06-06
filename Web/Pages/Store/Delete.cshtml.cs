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
    public class DeleteModel : PageModel
    {
        private readonly DataAccess.DBContext _context;

        public DeleteModel(DataAccess.DBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Domain.Entities.Store Store { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores.FirstOrDefaultAsync(m => m.Id == id);

            if (store == null)
            {
                return NotFound();
            }
            else
            {
                Store = store;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                Store = store;
                _context.Stores.Remove(Store);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
