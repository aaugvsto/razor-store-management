using Domain.Entities.Base;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers.Base
{
    public abstract class BaseController<T> : Controller where T : Entity, new()
    {
        protected readonly IBaseService<T> service;

        public BaseController(IBaseService<T> service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            var model = await service.GetAll();
            return View(model);
        }

        public virtual IActionResult Create()
        {
            T model = new();
            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(T model)
        {
            if(ModelState.IsValid) 
            {
                await service.Add(model);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public virtual async Task<IActionResult> Edit(int id)
        {
            var model = await service.Get(id);
            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Edit(T model)
        {
            if (ModelState.IsValid)
            {
                await service.Add(model);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public virtual async Task<IActionResult> Delete(int id)
        {
            var model = await service.Get(id);
            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(T entity)
        {
            if (ModelState.IsValid)
            {
                await service.Remove(entity);
            }

            return RedirectToAction("Index");
        }

        public virtual async Task<IActionResult> Details(int id)
        {
            var model = await service.Get(id);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
