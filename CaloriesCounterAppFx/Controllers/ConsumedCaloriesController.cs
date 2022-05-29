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
    public class ConsumedCaloriesController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ConsumedCalories
        public ActionResult Index()
        {
            return View(db.ConsumedCaloriesTable.ToList());
        }

        // GET: ConsumedCalories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumedCalories consumedCalories = db.ConsumedCaloriesTable.Find(id);
            if (consumedCalories == null)
            {
                return HttpNotFound();
            }
            return View(consumedCalories);
        }

        // GET: ConsumedCalories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConsumedCalories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Amount,DateAdded")] ConsumedCalories consumedCalories)
        {
            if (ModelState.IsValid)
            {
                db.ConsumedCaloriesTable.Add(consumedCalories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(consumedCalories);
        }

        // GET: ConsumedCalories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumedCalories consumedCalories = db.ConsumedCaloriesTable.Find(id);
            if (consumedCalories == null)
            {
                return HttpNotFound();
            }
            return View(consumedCalories);
        }

        // POST: ConsumedCalories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Amount,DateAdded")] ConsumedCalories consumedCalories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consumedCalories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consumedCalories);
        }

        // GET: ConsumedCalories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumedCalories consumedCalories = db.ConsumedCaloriesTable.Find(id);
            if (consumedCalories == null)
            {
                return HttpNotFound();
            }
            return View(consumedCalories);
        }

        // POST: ConsumedCalories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConsumedCalories consumedCalories = db.ConsumedCaloriesTable.Find(id);
            db.ConsumedCaloriesTable.Remove(consumedCalories);
            db.SaveChanges();
            return RedirectToAction("Index");
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
