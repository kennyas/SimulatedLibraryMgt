using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimulatedLibraryMgt.Models;

namespace SimulatedLibraryMgt.Controllers
{
    [AuthorizeRoles(Constants.LibrarianRoleName, Constants.SupervisorRoleName)]
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        [AuthorizeRoles(Constants.LibrarianRoleName,Constants.SupervisorRoleName)]
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        [AuthorizeRoles(Constants.LibrarianRoleName, Constants.SupervisorRoleName)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var history = db.BorrowHistories
                    //.Where(borrowHistory => borrowHistory.CustomerId == 1)
                    .Where(borrowHistory => borrowHistory.CustomerId == id) //correction for returning same borrowal history in all customers detail page
                    .Include(b => b.Book)
                    .OrderByDescending(borrowHistory => borrowHistory.BorrowDate);
            //if(history.Count() > 0)
            //{
            //    //ViewBag.Book = db.Books.Where(x => x.BookId == history.FirstOrDefault().BookId);
            //    var book = db.Books.Find(history.FirstOrDefault().BookId);
            //    ViewBag.Book = book;
            //}
            
            ViewBag.BorrowHistoryLength = history.Count();
            ViewBag.BorrowHistory = history;
            return View(customer);
        }

        // GET: Customers/Create
        [AuthorizeRoles(Constants.SupervisorRoleName)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,Name,Address,Contact,NationalID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        [AuthorizeRoles(Constants.SupervisorRoleName)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,Name,Address,Contact,NationalID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        [AuthorizeRoles(Constants.SupervisorRoleName)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
