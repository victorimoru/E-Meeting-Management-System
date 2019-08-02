using E_MeetingMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_MeetingMS.ViewModels
{
    public class ViewDocumentViewModel
    {
        public int DocMeetingID { get; set; }
        public List<Document> Documents { get; set; }

      


    }
}