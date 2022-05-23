using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaloriesCounterAppFx.Controllers
{
    public class MyDiaryController : Controller
    {
        // GET: MyDiary
        public ActionResult Index()
        {
            return View();
        }
    }
}