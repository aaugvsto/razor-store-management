using Models.Entities.Base;
using Models.ViewModels;
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

        /// <summary>
        /// Show the entity Index view
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var model = await service.GetAll();
            return View(model);
        }

        /// <summary>
        /// Show the entity Create view
        /// </summary>
        public virtual IActionResult Create()
        {
            T model = new();
            return View(model);
        }

        /// <summary>
        /// Create the entity posted from the Create view
        /// </summary>
        /// <param name="model">Model posted from Create view</param>
        [HttpPost]
        public virtual async Task<IActionResult> Create(T model)
        {
            if(ModelState.IsValid) 
            {
                await service.Add(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// Show the entity Edit view
        /// </summary>
        /// <param name="id">Id of the entity being edited</param>
        public virtual async Task<IActionResult> Edit(int id)
        {
            var model = await service.Get(id);
            return View(model);
        }

        /// <summary>
        /// Edit the entity posted from the Edit view
        /// </summary>
        /// <param name="model">Model posted from Edit view</param>
        [HttpPost]
        public virtual async Task<IActionResult> Edit(T model)
        {
            if (ModelState.IsValid)
            {
                await service.Update(model);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Show the entity Delete view
        /// </summary>
        /// <param name="id">Id of the entity being deleted</param>
        public virtual async Task<IActionResult> Delete(int id)
        {
            var model = await service.Get(id);
            return View(model);
        }


        /// <summary>
        /// Delete the entity posted from Delete view
        /// </summary>
        /// <param name="entity">Entity posted from Delete view</param>
        [HttpPost]
        public virtual async Task<IActionResult> Delete(T entity)
        {
            if (ModelState.IsValid)
            {
                await service.Remove(entity);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Show the entity Details view
        /// </summary>
        /// <param name="id">Id of the entity being detailed</param>
        public virtual async Task<IActionResult> Details(int id)
        {
            var model = await service.Get(id);
            return View(model);
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
