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
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.DBContext _context;

        public DetailsModel(DataAccess.DBContext context)
        {
            _context = context;
        }

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

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var store = _context.Stores.Find(id);

            _context.Stores.Remove(store!);

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
