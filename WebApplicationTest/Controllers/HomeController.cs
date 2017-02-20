using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = Role.ADMIN)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();

            #region Comments
            //ViewBag.Message = "Your application description page is bla bla.";

            //string[] months = { "January", "February", "March", "April", "May", "June", "July",
            //            "August", "September", "October", "November", "December"};

            //var shortNames = months.Where(name => name.Length < 6)
            //                    .OrderBy(name => name.Length)
            //                    .Select(name => name);

            //return Content(String.Join(",",shortNames));

            //return Json(new { Pera = 10, Djoka = "TestString"} , JsonRequestBehavior.AllowGet);
            #endregion
        }

        public ActionResult TestAction()
        {
            throw new Exception("Error");

            ViewBag.Person = new Student() { Name = "Zeljko" };
            ViewData["Message"] = "From Controller";
            ViewData["Person"] = new Person() { Name = "Djole" }; //"Mile";//new Student() { Name = "Perica" };

            return View();
        }

        public ActionResult TestActionModel()
        {
            var model = new Student() { Name = "Perica" };
            return View("TestModelView", model);
        }

        public ActionResult RouteTest(int? year, string category = "web programiranje")
        {
            return Content(string.Format("{0}/{1}", year, category));
        }

        [Route("FreeWebProgrammingCourse/{year:int}/{category:length(1,5)}", Order = 2)]
        [Route("FreeProgrammingCourse/{year:int}/{category?}", Order = 1)]
        public ActionResult AttributeRouteTest(int year, string category)
        {
            return Content(string.Format("{0}/{1}", year, category));
        }

        public ActionResult ModelBinderTest([Bind(Include = "Id,Name,YearOfBirth,DateOfBirth")] Person person)
        {
            return Json(person, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FileResult()
        {
            return File(Server.MapPath("~/Content/Images/logo.png"), MimeMapping.GetMimeMapping("logo.png"));
        }

        public ActionResult DownloadFileResult()
        {
            var filename = "logo.png";
            return File(Server.MapPath("~/Content/Images/" + filename), MimeMapping.GetMimeMapping(filename), filename);
        }

        public ActionResult Contact()
        {
            if (User.IsInRole(Role.ADMIN))
            {
                string currentUserId = User.Identity.GetUserId();
                Administrator currentUser = db.Administrators.FirstOrDefault(x => x.Id == currentUserId);

                ViewBag.Message = "User is ADMIN, admin property is: " + currentUser.AdministratorProperty;
            }
            else
            {
                ViewBag.Message = "Your application contact page.";
            }

            return View();
        }
    }
}