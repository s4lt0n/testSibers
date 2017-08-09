using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test_Sibers.DB;

namespace test_Sibers.Controllers
{
    public class HomeController : Controller
    {
        //Test_ProjectsDBEntities db = new Test_ProjectsDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return RedirectToAction("Index", "Workers");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}