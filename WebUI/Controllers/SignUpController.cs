using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebUI.Models;
using BuisnessLogic;

namespace WebUI.Controllers
{
    public class SignUpController : Controller
    {
        private BLogic _BL;
        public SignUpController(BLogic BL)
        {
            _BL = BL;
        }

        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CustomerVM newUser)
        {
            try
            {
                int userId = _BL.AddNewUser(newUser.Name, newUser.Email, newUser.Password);
                return View("../Home/Index", new CustomerVM(userId));
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return View();
        }
    }
}