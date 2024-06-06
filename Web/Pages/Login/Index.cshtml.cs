using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Web.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.DBContext _context;

        public IndexModel(DataAccess.DBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Domain.Entities.User User { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var success = _context.Users
                .AnyAsync(x => x.Email.Equals(User.Email, StringComparison.OrdinalIgnoreCase) && x.Password.Equals(User.Password, StringComparison.OrdinalIgnoreCase));

            if(await success)
            {
                return RedirectToPage("./Index");
            }
                

            return Page();
        }
    }
}
