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

    public class TableController : Controller
    {
        private readonly ITableService service;

        public TableController(ITableService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Show the entity Index view
        /// </summary>
        public async Task<IActionResult> Index(int id)
        {
            ViewData["StoreId"] = id;

            var model = await service.GetAll();

            return View(model.Where(x => x.StoreId == id).ToList());
        }

        /// <summary>
        /// Show the entity Create view
        /// </summary>
        /// <param name="id">Id of the store the table belongs to</param>
        public IActionResult Create(int id)
        {
            var model = new Table
            {
                Id = 0,
                StoreId = id,
            };

            return View(model);
        }

        /// <summary>
        /// Create the entity posted from the Create view
        /// </summary>
        /// <param name="model">Model posted from Create view</param>
        [HttpPost]
        public async Task<IActionResult> Create(Table model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != null || model.Id != 0)
                    model.Id = 0;

                await service.Add(model);
                return RedirectToAction("Index", new { id = model.StoreId });
            }

            return View(model);
        }

        /// <summary>
        /// Show the entity Edit view
        /// </summary>
        /// <param name="id">Id of the entity being edited</param>
        public async Task<IActionResult> Edit(int id)
        {
            var model = await service.Get(id);
            return View(model);
        }

        /// <summary>
        /// Edit the entity posted from the Edit view
        /// </summary>
        /// <param name="model">Model posted from Edit view</param>
        [HttpPost]
        public async Task<IActionResult> Edit(Table model)
        {
            if (ModelState.IsValid)
                await service.Update(model);

            return RedirectToAction("Index", new{ id = model.StoreId });
        }

        /// <summary>
        /// Show the entity Delete view
        /// </summary>
        /// <param name="id">Id of the entity being deleted</param>
        public async Task<IActionResult> Delete(int id)
        {
            var model = await service.Get(id);
            return View(model);
        }

        /// <summary>
        /// Delete the entity posted from Delete view
        /// </summary>
        /// <param name="entity">Entity posted from Delete view</param>
        [HttpPost]
        public async Task<IActionResult> Delete(Table entity)
        {
            if (ModelState.IsValid)
            {
                await service.Remove(entity);
            }

            return RedirectToAction("Index", new { id = entity.StoreId });
        }

        /// <summary>
        /// Show the entity Details view
        /// </summary>
        /// <param name="id">Id of the entity being detailed</param>
        public async Task<IActionResult> Details(int id)
        {
            var model = await service.Get(id);
            return View(model);
        }

        /// <summary>
        /// Change the table status to unavailable or available
        /// </summary>
        /// <param name="idTable">Id of the table to modified</param>
        [HttpPost]
        public async Task<IActionResult> ChangeTableStatus(int idTable)
        {
            try
            {
                var table = await service.Get(idTable) ?? throw new NullReferenceException();

                table.IsAvailable = !table.IsAvailable;

                await service.Update(table);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Show the Error view
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
