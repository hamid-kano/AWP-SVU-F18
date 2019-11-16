using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AnalysisServices.AdomdClient;

namespace WebApplication4.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        int? Id_Movie_Download;

        // GET: Movies
        public ActionResult Index()
        {
            var movies = db.Movies.Include(m => m.Category).Include(m => m.Director);
            return View(movies.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
           

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.MovieID = id;
            // الافلام مشترك فيها الممثل
            var Results = from b in db.Actors
                          select new
                          {
                              b.Id,
                              b.Name,
                              Checked = ((
                              from ab in db.MoviesToActors
                              where (ab.MoviesID == id) & (ab.ActorsID == b.Id)
                              select ab).Count() > 0)
                          };

            var MyViewModel = new MoviesViewModel();
            MyViewModel.ID = id.Value;
            MyViewModel.Name = movie.Name;
            MyViewModel.Image = movie.Image;
            MyViewModel.LinkDownload = movie.LinkDownload;
            MyViewModel.Sight = movie.Sight;
            MyViewModel.Price = movie.Price;
            MyViewModel.CategoryId = movie.CategoryId;
            MyViewModel.DirectorId = movie.DirectorId;

              
           var MyCheckBoxList = new List<CheckBoxViewModel>();
            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel { ID = item.Id, Name = item.Name, Checked = item.Checked });
            }
            MyViewModel.Actors = MyCheckBoxList;


            ViewBag.MovieID = id.Value;

            var comments = db.CommentsMovies.Where(d => d.MovieID.Equals(id.Value)).ToList();
            ViewBag.Comments = comments;

            var CategoryName = db.Categories.Where(d => d.Id.Equals(movie.CategoryId)).ToList(); ;
            ViewBag.CategoryName = CategoryName;
            var DirectorName = db.Directors.Where(d => d.Id.Equals(movie.DirectorId)).ToList(); ;
            ViewBag.DirectorName = DirectorName;

            var ratings = db.CommentsMovies.Where(d => d.MovieID.Equals(id.Value)).ToList();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.Rating);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }

            //Start section DataMining :Customers Who Bought This Item Also Bought
            try
            {         

            AdomdConnection CON = new AdomdConnection("Data Source=ABOREDA; Initial catalog=MoviesDB");
            CON.Open();
            AdomdCommand COM = CON.CreateCommand();
            string s = @"SELECT Flattened  PREDICT([Movies], 25)  FROM CustomersMMAssociation
                        NATURAL PREDICTION JOIN (select (select '" + movie.Name.ToString() + "' as [Movie])  AS[Movies]) As T";

            COM.CommandText = s;
            List<string> listofNameMoviesRelated=new List<string>();
           AdomdDataReader DR = COM.ExecuteReader();
            while (DR.Read())
            {
                if (DR[0] != null)
                {
                    listofNameMoviesRelated.Add(DR[0].ToString());
                }
            }
            DR.Close();
            CON.Close();
            var movies = new List<Movie>();

            foreach (var item in db.Movies)
            {
                for (int i = 0; i < listofNameMoviesRelated.Count; i++)
                {
                    if (item.Name.Equals(listofNameMoviesRelated[i]))
                    {
                        movies.Add(item);
                    }
                }
            }

            ViewBag.MovieRelated = movies;
                ViewBag.ConnectionDataBase = true;
            }
            catch (Exception)
            {
                ViewBag.ConnectionDataBase = false;
                // throw;
            }

            //End section DataMining :Customers Who Bought This Item Also Bought

            return View(MyViewModel);
        }

        // GET: Movies/Create
        [Authorize(Users ="Hamid")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.DirectorId = new SelectList(db.Directors, "Id", "Name");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Movie movie , HttpPostedFileBase UploadImage ,HttpPostedFileBase UploadMovie)
        {
            if (ModelState.IsValid)
            {
                string pathImage = Path.Combine(Server.MapPath("~/Image"), UploadImage.FileName);
                UploadImage.SaveAs(pathImage);
                string pathMovie = Path.Combine(Server.MapPath("~/Movie"), UploadMovie.FileName);
                UploadImage.SaveAs(pathMovie);
                movie.LinkDownload = UploadMovie.FileName;
                movie.Image = UploadImage.FileName;
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", movie.CategoryId);
            ViewBag.DirectorId = new SelectList(db.Directors, "Id", "Name", movie.DirectorId);
            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize(Users = "Hamid")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", movie.CategoryId);
            ViewBag.DirectorId = new SelectList(db.Directors, "Id", "Name", movie.DirectorId);
            // الافلام مشترك فيها الممثل
            var Results = from b in db.Actors
                          select new
                          {
                              b.Id,
                              b.Name,
                              Checked = ((
                              from ab in db.MoviesToActors
                              where (ab.MoviesID == id) & (ab.ActorsID == b.Id)
                              select ab).Count() > 0)
                         };

            var MyViewModel = new MoviesViewModel();
            MyViewModel.ID = id.Value;
            MyViewModel.Name = movie.Name;
            MyViewModel.Image = movie.Image;
            MyViewModel.LinkDownload = movie.LinkDownload;
            MyViewModel.Sight = movie.Sight;
            MyViewModel.Price = movie.Price;
            MyViewModel.CategoryId = movie.CategoryId;
            MyViewModel.DirectorId = movie.DirectorId;

            var MyCheckBoxList = new List<CheckBoxViewModel>();
            foreach (var item in Results )
            {
                MyCheckBoxList.Add(new CheckBoxViewModel { ID = item.Id, Name = item.Name, Checked = item.Checked });
            }
            MyViewModel.Actors = MyCheckBoxList;

            return View(MyViewModel);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MoviesViewModel  movie, HttpPostedFileBase UploadImage, HttpPostedFileBase UploadMovie)
        {
            if (ModelState.IsValid)
            {
                var MyMovie = db.Movies.Find(movie.ID);
                string pathImage = Path.Combine(Server.MapPath("~/Image"), UploadImage.FileName);
                UploadImage.SaveAs(pathImage);
                string pathMovie = Path.Combine(Server.MapPath("~/Movie"), UploadMovie.FileName);
                UploadImage.SaveAs(pathMovie);
                MyMovie.Name = movie.Name;
                MyMovie.Image = UploadImage.FileName;
                MyMovie.LinkDownload = UploadMovie.FileName;
                MyMovie.Sight = movie.Sight;
                MyMovie.Price = movie.Price;
                MyMovie.CategoryId = movie.CategoryId;
                MyMovie.DirectorId = movie.DirectorId;

                foreach (var item in db.MoviesToActors )
                {
                    if (item.MoviesID==movie.ID)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
                foreach (var item in movie.Actors)
                {
                    if (item.Checked)
                    {
                        db.MoviesToActors.Add(new MoviesToActors() { MoviesID = movie.ID, ActorsID = item.ID });
                    }
                }
              
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", movie.CategoryId);
            ViewBag.DirectorId = new SelectList(db.Directors, "Id", "Name", movie.DirectorId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        [Authorize(Users = "Hamid")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            // الافلام مشترك فيها الممثل
            var Results = from b in db.Actors
                          select new
                          {
                              b.Id,
                              b.Name,
                              Checked = ((
                              from ab in db.MoviesToActors
                              where (ab.MoviesID == id) & (ab.ActorsID == b.Id)
                              select ab).Count() > 0)
                          };

            var MyViewModel = new MoviesViewModel();
            MyViewModel.ID = id.Value;
            MyViewModel.Name = movie.Name;
            MyViewModel.Image = movie.Image;
            MyViewModel.LinkDownload = movie.LinkDownload;
            MyViewModel.Sight = movie.Sight;
            MyViewModel.Price = movie.Price;
            MyViewModel.CategoryId = movie.CategoryId;
            MyViewModel.DirectorId = movie.DirectorId;

            var MyCheckBoxList = new List<CheckBoxViewModel>();
            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel { ID = item.Id, Name = item.Name, Checked = item.Checked });
            }
            MyViewModel.Actors = MyCheckBoxList;

            return View(MyViewModel);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            // حذف الارتباطات بجدل فيلم ممثل
            foreach (var item in db.MoviesToActors)
            {
                if (item.MoviesID == movie.ID)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
            }

            // حذف التعليقات بجدل تعليقات الفيلم
            foreach (var item in db.CommentsMovies)
            {
                if (item.MovieID == movie.ID)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
            }
            
            db.Movies.Remove(movie);
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

      
        public FileResult Download(string MovieName,int MovieId)
        {
            Movie MovieDownload_id = db.Movies.Where(m => m.ID == MovieId).ToList().First();
            try
            {           
            Profits profits = new Profits();
                profits.MovieID = MovieDownload_id.ID;
                profits.MovieName = MovieDownload_id.Name;
            profits.NameUser = User.Identity.Name ;
                profits.PriceMovie = MovieDownload_id.Price;
                profits.DateTimeDownload = DateTime.Now ;
            db.Profits.Add(profits);
            db.SaveChanges();

             }
            catch (Exception)
            {

                throw;
            }
            
            return File("~/Movie/" + MovieName, System.Net.Mime.MediaTypeNames.Application.Octet, MovieName);         
        }

        [Authorize]
        public ActionResult OrderMovie(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movies = db.Movies.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }

            var userId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == userId);
            ViewBag.UserName = currentUser.UserName.ToString();
            ViewBag.Credit_ID = currentUser.Credit_ID.ToString();
            ViewBag.Email = currentUser.Email.ToString();
            ViewBag.Password = currentUser.PasswordHash.ToString();
            Id_Movie_Download = id;
            return View(movies);

        }

    }
    }
