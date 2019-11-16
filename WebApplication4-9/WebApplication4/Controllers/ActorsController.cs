using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class ActorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Actors
        public ActionResult Index()
        {
            return View(db.Actors.ToList());
        }

        // GET: Actors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actors actors = db.Actors.Find(id);
            if (actors == null)
            {
                return HttpNotFound();
            }

            //Start section DataMining :Who are actor’s friends

            try
            {

            AdomdConnection CON = new AdomdConnection("Data Source=ABOREDA; Initial catalog=MoviesDB");
            CON.Open();
            AdomdCommand COM = CON.CreateCommand();
            string s = @"SELECT Flattened  PREDICT([Actors], 5)  FROM CustomersMMAssociationActors
                        NATURAL PREDICTION JOIN (select (select '" + actors.Name.ToString() + "' as [Actor])  AS[Actors]) As T";

            COM.CommandText = s;
            List<string> listofNameActorsRelated = new List<string>();
            AdomdDataReader DR = COM.ExecuteReader();
            while (DR.Read())
            {
                if (DR[0] != null)
                {
                    listofNameActorsRelated.Add(DR[0].ToString());
                }
            }
            DR.Close();
            CON.Close();
            var Listactors = new List<Actors>();

            foreach (var item in db.Actors)
            {
                for (int i = 0; i < listofNameActorsRelated.Count; i++)
                {
                    if (item.Name.Equals(listofNameActorsRelated[i]))
                    {
                        Listactors.Add(item);
                    }
                }
            }

            ViewBag.ActorRelated = Listactors;
                ViewBag.listofNameActorsRelated = listofNameActorsRelated;
                ViewBag.ConnectionDataBase = true;
            }
            catch (Exception)
            {
                ViewBag.ConnectionDataBase = false;
                //  throw;
            }

            //End section DataMining :Who are actor’s friends



            return View(actors);
        }

        // GET: Actors/Create
        [Authorize(Users = "Hamid")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Actors actors, HttpPostedFileBase UploadImage)
        {
            if (ModelState.IsValid)
            {
                string pathImage = Path.Combine(Server.MapPath("~/ImageActors"), UploadImage.FileName);
                UploadImage.SaveAs(pathImage);
                actors.Image = UploadImage.FileName;              
                db.Actors.Add(actors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(actors);
        }

        // GET: Actors/Edit/5
        [Authorize(Users = "Hamid")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actors actors = db.Actors.Find(id);
            if (actors == null)
            {
                return HttpNotFound();
            }
            return View(actors);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Actors actors, HttpPostedFileBase UploadImage)
        {
            if (ModelState.IsValid)
            {
                string pathImage = Path.Combine(Server.MapPath("~/ImageActors"), UploadImage.FileName);
                UploadImage.SaveAs(pathImage);
                actors.Image = UploadImage.FileName;
                db.Entry(actors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(actors);
        }

        // GET: Actors/Delete/5
        [Authorize(Users = "Hamid")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actors actors = db.Actors.Find(id);
            if (actors == null)
            {
                return HttpNotFound();
            }
            return View(actors);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Actors actors = db.Actors.Find(id);
            db.Actors.Remove(actors);
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
