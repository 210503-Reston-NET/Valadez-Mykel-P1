using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using BuisnessLogic;
using Models;

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
        public ActionResult FindOrderResults(int orderId)
        {
            OrderVM order = new OrderVM( _BL.CheckOrder(orderId));
            return View(order);
        }

        //public ActionResult SearchForLocation()
        //{
        //    return View();
        //}

        public ActionResult ManageLocation(int locationId)
        {
            return View(locationId);
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



        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}