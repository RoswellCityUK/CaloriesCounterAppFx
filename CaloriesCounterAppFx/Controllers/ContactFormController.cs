using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaloriesCounterAppFx.Models;
using Microsoft.AspNet.Identity;

namespace CaloriesCounterAppFx.Controllers
{
    public class ContactFormController : BaseController
    {
        //Controller for COntact Form
        //Messages Validated and saved in database for future retrieval
        //Tomasz Grabowski 22/05/2022

        private ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Method to retrived sended message via POST method
        public ActionResult Index([Bind(Include = "Id,From,Subject,Message,RegisterForNewsletter")] ContactFormMessage contactFormMessage)
        {
            //Defining needed variables
            contactFormMessage.To = "admin@localhost";
            contactFormMessage.SendingDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                TempData["Success"] = "Message sent successfully! Congratulations!";
                db.ContactFormMessages.Add(contactFormMessage);

                //In case User is Registered User saving in database information that he wants to be registered for newsletter
                if (!String.IsNullOrEmpty(User.Identity.GetUserId()) && contactFormMessage.RegisterForNewsletter)
                {
                    var currentId = User.Identity.GetUserId();
                    db.Users.FirstOrDefault(u => u.Id == currentId).IsRegisteredForNewsletter = true;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                //Errors handling
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
