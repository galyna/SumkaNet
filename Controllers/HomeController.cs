using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Data;
using Core.Data.Repository.Interfaces;
using Core.Data.Repository;
using SumkaWeb.Models;


namespace SumkaWeb.Controllers
{
    public class HomeController : Controller
    {
        IHomeRepository _homeRepository;

        public HomeController() : this(new HomeRepository()) { }

        public HomeController(IHomeRepository homeRepository)
        {
            this._homeRepository = homeRepository;
        } 
        public ActionResult Index()
        {
            return View(new HomeModel(){Stores= _homeRepository.GetStores()});
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
