using Controllers.Base;
using Models.Entities;
using Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using Services.Interfaces.Base;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        /// <summary>
        /// Show the Edit view
        /// </summary>
        /// <param name="id">Id of the store that will be managing it</param>
        public override async Task<IActionResult> Edit(int id)
        {
            var model = this.service.Get(id, new string[] { "Tables" });
            return View(await model);
        }

        /// <summary>
        /// Edit the entity posted from the Edit view
        /// </summary>
        /// <param name="model">Model posted from Edit view</param>
        [HttpPost]
        public override async Task<IActionResult> Edit(Store model)
        {
            if (!Request.Form.Files.Any())
                return View(model);

            #region Convert File to Base64

            var file = Request.Form.Files[0];
            string base64String;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                // Convert the memory stream to a byte array
                var fileBytes = memoryStream.ToArray();

                // Convert the byte array to a Base64 string
                base64String = Convert.ToBase64String(fileBytes);
            }

            #endregion

            model.ImageBase64 = base64String;

            if (!string.IsNullOrEmpty(model.ImageBase64))
                ModelState["ImageBase64"].ValidationState = ModelValidationState.Valid;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await service.Update(model);
            return RedirectToAction("Details", new { id = model.Id });
        }

        /// <summary>
        /// Create the entity posted from the Create view
        /// </summary>
        /// <param name="model">Model posted from Create view</param>
        [HttpPost]
        public override async Task<IActionResult> Create(Store model)
        {
            if (!Request.Form.Files.Any())
                return View(model);

            #region Convert File to Base64

            var file = Request.Form.Files[0];
            string base64String;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                // Convert the memory stream to a byte array
                var fileBytes = memoryStream.ToArray();

                // Convert the byte array to a Base64 string
                base64String = Convert.ToBase64String(fileBytes);
            }

            #endregion

            model.ImageBase64 = base64String;

            if(!string.IsNullOrEmpty(model.ImageBase64))
                ModelState["ImageBase64"].ValidationState = ModelValidationState.Valid;

            if (ModelState.IsValid)
            {
                await service.Add(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
