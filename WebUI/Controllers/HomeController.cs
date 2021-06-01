using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Models;
using BuisnessLogic;
using Serilog;


namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private BLogic _BL;

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

            _logger.LogInformation("Getting All Locations");
            Log.Information("This way works too");

            List<LocationVM> stores = new List<LocationVM>();
            try{
                _BL.GetAllStores().ForEach(i => stores.Add(new LocationVM(i)));
                
            }
            catch(Exception e){
                _logger.LogError(e, "Unable to get Location information");
            }
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
