using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_MeetingMS.Models
{
    public class Meeting
    {
        [Key]
        public int MeetingID { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        public string Document_View_Type { get; set; }


        public bool IsCancelled { get; set; }

        [Required]
        public string Venue { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DateTime { get; set; }

        public ICollection<Document> Documents { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; internal set; }


        public Meeting()
        {
            ApplicationUsers = new HashSet<ApplicationUser>();
            Documents = new HashSet<Document>();
        }
    }
}