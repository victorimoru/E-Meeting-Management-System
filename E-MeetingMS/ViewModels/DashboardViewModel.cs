using E_MeetingMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace E_MeetingMS.ViewModels
{
    public class DashboardViewModel
    {
        public ApplicationUser User { get; set; }
        public List<ApplicationUser> Users { get; set; }

        public List<Meeting> Meetings { get; set; }

        public List<Comment> Comments { get; set; }

        [Required]
        [Display(Name = "Select User")]
        public string selectedUser { get; set; }

        [Required]
        [Display(Name = "Select Role")]
        public string selectedRole { get; set; }
        public List<IdentityRole> Roles { get; internal set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [RegularExpression(@"^[a-z]+[a-zA-Z0-9-'\s]*$", ErrorMessage = "UserName must begin with small letter and may contain letters and digits only" )]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z0-9-'\s]*$", ErrorMessage = "Password must contain atleast a capital letter with letters and digits only ")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,}$")]
        public string Password { get; set;  }
    }

    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}