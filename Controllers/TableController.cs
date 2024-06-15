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

    public class TableController : Controller
    {
        private readonly ITableService service;

        public TableController(ITableService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index(int id)
        {
            ViewData["StoreId"] = id;

            var model = await service.GetAll();

            return View(model.Where(x => x.StoreId == id).ToList());
        }

        public IActionResult Create(int id)
        {
            var model = new Table
            {
                Id = 0,
                StoreId = id,
            };

            return View(model);
        }

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

        public async Task<IActionResult> Edit(int id)
        {
            var model = await service.Get(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Table model)
        {
            if (ModelState.IsValid)
            {
                await service.Add(model);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await service.Get(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Table entity)
        {
            if (ModelState.IsValid)
            {
                await service.Remove(entity);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await service.Get(id);
            return View(model);
        }

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
