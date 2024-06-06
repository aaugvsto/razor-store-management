using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Domain.Entities;

namespace Web.Pages.Store
{
    public class EditModel : PageModel
    {
        private readonly DataAccess.DBContext _context;

        public EditModel(DataAccess.DBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Domain.Entities.Store Store { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var store =  await _context.Stores
                .Include(x => x.Tables)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (store == null)
                return NotFound();

            Store = store;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var store = _context.Stores.Find(Store.Id);

            store.Name = Store.Name;
            store.Address = Store.Address;
            store.Phone = Store.Phone;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(Store.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Details", new { id = Store.Id });
        }

        public async Task<IActionResult> OnPostDeleteTableAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);

            if (table != null)
            {
                _context.Tables.Remove(table);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Edit", new { id = Store.Id });
        }

        private bool StoreExists(int id)
        {
            return _context.Stores.Any(e => e.Id == id);
        }
    }
}
