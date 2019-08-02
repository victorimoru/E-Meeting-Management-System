using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_MeetingMS.ViewModels;
using E_MeetingMS.Models;
using System.IO;
using System.Data.Entity;
using System.Net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Globalization;

namespace E_MeetingMS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext ctx;


       // public List<Employee> DBUsers { get; set; }

        public HomeController()
        {
           // DBUsers = new List<Employee>();
        }
        [HttpGet]
        public JsonResult CommentView(int ID = 16)
        {
            List<UserComment> UScomment = new List<UserComment>();
            

            using (ctx = new ApplicationDbContext())
            {
                var CommentList = from c in ctx.Comments.Where(c => c.IsCancelled == false).Include(s => s.Document)
                                  where c.Document.ID == ID
                                  select new { id = c.ID, username = c.Username, content = c.Content, datetime = c.DateTime};

                foreach (var item in CommentList)
                {
                   UScomment.Add (new UserComment() { id = item.id, username = item.username, content = item.content, datetime = item.datetime.ToString("dddd, d MMM yyyy h:mm tt") });
                }
                return Json(new { Comments = UScomment }, JsonRequestBehavior.AllowGet);
            }

           

        }

        public string PostComment(string username, string content, string datetime, int DOC_ID)
        {
            using (ctx = new ApplicationDbContext())
            {
                var doc = ctx.Documents.Find(DOC_ID);
                if (doc == null)
                    return "Error!";
                var comment = new Comment() { Username = username, Content = content, DateTime = DateTime.Parse(datetime), Document = doc };

                ctx.Comments.Add(comment);

                ctx.SaveChanges();
                return "Success";
            }
        }
            public string DeleteComment(int comment_id)
        {
            using (ctx = new ApplicationDbContext())
            {
                var commentInDB = ctx.Comments.Find(comment_id);
                if ( commentInDB == null)
                    return "Error!";
                //ctx.Entry(commentInDB).State = EntityState.Deleted;
                ctx.Comments.Remove(commentInDB);
                ctx.SaveChanges();
                return "Success";
            }



        }

        [AllowAnonymous]
        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult Index()
        {
            var user =  User.Identity.GetUserId();

            using (ctx = new ApplicationDbContext())
            {
                var meetingsindb = ctx.Meetings.Where(c => c.IsCancelled == false).Include(d => d.Documents).Include(s => s.ApplicationUsers)
                                   .ToList();
                var meetingParticipants = ctx.Users.Include(d => d.Meetings).ToList();

                ApplicationUser appUser = new ApplicationUser();
                appUser = ctx.Users.Find(user);

                var User_Name = appUser.UserName;



                var x = ctx.Meetings.Where(e => e.IsCancelled == false).Include(d => d.ApplicationUsers).
                    Where(s => s.ApplicationUsers.Any(a => a.UserName == User_Name))
                    .Select(s => s.Title).ToList();

                                
                        

                var viewmodel = new UpcomingViewModel() { Meetings = meetingsindb, Invite = x };
                return View(viewmodel);
            }

        }

        public ActionResult Create()
        {
            using (ctx = new ApplicationDbContext())
            {
                var userInDB = ctx.Users.ToList();

              
                ExactTime exact = new ExactTime();

                var vm = new NewMeetingViewModel()
                {
                    
                    Userlist = userInDB,
                    ExactTime = exact.GetLocations()
                    
                };

                return View(vm);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewMeetingViewModel nvm, List<HttpPostedFileBase> postedFiles)
        {

            if (!(nvm.to.Length > 0))
            {

                using (ctx = new ApplicationDbContext())
                {
                    //var userInDB = ctx.Users.ToList();
                    //var vm = new NewMeetingViewModel()
                    //{
                    //    Userlist = 
                    //};

                    return View();
                }



            }
            else

            {

                string path = Server.MapPath("~/Docs/");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

               
                var newMeeting = new Meeting()
                {
                    Title = nvm.Title,
                    Venue = nvm.Venue,

                     DateTime = DateTime.Parse(String.Format("{0} {1}", nvm.Date, nvm.Time))
             
                };

                IList<Document> newDocs = new List<Document>();
                foreach (HttpPostedFileBase postedFile in postedFiles)
                {
                    if (postedFile != null)
                    {
                        string fileName = Path.GetFileName(postedFile.FileName);
                        var extension = Path.GetExtension(postedFile.FileName);
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(postedFile.FileName);
                        var Id = Guid.NewGuid();
                        var shortGuid = Id.ToString().Substring(0, 7);


                        string __path = Path.Combine(path, shortGuid + "_" + fileName);
                        postedFile.SaveAs(__path);
                        var filepath = shortGuid + "_" + fileName;
                        newDocs.Add(new Document() { MeetingID = newMeeting.MeetingID, Document_Name = fileName, Document_URL = filepath });
                    }
                }


                using (ctx = new ApplicationDbContext())
                {
                    ctx.Meetings.Add(newMeeting);
                    ctx.Documents.AddRange(newDocs);

                    foreach (var item in nvm.to)
                    {
                        var user = ctx.Users.Find(item);
                        newMeeting.ApplicationUsers.Add(user);
                    }
                    ctx.SaveChanges();
                    return RedirectToAction("index", "Dashboard");
                }


            }

        }

        public ActionResult View(int id)
        {
            if (id < 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            using (ctx = new ApplicationDbContext())
            {
                var DocumentInDB = ctx.Documents.Where(m => m.MeetingID == id).ToList<Document>();
              // var DocumentInDB = ctx.Documments.SingleOrDefault(s => s.ID == doc_ID);
                if ( !(DocumentInDB.Count > 0) )
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                var viewmodel = new ViewDocumentViewModel() { Documents = DocumentInDB, DocMeetingID = id };
                return View(viewmodel);

            }
        }

        public JsonResult GetDocuments(int id)
        {
            using (ctx = new ApplicationDbContext())
            {
                var Docs = ctx.Documents.Where(m => m.MeetingID == id).Select(s => new { id = s.ID, name = s.Document_Name, url = s.Document_URL}).ToList();

                return Json(new { results = Docs }, JsonRequestBehavior.AllowGet);

            }


        }
        public ActionResult Viewer(int id = 1)
        {
            using (ctx = new ApplicationDbContext())

            {
                var meetingInDB = ctx.Meetings.Find(id);

                var MeetingInDBDocs = ctx.Meetings.Where(m => m.MeetingID == meetingInDB.MeetingID).SelectMany(s => s.Documents).ToList();

                var documentViewerVM = new DocumentViewerVM() { DocumentsAll = MeetingInDBDocs, Meeting = meetingInDB };

                return View(documentViewerVM);
            }

            
        }

        public ActionResult DocViewer(int id,int meeting_id)
        {
            using (ctx = new ApplicationDbContext())

            {
                var meetingInDB = ctx.Meetings.Find(meeting_id);
                var MeetingInDBDocs = ctx.Meetings.Where(m => m.MeetingID == meeting_id).SelectMany(s => s.Documents).ToList();

                var _DocumentInDB = ctx.Documents.Find(id);
                var Doc_Comments = ctx.Documents.Where(m => m.ID == id).SelectMany(s => s.Comments).ToList();

                var documentViewerVM = new DocumentViewerVM()
                { DocumentsAll = MeetingInDBDocs, Meeting = meetingInDB, _Doc_Comment = Doc_Comments, DocumentInDB = _DocumentInDB};

                return View(documentViewerVM);
            }


        }

        public async System.Threading.Tasks.Task<string> RoleChecker(string username, string roleName)
        {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(roleName))
                return "Invalid UserID and RoleID";


            var userID = "";
            using (var ctx = new ApplicationDbContext())
            {
                userID = ctx.Users.SingleOrDefault(s => s.UserName == username).Id;
            }
            var _context = new ApplicationDbContext();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            var isUserInRole = await UserManager.IsInRoleAsync(userID, roleName);

            if (isUserInRole == true)
                return "In Role";
            else return "Not in Role";


        }
        
        public ActionResult Edity(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (ctx = new ApplicationDbContext())
            {

                var MeetingIndb = ctx.Meetings.Find(id);
                

                var SelectedUserInDB = new HashSet<ApplicationUser>();
                var RemanantUsers = new List<ApplicationUser>();

                var meetingDocuments = (from u in ctx.Documents
                                        where u.MeetingID == MeetingIndb.MeetingID
                                        select u).ToList();

                var RegisteredUsersArray = (from u in ctx.Users
                                            where u.Meetings.Any(s => s.MeetingID == MeetingIndb.MeetingID)
                                            select u.UserName).ToArray();

                var AllUsers2 = ctx.Users.Any(s => RegisteredUsersArray.Contains(s.UserName));

                var AllUsers = ctx.Users.ToList();

                foreach (var item in AllUsers)
                {
                    if (RegisteredUsersArray.Contains(item.UserName))
                    {
                        SelectedUserInDB.Add(item);

                    }
                }

                foreach (var item in AllUsers)
                {
                    if (!(RegisteredUsersArray.Contains(item.UserName)))
                    {
                        RemanantUsers.Add(item);

                    }
                }

                ExactTime exact = new ExactTime();
                var viewmodel = new EditMeetingViewModel()
                {
                    Meeting_Id = MeetingIndb.MeetingID,
                    Title = MeetingIndb.Title,
                    Venue = MeetingIndb.Venue,
                    Time = MeetingIndb.DateTime.ToString("h:mm tt"),
                    Date = MeetingIndb.DateTime.ToString("dddd, dd MMMM yyyy"),
                    Userlist = RemanantUsers,
                    to = RegisteredUsersArray,
                    MeetingDocuments = meetingDocuments,
                    DocCounter = meetingDocuments.Count(),
                    selectedUSersInDB = SelectedUserInDB,
                    ExactTime = exact.GetLocations()
                    
                };
                return View("Edit", viewmodel);




            }




        }


        public string DeleteMeeting(int id)
        {
            using (ctx = new ApplicationDbContext())
            {
                var meetingInDB = ctx.Meetings.SingleOrDefault(s => s.MeetingID == id);
                var MeetingInDBDocsComments = ctx.Documents.Include(c => c.Comments).Where(s => s.MeetingID == meetingInDB.MeetingID)
                    .SelectMany(u => u.Comments);

                if (meetingInDB != null)
                {
                    meetingInDB.IsCancelled = true;

                    foreach (var item in  MeetingInDBDocsComments)
                    {
                        item.IsCancelled = true;
                    }

                    ctx.SaveChanges();
                    return "Meeting Notification Deleted!";

                }

                else
                {
                    return "Invalid Meeting Notification!";
                }

            }
        }

        [HttpPost]
        public ActionResult Edity(EditMeetingViewModel vm, List<HttpPostedFileBase> postedFiles)
        {
            bool UsertoBeUpdated;
            using (ctx = new ApplicationDbContext())
            {
                var meeting = ctx.Meetings.Include(g => g.ApplicationUsers).SingleOrDefault(s => s.MeetingID == vm.Meeting_Id);

                var RegisteredUsersArray = (from u in ctx.Users
                                            where u.Meetings.Any(s => s.MeetingID == meeting.MeetingID)
                                            select u.UserName).ToArray();

                if (RegisteredUsersArray != null)
                {
                    UsertoBeUpdated = FilterDocument(vm.to, RegisteredUsersArray, meeting);
                }

               
                meeting.Title = vm.Title;
                meeting.Venue = vm.Venue;
                meeting.DateTime = DateTime.Parse(String.Format("{0} {1}", vm.Date, vm.Time));

                if (postedFiles.Count > 0)
                {
                    UpdateFiles(meeting, postedFiles);
                }
             

                ctx.SaveChanges();
                return RedirectToAction("index", "Dashboard");
            }
           
        }

        private void UpdateFiles(Meeting meeting, List<HttpPostedFileBase> postedFiles)
        {
            string path = Server.MapPath("~/Docs/");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            IList<Document> newDocs = new List<Document>();
            foreach (HttpPostedFileBase postedFile in postedFiles)
            {
                if (postedFile != null)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    var extension = Path.GetExtension(postedFile.FileName);
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(postedFile.FileName);
                    var Id = Guid.NewGuid();
                    var ShortGuid = Id.ToString().Substring(0, 7);
                    postedFile.SaveAs(path + ShortGuid + "_" +fileName);
                    var filepath = ShortGuid + "_" + fileName;
                    newDocs.Add(new Document() { MeetingID = meeting.MeetingID, Document_Name = fileName, Document_URL = filepath });
                }

                ctx.Documents.AddRange(newDocs);

            }
        }

        private bool FilterDocument(string[] to, string[] registeredUsersArray, Meeting meeting)
        {
           

            if (meeting != null)
            {
                foreach (var item in registeredUsersArray)
                {
                    var user = ctx.Users.SingleOrDefault(s => s.UserName == item);
                    if(user != null)
                    {
                        meeting.ApplicationUsers.Remove(user);
                    }
                }



                foreach (var item in to)
                {
                    var user = ctx.Users.SingleOrDefault(s => s.Id == item);
                    if (user != null)
                    {
                        meeting.ApplicationUsers.Add(user);
                    }
                }
            }

            
            return true;
        }

        private void RemoveUserFromMeeting(string item)
        {
            
        }

        
    }


    class UserComment {

        public string username { get; set; }
        public string content{ get; set; }

        public string datetime { get; set; }
        public int id { get; internal set; }
    }


}