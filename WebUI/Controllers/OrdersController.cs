using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuisnessLogic;
using WebUI.Models;
using Models;
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
            ord.Available = _BL.CheckItemAmount(1);
            return View(ord);
        }

        public ActionResult Rocks(OrderVM ord)
        {
            ord.Available = _BL.CheckItemAmount(2);
            return View(ord);
        }

        public ActionResult DirtWRocks(OrderVM ord)
        {
            ord.Available = _BL.CheckItemAmount(3);
            return View(ord);
        }

        public ActionResult RocksWDirt(OrderVM ord)
        {
            ord.Available = _BL.CheckItemAmount(4);
            return View(ord);
        }

        [HttpPost]
        public ActionResult MakeOrder(OrderVM ord)
        {
            // Console.WriteLine(ord.Available);
            // return View("FailedOrder", ord);
            if(ord.Available < ord.Quantity){
                return View("FailedOrder", ord);
            }else{
                _BL.MakePurchase(ord.ProductId, ord.Quantity, ord.CustomerId);
                return View("../Home/Index", new CustomerVM(ord.CustomerId));

            }
        }

        public ActionResult GetOrders(CustomerVM id)
        {
            Console.WriteLine(id.CustomerId);
            List<Tuple<Order, OrderDetail>> results = _BL.ViewTransactionsByCustomer(id.CustomerId);
            List<OrderVM> orders = new List<OrderVM>();
            results.ForEach(tup => {
                orders.Add(new OrderVM(tup));
            });
            return View(orders);
        }
    }
}