using Controllers.Base;
using Domain.Dtos;
using Domain.Entities;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using Services.Interfaces.Base;
using System.Diagnostics;

namespace WebMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService userService;

        public LoginController(IUserService service)
        {
            userService = service;
        }

        /// <summary>
        /// Show the Login view
        /// </summary>
        public IActionResult Login()
        {
            User model = new();
            return View(model);
        }

        /// <summary>
        /// Authenticates the user 
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (ModelState.IsValid)
                if(await this.userService.Authenticate(dto))
                    return RedirectToAction("Index", "Store");

            return View(new User { Email = dto.Email, Password = dto.Password });
        }
    }
}
