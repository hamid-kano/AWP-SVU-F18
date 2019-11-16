using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class MoviesToActors
    {
        public int MoviesToActorsID { get; set; }
        public int MoviesID { get; set; }
        public int ActorsID { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Actors  Actors { get; set; }
    }
}