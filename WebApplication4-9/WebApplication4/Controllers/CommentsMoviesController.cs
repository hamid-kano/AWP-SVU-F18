using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class CommentsMoviesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CommentsMovies
        [Authorize(Users = "Hamid")]
        public ActionResult Index()
        {
            var commentsMovies = db.CommentsMovies.Include(c => c.Movie);
            return View(commentsMovies.ToList());
        }

        // GET: CommentsMovies/Details/5
        [Authorize(Users = "Hamid")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentsMovies commentsMovies = db.CommentsMovies.Find(id);
            if (commentsMovies == null)
            {
                return HttpNotFound();
            }
            return View(commentsMovies);
        }

        // GET: CommentsMovies/Create
        public ActionResult Create()
        {
            ViewBag.MovieID = new SelectList(db.Movies, "ID", "Name");
            return View();
        }

        // POST: CommentsMovies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]       
        public ActionResult Create( CommentsMovies commentsMovies,int MovieID)
        {
            if (ModelState.IsValid)
            {
               if( User.Identity.Name=="")
                {
                    return Redirect("~/Account/Login");
                }
                else
                { 
                commentsMovies.UserName = User.Identity.Name;
                commentsMovies.DateTime = DateTime.Now;
                db.CommentsMovies.Add(commentsMovies);
                db.SaveChanges();
                return Redirect("~/Movies/Details/"+ MovieID.ToString());
                }
            }

            ViewBag.MovieID = new SelectList(db.Movies, "ID", "Name", commentsMovies.MovieID);
            return View(commentsMovies);
        }

        // GET: CommentsMovies/Edit/5
        [Authorize(Users = "Hamid")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentsMovies commentsMovies = db.CommentsMovies.Find(id);
            if (commentsMovies == null)
            {
                return HttpNotFound();
            }
            ViewBag.MovieID = new SelectList(db.Movies, "ID", "Name", commentsMovies.MovieID);
            return View(commentsMovies);
        }

        // POST: CommentsMovies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Comment,DateTime,Rating,MovieID")] CommentsMovies commentsMovies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentsMovies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MovieID = new SelectList(db.Movies, "ID", "Name", commentsMovies.MovieID);
            return View(commentsMovies);
        }

        // GET: CommentsMovies/Delete/5
        [Authorize(Users = "Hamid")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentsMovies commentsMovies = db.CommentsMovies.Find(id);
            if (commentsMovies == null)
            {
                return HttpNotFound();
            }
            return View(commentsMovies);
        }

        // POST: CommentsMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommentsMovies commentsMovies = db.CommentsMovies.Find(id);
            db.CommentsMovies.Remove(commentsMovies);
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
