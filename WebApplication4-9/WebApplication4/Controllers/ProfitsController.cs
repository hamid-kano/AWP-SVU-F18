using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class ProfitsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profits
        [Authorize(Users = "Hamid")]
        public ActionResult Index()
        {
            return View(db.Profits.ToList());
            
        }

        // GET: Profits/Details/5
        [Authorize(Users = "Hamid")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profits profits = db.Profits.Find(id);
            if (profits == null)
            {
                return HttpNotFound();
            }
            return View(profits);
        }

        // GET: Profits/Create
        [Authorize(Users = "Hamid")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NameUser,MovieID,MovieName,DateTimeDownload,PriceMovie")] Profits profits)
        {
            if (ModelState.IsValid)
            {
                db.Profits.Add(profits);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profits);
        }

        // GET: Profits/Edit/5
        [Authorize(Users = "Hamid")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profits profits = db.Profits.Find(id);
            if (profits == null)
            {
                return HttpNotFound();
            }
            return View(profits);
        }

        // POST: Profits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NameUser,MovieID,MovieName,DateTimeDownload,PriceMovie")] Profits profits)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profits).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profits);
        }

        // GET: Profits/Delete/5
        [Authorize(Users = "Hamid")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profits profits = db.Profits.Find(id);
            if (profits == null)
            {
                return HttpNotFound();
            }
            return View(profits);
        }

        // POST: Profits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profits profits = db.Profits.Find(id);
            db.Profits.Remove(profits);
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
