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
    public class UserController : BaseController<User>
    {
        public UserController(IUserService service) : base(service)
        {
        }
    }
}
