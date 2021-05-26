using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuisnessLogic;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class OrdersController : Controller
    {
        private BLogic _BL;
        public OrdersController(BLogic BL)
        {
            _BL = BL;
        }
        // GET: Orders
        public ActionResult Index(CustomerVM id)
        {

            return View(new OrderVM(id));
        }

        public ActionResult Dirt(OrderVM ord)
        {
            
            ViewBag.itemCount = _BL.CheckItemAmount(1);
            return View(ord);
        }

        [HttpPost]
        public ActionResult MakeOrder(OrderVM ord)
        {
            _BL.MakePurchase(ord.ProductId, ord.Quantity, ord.CustomerId);
            return View("../Home/Index", new CustomerVM(ord.CustomerId));
        }
        
    }
}