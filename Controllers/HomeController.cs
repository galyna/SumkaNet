using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Data;


namespace SumkaWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new NHibernateFiller();
            db.FillDB();
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
