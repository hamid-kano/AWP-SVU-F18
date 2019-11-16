using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class CommentsMovies
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
        public  int  Rating { get; set; }

        public int  MovieID { get; set; }
        public virtual Movie Movie { get; set; }

    }
}