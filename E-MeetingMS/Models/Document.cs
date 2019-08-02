using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_MeetingMS.Models
{
    public class Document
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Document_Name { get; set; }

        [Required]
        public string Document_URL { get; set; }

        public int MeetingID { get; set; }

        public Meeting Meeting { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }

    public class Comment
    {
        [Key]
        public int ID { get; set; }

        public string Username { get; set; }

        public string Content { get; set; }

        public bool IsCancelled { get; set; }

        public DateTime DateTime { get; set; }

        public Document Document { get; set; }
    }
}