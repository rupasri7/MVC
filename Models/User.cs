    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Models;

namespace Models
{
    [Table("User")]
    public class User
    {
        public User()
        {

        }
        [Key]
       
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
        [ForeignKey("UserRole")]
        public int UserTypeId { get; set; }
        public UserRole UserRole { get; set; }

        


    }
}