using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Models;
using BuisnessLogic;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private BLogic _BL;
        // public HomeController(BLogic BL)
        // {
        //     _BL = BL;
        // }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, BLogic BL)
        {
            _logger = logger;
            _BL = BL;
        }

        public ActionResult Index(int id)
        {
            return View(new CustomerVM(id));
        }

        public ActionResult GetLocations(){
            List<LocationVM> stores = new List<LocationVM>();
            _BL.GetAllStores().ForEach(i => stores.Add(new LocationVM(i)));
            return View(stores);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
