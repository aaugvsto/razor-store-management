using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess;
using Domain.Entities;
using Humanizer.Bytes;

namespace Web.Pages.Store
{
    public class CreateModel : PageModel
    {
        private readonly DataAccess.DBContext _context;

        public CreateModel(DataAccess.DBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Store = new Domain.Entities.Store
            {
                UserId = 1
            };

            return Page();
        }

        [BindProperty]
        public Domain.Entities.Store Store { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if(file is null)
                return Page();

            byte[] imgContent;
            using(var stream = file.OpenReadStream())
            {
                using var br = new BinaryReader(stream);
                    imgContent = br.ReadBytes((int)stream.Length);
            }

            Store.ImageBase64 = Convert.ToBase64String(imgContent, 0, imgContent.Length);

            _context.Stores.Add(Store);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
