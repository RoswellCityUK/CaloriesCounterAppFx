using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaloriesCounterAppFx.Controllers
{
    public class FoodCatalogueController : Controller
    {
        // GET: FoodCatalogue
        public ActionResult Index()
        {
            return View();
        }
    }
}