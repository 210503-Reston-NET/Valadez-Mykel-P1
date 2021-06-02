using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using BuisnessLogic;
using Models;
using Serilog;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        private BLogic _BL;
        public AdminController(BLogic BL)
        {
            _BL = BL;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult FindCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FindCustomerResults(CustomerVM customer)
        {
            Console.WriteLine(customer.CustomerId);

            Customer fc;

            try
            {
                fc = _BL.FindUser(customer.CustomerId);
                CustomerVM fc2 = new CustomerVM(fc);

                return View(fc2);
            }
            catch
            {
                Console.WriteLine("");
            }

            return View();
        }


        public ActionResult FindOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FindOrderResults(OrderVM Id)
        {
            try{
                OrderVM order = new OrderVM( _BL.CheckOrder(Id.OrderId));
                return View(order);
            }catch(Exception e){
                Log.Error(e, "No Order Was Found");
                return View(new OrderVM());
            }
        }

        //public ActionResult SearchForLocation()
        //{
        //    return View();
        //}

        public ActionResult ManageLocation(int locationId)
        {
            try
            {
                Location loc = getLoc(locationId);
                List<LocationProductInventory> items = getItems(locationId);
                
                return View(new FullLocation(loc, items));

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return View("ViewAllLocations");
        }

        public Location getLoc(int id)
        {
            return _BL.FindLocationById(id);
        }

        public List<LocationProductInventory> getItems(int locationId)
        {
            return _BL.ViewInventory(locationId);
        }

        public ActionResult ViewAllLocations()
        {
            List<LocationVM> stores = new List<LocationVM>();
            _BL.GetAllStores().ForEach(i => stores.Add(new LocationVM(i)));
            return View(stores);
        }

        public ActionResult AddLocation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddLocation(Location loc)
        {
            try
            {
                _BL.AddLocation(loc);
            }
            catch
            {

            }
            return View("ViewAllLocations");
        }

        public ActionResult AddInventory(int locationId)
        {
            Console.WriteLine(locationId);
            AddInventoryVM id = new AddInventoryVM(locationId);
            return View(id);
        }

        [HttpPost]
        public ActionResult AddInventory(AddInventoryVM toAddInv)
        {
            Console.WriteLine(toAddInv);
            try
            {
                _BL.AddInventory(toAddInv.ProductId, toAddInv.Quantity, toAddInv.LocationId);

            }
            catch
            {

            }

            return View("./ManageLocation", new FullLocation(getLoc(toAddInv.LocationId),(getItems(toAddInv.LocationId))));
            //return View();
        }

        public ActionResult LocationOrders(FullLocation id)
        {

            List<Order> orders = _BL.ViewTransactionsByLocation(id.LocationId);

            List<OrderVM> orderVMs = new List<OrderVM>();

            orders.ForEach(ord => orderVMs.Add(new OrderVM(_BL.CheckOrder(ord.OrderId))));
            return View(orderVMs);
        }

        
    }
}