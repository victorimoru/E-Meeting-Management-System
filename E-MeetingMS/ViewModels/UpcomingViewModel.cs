using E_MeetingMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_MeetingMS.ViewModels
{
    public class UpcomingViewModel
    {
        public List<string> Invite { get; internal set; }
        public Meeting Meeting { get; set; }
        public List<Meeting> Meetings { get; set; }

        public List<string> Participants { get; set; }

       // public List<Comment> Comments { get; set; }
    }


    public class EditMeetingViewModel
    {
        public string PageTitle;
        public int DocCounter { get; set; }
        internal List<Document> MeetingDocuments;

        public EditMeetingViewModel()
        {
        }

        [Required(ErrorMessage = "Meeting Title Name is Required")]
        [Display(Name = "Title")]
        public string Title { get; set; }


        public List<ExactTime> ExactTime { get; set; }


        [Required(ErrorMessage = "Meeting Venue is Required")]
        [Display(Name = "Venue")]
        public string Venue { get; set; }

        [Required(ErrorMessage = "Date is Required")]
        [Display(Name = "Date")]
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

        public List<string> selectedUsers { get; set; }

        public string[] to { get; set; }
        public HashSet<ApplicationUser> selectedUSersInDB { get; internal set; }

        [Required]
        public int Meeting_Id { get; set; }

    }
}