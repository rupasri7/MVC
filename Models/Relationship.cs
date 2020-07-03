using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Models
{
    [Table("Relationship")]
    public class Relationship
    {
        public Relationship()
        {

        }
        [Key]
        public int Rid { get; set; }
        public string Relation { get; set; }
       
    }
}