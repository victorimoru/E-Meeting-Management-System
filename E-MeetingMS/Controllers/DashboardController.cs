using E_MeetingMS.Models;
using E_MeetingMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

namespace E_MeetingMS.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]

        [Authorize(Roles="SuperAdmin")]
    public class DashboardController : Controller
    {
        public List<Role> roles;



        // GET: Dashboard
        public JsonResult Me()
        {
            return Json(new
            {
                name = "victor",
                school = "Korecome College!"

            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ViewUserRoles()
        {

            using (var ctx = new ApplicationDbContext())
            {

                // var userRoles = ctx.Users.Where(u => u.Roles.Any(r => r.UserId == "40e0ce4e-fca6-4aff-9657"))
                var UserWithSuperAadminRoleInDB = (from u in ctx.Users
                                                   where u.Roles.Any(s => s.RoleId == "40e0ce4e-fca6-4aff-9657-5dbd224f6a44")
                                                   select new { username = u.UserName, role = "SuperAdmin" }).ToList();
                return Json(new { results = UserWithSuperAadminRoleInDB }, JsonRequestBehavior.AllowGet);

            }
        }




        public ActionResult Index()
        {
            using (var ctx = new ApplicationDbContext())
            {



                var rolesInDB = ctx.Roles.ToList();
                var vm = new DashboardViewModel()
                {
                    Meetings = ctx.Meetings.Where(s => s.IsCancelled == false)
                                           .Include(d => d.Documents)         
                                           .ToList(),
                    Users = ctx.Users.ToList(),
                    Roles = rolesInDB,
                    Comments = ctx.Comments.Where(c => c.IsCancelled == false).ToList()


                };
                return View("Index_Dashboard", vm);
            }

        }

        public async System.Threading.Tasks.Task<string> AssignRole(string userid, string roleid)
        {

            if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(roleid))
                return "Invalid UserID and RoleID";

            ApplicationUser UserInDB;

            using (var ctx = new ApplicationDbContext())
            {
                UserInDB = ctx.Users.Find(userid);
            }
            var _context = new ApplicationDbContext();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            var isUserInRole = await UserManager.IsInRoleAsync(userid, roleid);

            if (isUserInRole)
            {
                return $"{UserInDB.UserName} has already been assigned to {roleid} role.";
            }

            else
            {
                await UserManager.AddToRoleAsync(userid, roleid);

                return $"{UserInDB.UserName} has been assigned to {roleid} role.";
            }


        }


        public async System.Threading.Tasks.Task<string> DeleteUserFromRole(string userid, string roleid)
        {

            if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(roleid))
                return "Invalid UserID and RoleID";

            if (roleid == "Ordinary")
                return "You cannot remove a user from Ordinary role";

            ApplicationUser UserInDB;

            using (var ctx = new ApplicationDbContext())
            {
                UserInDB = ctx.Users.Find(userid);
            }
            var _context = new ApplicationDbContext();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            var isUserInRole = await UserManager.IsInRoleAsync(userid, roleid);

            if (isUserInRole)
            {
                await UserManager.RemoveFromRoleAsync(userid, roleid);
                return "Success!";
            }

            else
            {

                return $"{UserInDB.UserName} his not in  {roleid} role.";
            }


        }



        public async Task<string> CreateUser(string username, string password, string role)
        {
            role = "Ordinary";
            if ((string.IsNullOrEmpty(username) || (string.IsNullOrEmpty(password))))
            {
                return "Invalid UserName and/or Password";
            }


            else
            {
                var _context = new ApplicationDbContext();
                var user = new ApplicationUser { UserName = username, Email = username + "@gmail.com" };

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

                
                var result = await UserManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    switch (role)
                    {
                        case "Ordinary":
                            var outcome = await AssignUsertoRole(user.Id, role, UserManager);
                            return "Success";

                        case "SuperAdmin":
                            var outcome2 = await AssignUsertoRole(user.Id, role, UserManager);
                            return "Success";

                        default:
                            return "Failed";
                    }
                  
                   


                }
                else
                {
                    return "Failed";
                }
            }

           

        }

        private async Task<bool> AssignUsertoRole(string id, string v, UserManager<ApplicationUser> userManager)
        {
            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            if(!roleManager.RoleExists(v))
            {
                await roleManager.CreateAsync(new IdentityRole(v));
            }
           
            var result = userManager.AddToRoleAsync(id, v);

            if (result.IsCompleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}