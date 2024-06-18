using Controllers.Base;
using Models.Entities;
using Models.ViewModels;
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

        /// <summary>
        /// Show the TableManagement view
        /// </summary>
        /// <param name="id">Id of the store that will be managing it</param>
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

        /// <summary>
        /// Delete a store's table
        /// </summary>
        /// <param name="idTable">Id of the table that will be deleted</param>
        /// <exception cref="ArgumentOutOfRangeException">Throw if the number is less than or equal to zero</exception>
        [HttpPost]
        public async Task<IActionResult> DeleteTable(int idTable)
        {
            try
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(idTable);

                var table = await tableService.Get(idTable);

                if (table != null)
                    await tableService.Remove(table);

                return NoContent();
            }
            catch(ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
            catch
            {
                return StatusCode(500);
            }
        } 
    }
}
