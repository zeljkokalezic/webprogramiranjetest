using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult TestAction()
        {
            //ViewBag.Message = "Your application description page is bla bla.";

            string[] months = { "January", "February", "March", "April", "May", "June", "July",
                        "August", "September", "October", "November", "December"};

            var shortNames = months.Where(name => name.Length < 6)
                                .OrderBy(name => name.Length)
                                .Select(name => name);

            return Content(String.Join(",",shortNames));

            //return Json(new { Pera = 10, Djoka = "TestString"} , JsonRequestBehavior.AllowGet);
        }

        public ActionResult RouteTest(int year, string category)
        {
            return Content(string.Format("{0}/{1}", year, category));
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}