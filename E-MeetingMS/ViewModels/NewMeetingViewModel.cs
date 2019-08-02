using E_MeetingMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_MeetingMS.ViewModels
{
    public class NewMeetingViewModel
    {
        [Required(ErrorMessage = "Meeting Title Name is Required")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Meeting Venue is Required")]
        [Display(Name = "Venue")]
        public string Venue { get; set; }

        [Required(ErrorMessage = "Date is Required")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MMM-dd}", ApplyFormatInEditMode = true)]
        public string Date { get; set; }

        [Required(ErrorMessage = "Time is Required")]
        [Display(Name = "Time")]
        public string Time { get; set; }

        [Required(ErrorMessage = "Select UserName")]
        [Display(Name = "Select Username")]
        public string UserID
        {
            get;
            set;
        }
        public List<ApplicationUser> Userlist
        {
            get;
            set;
        }

        public List<string> selectedUsers { get; set;  }

        public List<ExactTime> ExactTime { get; set; }

        public string[] to { get; set; }


    }

    public class ExactTime
    {
        public int ID { get; set; }

        public string Display { get; set; }

        public string Hidden { get; set; }


        public List<ExactTime> GetLocations()
        {
            List<ExactTime> LocationCollection = new List<ExactTime>()
            {
                new ExactTime() {ID = 1, Display = "8:00 AM", Hidden = "8:00:00 AM" },
                new ExactTime() {ID = 1, Display = "8:30 AM", Hidden = "8:30:00 AM" },
                new ExactTime() {ID = 1, Display = "9:00 AM", Hidden = "9:00:00 AM" },
                new ExactTime() {ID = 1, Display = "9:30 AM", Hidden = "9:30:00 AM" },
                new ExactTime() {ID = 1, Display = "10:00 AM", Hidden = "10:00:00 AM" },
                      new ExactTime() {ID = 1, Display = "10:30 AM", Hidden = "10:30:00 AM" },
                new ExactTime() {ID = 1, Display = "11:00 AM", Hidden = "11:00:00 AM" },
                      new ExactTime() {ID = 1, Display = "12:00 PM", Hidden = "12:00:00 PM" },
                new ExactTime() {ID = 1, Display = "1:00 PM", Hidden = "1:00:00 PM" },
                      new ExactTime() {ID = 1, Display = "1:30 PM", Hidden = "1:30:00 PM" },
                new ExactTime() {ID = 1, Display = "2:00 PM", Hidden = "2:00:00 PM" },
                 new ExactTime() {ID = 1, Display = "3:00 PM", Hidden = "3:00:00 PM" },
                  new ExactTime() {ID = 1, Display = "3:30 PM", Hidden = "3:30:00 PM" },
                   new ExactTime() {ID = 1, Display = "4:00 PM", Hidden = "4:00:00 PM" },
                    new ExactTime() {ID = 1, Display = "4:30 PM", Hidden = "4:30:00 PM" },
                     new ExactTime() {ID = 1, Display = "5:00 PM", Hidden = "5:00:00 PM" },
                   
            };

            return LocationCollection;
        }
    }


}