using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace UserHouseHoldPresentationLayer.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Searchdata(string firstname, string lastname,string dob,string appid,string status)
        {
            try 
            {
                WcfService_CodeFirstApproach.Service1 service = new WcfService_CodeFirstApproach.Service1();
                
                var value = service.Searchdata( firstname, lastname,dob,appid,status);
                ViewBag.SearchData = value;
                return View();
                
             }

            catch (Exception ex)  
            {  
                throw ex;  
            }
        }   
       
    }
}