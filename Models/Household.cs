using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Models;

namespace Models
{
    [Table("Household")]
    public class Household
    {
        public Household()
        {

        }
        [Key]
        public int HId { get; set; }

        
        [Required]
        [StringLength(32, MinimumLength = 3)]
        [RegularExpression(@"[a-zA-Z]*$", ErrorMessage = "Use letters and special characters please")]
        [Display(Name = "Firstname")]
        public string Firstname { get; set; }

        //[Required]
        //[RegularExpression(@"^[a-zA-Z_*'-]*$", ErrorMessage = "Use letters and special characters please")]
        [Display(Name = "Middlename")]
        public string Middlename { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z_*'-]*$", ErrorMessage = "Use letters and special characters please")]
        [Display(Name = "lastname")]
        public string Lastname { get; set; }
        public string Suffix { get; set; }

        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "DOB")]
        public string Dob { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}