using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Models;

namespace Models
{
    [Table("UserRole")]
    public class UserRole
    {
        public UserRole()
        {

        }
        [Key]
        public int UserRoleID { get; set; }
        public string UserRoleName { get; set; }
    }
}