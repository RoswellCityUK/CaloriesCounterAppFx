using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaloriesCounterAppFx.Models;

namespace CaloriesCounterAppFx.Controllers
{
    public class ContactFormController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id,From,Subject,Message")] ContactFormMessage contactFormMessage)
        {
            contactFormMessage.To = "admin@localhost";
            contactFormMessage.SendingDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                TempData["Success"] = "Message sent successfully! Congratulations!";
                db.ContactFormMessages.Add(contactFormMessage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMsg"] = "";

                foreach (var items in ModelState.Values)
                {
                    foreach (var er in items.Errors)
                    {

                        TempData["ErrorMsg"] = TempData["ErrorMsg"].ToString() + " " + er.ErrorMessage.ToString();
                    }

                }

                return View(contactFormMessage);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
