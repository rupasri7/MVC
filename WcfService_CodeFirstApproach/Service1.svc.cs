using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService_CodeFirstApproach
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public bool PostUser(User user)
        {
            MainDbContext dbContext = new MainDbContext();
            dbContext.UserTable.Add(user);
            dbContext.SaveChanges();
            return true;
        }
        public int GetUser(string userName, string password)
        {
            MainDbContext dbContext = new MainDbContext();
            //var user = (from userlist in dbContext.UserTable
            //            where userlist.Username == login.Username && userlist.Password == login.Password select userlist.UserId).FirstOrDefault();
            var user = dbContext.UserTable.Where(x => x.Username.ToLower().Equals(userName.ToLower())).FirstOrDefault();

            if (user == null)
            {
                return 0;
            }
            if (user.Password.Equals(password))
            {
                return user.UserTypeId;
            }
            return 0;

        }
        public bool PostHouseHold(Household household)
        {
            MainDbContext dbContext = new MainDbContext();
            dbContext.HouseholdTable.Add(household);
            dbContext.SaveChanges();
            return true;
        }
        public List<Relationship> GetRelationshipdata()
        {
            MainDbContext dbContext = new MainDbContext();

            List<Relationship> values = dbContext.RelationshipTable.ToList<Relationship>();
            return values;
        }

        public bool SaveDataToDB(List<Household> data)
        {
            try
            {
                MainDbContext dbContext = new MainDbContext();
                foreach (var item in data)
                {
                    dbContext.HouseholdTable.Add(item);
                }
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<Household> GetAllHouseHoldMembers()
        {
            try
            {
                MainDbContext dbContext = new MainDbContext();
                return dbContext.HouseholdTable.Take(5).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Household> Searchdata(string firstname, string lastname, string dob, string appid, string status)
        {
            MainDbContext dbContext = new MainDbContext();
            var values = dbContext.HouseholdTable.ToList();
            if (!string.IsNullOrEmpty(firstname))
                values = values.Where(s => s.Firstname.Contains(firstname)).ToList();
            if (!string.IsNullOrEmpty(lastname))
                values = values.Where(sa => sa.Lastname.Contains(lastname)).ToList();
            if (!string.IsNullOrEmpty(dob))
                values = values.Where(sb => sb.Dob.Contains(dob)).ToList();
            if (!string.IsNullOrEmpty(appid))
                values = values.Where(sc => sc.HId == Int32.Parse(appid)).ToList();

            return (values.Take(100).ToList());
        }
        public bool PostHouseHoldMapping(List<UserHouseholdMapping> userhousehold)
        {
            MainDbContext dbContext = new MainDbContext();
            dbContext.UserHouseholdMappingsTable.AddRange(userhousehold);
            dbContext.SaveChanges();
            return true;
        }
        public IQueryable<PdfData> PdfRelationMapperList()
        {
            MainDbContext dbContext = new MainDbContext();
            return  from r in dbContext.UserHouseholdMappingsTable
                       join h1 in dbContext.HouseholdTable on r.HouseHoldId1 equals h1.HId
                       join h2 in dbContext.HouseholdTable on r.HouseHoldId2 equals h2.HId
                       join r1 in dbContext.RelationshipTable on r.RelationshipId equals r1.Rid
                       select new PdfData
                       {
                           ApplicationId = h1.HId,
                           ApplicantName = h1.Firstname + " " + h1.Lastname,
                           MemberId = h2.HId,
                           MemberName = h2.Firstname + " " + h2.Lastname,
                           MemberDob = h2.Dob,
                           MemberGender = h2.Gender,
                           Relate = r1.Relation
                       };
       }
    }
}
