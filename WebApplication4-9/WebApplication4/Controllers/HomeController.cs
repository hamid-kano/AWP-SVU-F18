using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var UserID = User.Identity.GetUserId();
            ViewBag.ConnectionDataBase = false;
            if (UserID !=null)
            {
                try
                {

                var user = db.Users.Where(a => a.Id == UserID).SingleOrDefault();
                    //section Data Mining for classifiction Customer selector movie;
                    var DataMiningConnection = ConfigurationManager.ConnectionStrings["DataMiningConnection"].ConnectionString;
                    // AdomdConnection con = new AdomdConnection("Data Source=ABOREDA; Initial catalog=MoviesDB");
                    AdomdConnection con = new AdomdConnection(DataMiningConnection);
                    con.Open();
                AdomdCommand com = new AdomdCommand();
                com.Connection = con;
                    //string s = @"select [Movie Selector] from ClassificationMM
                    //                   natural prediction join
                    //           (select '" + user.HomeOwnerShaip + @"'as [Home Ownership] ,
                    //                    '" + user.Gender + @"' as Gender,
                    //                    '" + user.Age + @"'as Age ,
                    //                    '" + user.MaritalStatus + @"'as [Marital Status],
                    //                    '" + user.NumChildren + @"'as [Num Children],
                    //                    '" + user.NumTVs + @"'as [Num TVs],
                    //                    '" + user.PPV_Freq + @"'as [PPV Freq],
                    //                    '" + user.RentingFreq + @"'as [Renting Freq],
                    //                    '" + user.TheaterFreq + @"'as [Theater Freq],
                    //                    '" + user.TV_MovieFreq + @"'as [TVMovie Freq],
                    //                    '" + user.TV_Signal + @"'as [TV Signal],
                    //                    '" + user.ViewigFreq + @"'as [Viewing Freq],
                    //                    '" + user.Format + @"'as [Format],
                    //                    '" + user.NumBathrooms + @"'as [Num Bathrooms],
                    //                    '" + user.NumBedrooms + @"'as [Num Bedrooms],
                    //                    '" + user.Buying_Freq + @"'as [Buying Freq],
                    //                    '" + user.EducationLevel + @"' as [Education Level],
                    //                    '" + user.InternetConnections + @"'as [Internet Connection],
                    //                    '" + user.NumCars + @"' as [Num Cars] )as t";


                string s = @"SELECT [ClassificationMM].[Movie Selector]
                        From  [ClassificationMM]
                        NATURAL PREDICTION JOIN
                        (SELECT '" + user.Age + @"' AS[Age],
                          '"+user.EducationLevel+ @"' AS[Education Level],
                          '"+user.Gender+ @"' AS[Gender],
                          '"+user.HomeOwnerShaip+ @"' AS[Home Ownership],
                          '"+user.MaritalStatus+ @"' AS[Marital Status],                          
                          '"+user.NumCars + @"' AS[Num Cars],
                          '"+user.NumChildren + @"' AS[Num Children]) AS t";

                    com.CommandText = s;
                AdomdDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    if (dr[0] != null)
                        ViewBag.ResultClassification = dr[0].ToString();
                }
                dr.Close();
                con.Close();
                //section Data Mining for classifiction Customer selector movie;

                //section Data Mining for Clustering Customer selector movie;
                con.Open();
                com.Connection = con;
                    //s = @"SELECT Cluster() FROM [ViewClusteringMM]
                    //                  NATURAL PREDICTION JOIN
                    //          (select '" + user.HomeOwnerShaip + @"'as [Home Ownership] ,
                    //                   '" + user.Gender + @"' as Gender,
                    //                   '" + user.Age + @"'as Age ,
                    //                   '" + user.MaritalStatus + @"'as [Marital Status],
                    //                   '" + user.NumChildren + @"'as [Num Children],
                    //                   '" + user.NumTVs + @"'as [Num TVs],
                    //                   '" + user.PPV_Freq + @"'as [PPV Freq],
                    //                   '" + user.RentingFreq + @"'as [Renting Freq],
                    //                   '" + user.TheaterFreq + @"'as [Theater Freq],
                    //                   '" + user.TV_MovieFreq + @"'as [TVMovie Freq],
                    //                   '" + user.TV_Signal + @"'as [TV Signal],
                    //                   '" + user.ViewigFreq + @"'as [Viewing Freq],
                    //                   '" + user.Format + @"'as [Format],
                    //                   '" + user.NumBathrooms + @"'as [Num Bathrooms],
                    //                   '" + user.NumBedrooms + @"'as [Num Bedrooms],
                    //                   '" + user.Buying_Freq + @"'as [Buying Freq],
                    //                   '" + user.EducationLevel + @"' as [Education Level],
                    //                   '" + user.InternetConnections + @"'as [Internet Connection],
                    //                   '" + user.NumCars + @"' as [Num Cars] )as t";

                    s = @"SELECT  Cluster() From  [_ViewClusteringMM] NATURAL PREDICTION JOIN
                        (SELECT '" + user.Age + @"' AS[Age],
                          '" + user.EducationLevel + @"' AS[Education Level],
                          '" + user.Gender + @"' AS[Gender],
                          '" + user.Hobbies + @"' AS[Hobby],
                          '" + user.HomeOwnerShaip + @"' AS[Home Ownership],
                          '" + user.MaritalStatus + @"' AS[Marital Status],
                          '" + user.NumCars + @"' AS[Num Cars],
                          '" + user.NumChildren + @"' AS[Num Children]) AS t";
                    com.CommandText = s;
                dr = com.ExecuteReader();
                string numberCluster = ""; ;
                if (dr.Read())
                {if (dr[0] != null) { numberCluster = dr[0].ToString().Substring(dr[0].ToString().Length - 1);}}
                dr.Close();
                con.Close();
                con.Open();
                com.Connection = con;
                s = "SELECT[Movie] FROM [_ViewClusteringMM].CASES WHERE IsInNode('" + numberCluster+ "')  order by Movie"; 
                com.CommandText = s;
                dr = com.ExecuteReader();
                List<string> listofNameMoviesClustering = new List<string>();                
                while (dr.Read())
                {
                    if (dr[0] != null)
                    {   if(!listofNameMoviesClustering.Contains(dr[0].ToString())) 
                        listofNameMoviesClustering.Add(dr[0].ToString());
                    }
                }
                dr.Close();
                con.Close();
                var movies = new List<Movie>();

                foreach (var item in db.Movies)
                {
                    for (int i = 0; i < listofNameMoviesClustering.Count; i++)
                    {
                        if (item.Name.Equals(listofNameMoviesClustering[i]))
                        {
                            movies.Add(item);
                        }
                    }
                }
                ViewBag.numberClusterofCustomer = numberCluster;    
                ViewBag.MovieClustering = movies;
                ViewBag.listofNameMoviesClustering = listofNameMoviesClustering;

                    //section Data Mining for Clustering Customer selector movie;
                    ViewBag.ConnectionDataBase = true;
                }
                catch (Exception)
                {
                    ViewBag.ConnectionDataBase = false;
                   
                }


            }

            return View(db.Categories.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Search(string search)
        {
            var result = db.Movies.Where(a =>a.Name.Contains(search) || a.Category.Name.Contains(search)
            || a.Director.Name.Contains(search)).ToList();            
          
            return View(result);

        }


        public ActionResult SearchAlphabetMovie(string searchAlphabetMovie)
        {
            var result = db.Movies.Where(a => a.Name.StartsWith(searchAlphabetMovie)).ToList();


            return View(result);
        }

        public ActionResult SearchAlphabetActor(string searchAlphabetActor)
        {
            var result = db.Actors.Where(a => a.Name.StartsWith(searchAlphabetActor)).ToList();


            return View(result);
        }
   

    }
}