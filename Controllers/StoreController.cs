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
    }
}
