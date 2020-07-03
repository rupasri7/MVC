
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace UserHouseHoldPresentationLayer.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registration()
        {
            return View("Register");
        }
        public ActionResult RegistrationData(User user)
        
        {
            try
            {
                user.UserTypeId = 2;

                WcfService_CodeFirstApproach.Service1 service = new WcfService_CodeFirstApproach.Service1();
                bool status = service.PostUser(user);

                if (status)
                {
                    return View("Success");
                }
                else
                {
                    return View("Failure");
                }
            }
            catch(Exception ex)
            {
                return View("Failure");
            }
        }
    }
}