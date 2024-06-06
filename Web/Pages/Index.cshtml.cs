using DataAccess;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DBContext _context;


        [BindProperty]
        public Domain.Entities.User User { get; set; }

        public IndexModel(DBContext context)
        {
             _context = context;
        }

        public IActionResult OnGet()
        {
            if(HttpContext.Session.GetInt32("UID") != null)
            {
                return RedirectToPage("Store/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == User.Email && x.Password == User.Password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UID", user.Id);
                return RedirectToPage("Store/Index");
            }


            return Page();
        }
    }
}
