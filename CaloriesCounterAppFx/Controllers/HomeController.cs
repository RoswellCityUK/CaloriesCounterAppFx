using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaloriesCounterAppFx.Controllers
{
    //Home controller for a static content pages
    //Tomasz Grabowski 22/05/2022
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            //Redirecting logged in User to My Diary Index view
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "MyDiary");
            }
            return View();
        }

        public ActionResult KnowledgeBase()
        {
            return View();
        }

        public ActionResult UnderstandCalories()
        {
            return View();
        }

        public ActionResult Podcasts()
        {
            return View();
        }

        public ActionResult HowTo()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}