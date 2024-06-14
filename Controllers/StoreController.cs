using Controllers.Base;
using Domain.Entities;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using Services.Interfaces.Base;
using System.Diagnostics;

namespace WebMVC.Controllers
{
    public class StoreController : BaseController<Store>
    {
        public StoreController(IStoreService service) : base(service)
        {
        }
        public async Task<IActionResult> TableManagement(int id)
        {
            ViewData["StoreId"] = id;

            var model = await service.Get(id, new string[] { "Tables" });
            return View(model!.Tables);
        }
    }
}
