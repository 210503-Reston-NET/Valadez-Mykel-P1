using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using BuisnessLogic;

namespace WebUI.Controllers
{
    public class LoginController : Controller
    {
        private BLogic _BL;
        public LoginController(BLogic BL)
        {
            _BL = BL;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CustomerVM userInfo)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    int userId = _BL.CheckUserCredentials(userInfo.Email, userInfo.Password);
                    //if (userId == null) Console.WriteLine("User doesn't exist");
                }

                return View("../Home/Index");
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("unable to find user");
            }


            return View();
        }
    }
}