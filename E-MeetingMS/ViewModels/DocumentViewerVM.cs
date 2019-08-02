using System.Linq;
using E_MeetingMS.Models;
using System.Collections.Generic;

namespace E_MeetingMS.Controllers
{
    public class DocumentViewerVM
    {
        public DocumentViewerVM()
        {
        }

        public Document DocumentInDB { get; internal set; }
        public IEnumerable<Document> DocumentsAll { get; set; }
        public Meeting Meeting { get; internal set; }
        public List<Comment> _Doc_Comment { get; internal set; }
    }
}