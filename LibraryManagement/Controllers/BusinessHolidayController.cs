using SimulatedLibraryMgt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SimulatedLibraryMgt.Controllers
{
    [AuthorizeRoles(Constants.SupervisorRoleName)]
    public class BusinessHolidayController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BusinessHoliday
        public ActionResult Index()
        {
            var holidays = db.BusinessHolidays.ToList();
            return View(holidays);
        }

        // GET: BusinessHoliday/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessHoliday holiday = db.BusinessHolidays.Find(id);
            if (holiday == null)
            {
                return HttpNotFound();
            }

            return View(holiday);
        }


        // GET: BusinessHoliday/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusinessHoliday/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessHolidayId,HolidayOccassion,FromDate,ToDate,IsActive")] BusinessHoliday holiday)
        {
            if (ModelState.IsValid)
            {
                db.BusinessHolidays.Add(holiday);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(holiday);
        }

        // GET: BusinessHoliday/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessHoliday holiday = db.BusinessHolidays.Find(id);
            if (holiday == null)
            {
                return HttpNotFound();
            }
            return View(holiday);
        }

        // POST: BusinessHoliday/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessHolidayId,HolidayOccassion,FromDate,ToDate,IsActive")] BusinessHoliday holiday)
        {
            if (ModelState.IsValid)
            {
                db.Entry(holiday).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(holiday);
        }

        // GET: BusinessHoliday/Delete/5
        public ActionResult Delete(int? id)
        {
            return RedirectToAction("Index");
        }

        // POST: BusinessHoliday/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusinessHoliday holiday = db.BusinessHolidays.Find(id);
            db.BusinessHolidays.Remove(holiday);
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