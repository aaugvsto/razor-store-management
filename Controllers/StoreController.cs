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
        private readonly ITableService tableService;

        public StoreController(IStoreService service, ITableService tableService) : base(service)
        {
            this.tableService = tableService;
        }

        public async Task<IActionResult> TableManagement(int id)
        {
            ViewData["StoreId"] = id;

            var model = await service.Get(id, new string[] { "Tables" });
            return View(model!.Tables);
        }

        public override async Task<IActionResult> Edit(int id)
        {
            var model = this.service.Get(id, new string[] { "Tables" });
            return View(await model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTable(int idTable)
        {
            try
            {
                if (idTable <= 0)
                    throw new Exception();

                var table = await tableService.Get(idTable);

                //if (table != null)
                //    await tableService.Remove(table);

                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        } 
    }
}
