using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Models;

namespace Models
{
    [Table("UserHouseholdMapping")]
    public class UserHouseholdMapping
    {
        
        [Key]
        public int UserHouseholdMappingId { get; set; }

        [ForeignKey("Household")]
        public int HouseHoldId1 { get; set; }
        public Household Household { get; set; }
            
        [ForeignKey("Household2")]
        public int HouseHoldId2 { get; set; }
        public Household Household2 { get; set; }

        [ForeignKey("Relationship")]
        public int RelationshipId { get; set; }
        public Relationship Relationship { get; set; }
    }
    public class Relation
    {
       
        public int HouseHoldId2 { get; set; }

        public int RelationshipId { get; set; }
    }
    public class PdfData
    {
        public int ApplicationId { get; set; }
        public string ApplicantName { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberDob { get; set; }
        public string MemberGender { get; set; }
        public string Relate { get; set; }
    }
}