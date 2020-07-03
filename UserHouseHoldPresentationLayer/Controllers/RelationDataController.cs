using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace UserHouseHoldPresentationLayer.Controllers
{
    [System.Web.Http.Route("api/[Controller]")]
    public class RelationDataController : ApiController
    {
        [System.Web.Http.HttpPost]
        //[System.Web.Http.ActionName("SaveDataWithRelation")]
        [System.Web.Http.Route("api/RelationData/SaveDataWithRelation")]
        public void SaveDataWithRelation(FormCollection form = null)
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

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
