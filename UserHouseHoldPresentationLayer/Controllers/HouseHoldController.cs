
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace UserHouseHoldPresentationLayer.Controllers
{
    public class HouseHoldController : Controller
    {
        // GET: HouseHold
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HouseholdView()
        {
            return View();
        }
        public ActionResult HouseholdData(Household houseHold)
        {
            WcfService_CodeFirstApproach.Service1 service = new WcfService_CodeFirstApproach.Service1();

            //Add Member -->Session Store --> HouseholdView
            //Next -->Relationshil Page --- Session Store 
            bool status = service.PostHouseHold(houseHold); //Save & Exit
            if (status)
            {
                return View();
            }
            return View();
        }
        public ActionResult HouseholdAddMember(Household houseHold)
        {
            
            List<Household> householdsList = new List<Household>();
            DateTime dob = DateTime.Parse(houseHold.Dob);
            DateTime date = DateTime.Now;
           

                try
                {
                if (Math.Abs(date.Year - dob.Year) < 125 && (dob<date))
                {
                    List<Household> data = new List<Household>();
                    try
                    {
                        data = JsonConvert.DeserializeObject<List<Household>>(HttpContext.Session["Data"].ToString());

                    }
                    catch (Exception)
                    {

                    }
                    bool status = false;
                    foreach (var item in data)
                    {
                        if (item.Firstname.ToLower().Equals(houseHold.Firstname.ToLower()))
                        {
                            status = true;
                            break;
                        }

                    }
                    if (!status)
                    {
                        if (data.Count > 0)
                        {
                            householdsList.AddRange(data);
                        }
                        householdsList.Add(houseHold);
                        HttpContext.Session.Add("Data", JsonConvert.SerializeObject(householdsList));
                    }
                }
                else
                    ViewData["dobmessage"] = "age should be less than 125 years and today's date ";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            ViewBag.RelationShip = houseHold;
            return View("HouseholdView");


        }

        public ActionResult HouseholdRelationShip()
        {
            WcfService_CodeFirstApproach.Service1 service = new WcfService_CodeFirstApproach.Service1();
            try
            {
                List<Household> data = HttpContext.Session["Data"] != null ? JsonConvert.DeserializeObject<List<Household>>(HttpContext.Session["Data"].ToString()) : new List<Household>();
                if (data.Count > 0 && data.Count <= 5)
                {
                    service.SaveDataToDB(data);
                    return RedirectToAction("MapHouseHoldData");
                }
                else
                {
                    ViewData["message"] = "Hey bro please give 5 members yoooo";
                    return View("HouseholdView");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult MapHouseHoldData()
        {
           WcfService_CodeFirstApproach.Service1 service = new WcfService_CodeFirstApproach.Service1();
            try
            {
                List<Household> HouseHoldList = service.GetAllHouseHoldMembers();

                return View("RelationShip", HouseHoldList);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult DeleteMember(string Dob, string FirstName, string LastName)
        {
         
            try
            {
                List<Household> data = JsonConvert.DeserializeObject<List<Household>>(HttpContext.Session["Data"].ToString());
                if (data.Count > 0)
                {
                    Household household1 = data.Where(x => x.Firstname == FirstName).FirstOrDefault();
                    if (household1 != null)
                    {
                        data.Remove(household1);
                        //HttpContext.Session.Remove("data");
                        HttpContext.Session.Add("Data", JsonConvert.SerializeObject(data));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("HouseholdView");
        }

        public ActionResult SaveData()
        {
            WcfService_CodeFirstApproach.Service1 service = new WcfService_CodeFirstApproach.Service1();
            try
            {
                List<Household> data = JsonConvert.DeserializeObject<List<Household>>(HttpContext.Session["Data"].ToString());
                if (data.Count > 0 && data.Count <= 5)
                {
                    bool status = service.SaveDataToDB(data);
                    if (status)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View("~Views/Registration/Failure");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("HouseholdView");
        }
        public bool SaveDataWithRelation(FormCollection form)
        {
            WcfService_CodeFirstApproach.Service1 service = new WcfService_CodeFirstApproach.Service1();
            try
            {
                //for( var item in )
                List<UserHouseholdMapping> data = new List<UserHouseholdMapping>();
                int count = Int32.Parse(form["count"]);
                for (int i = 0; i < count; i++)
                {
                    Relation relate = JsonConvert.DeserializeObject<Relation>(form[i.ToString()]);
                    UserHouseholdMapping userHousehold = new UserHouseholdMapping()
                    {
                        HouseHoldId1 = Int32.Parse(form["HouseHoldId1"]),
                        HouseHoldId2 = relate.HouseHoldId2,
                        RelationshipId = relate.RelationshipId
                    };
                    data.Add(userHousehold);
                }
                bool status = service.PostHouseHoldMapping(data); //Save & Exit               
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}