using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;

namespace UserHouseHoldPresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View("Login");
        }
        public ActionResult LoginData(string userName, string password)
        {
            try
            { //int status = UserManager.GetUserFromDB(user);
                WcfService_CodeFirstApproach.Service1 service = new WcfService_CodeFirstApproach.Service1();

                int roleId = service.GetUser(userName, password);
                if (roleId == 1)
                {
                    return View("AdminView");
                }
                else if (roleId == 2)
                {
                    return View("UserView");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The user name or password is incorrect");

                    return View("Login");
                }
            }
            catch(Exception ex)
            {
                return View("Login");
            }
           
           
        }
    }
}