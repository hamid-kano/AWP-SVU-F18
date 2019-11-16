using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class MoviesViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Sight { get; set; }
        public int Price { get; set; }
        public string LinkDownload { get; set; }
        public string Image { get; set; }
        public   int CategoryId { get; set; }
        public virtual  Category Category { get; set; }
        public   int DirectorId { get; set; }
        public virtual  Director Director { get; set; }

        public  List<CheckBoxViewModel> Actors { get; set; }
        public virtual ICollection<CommentsMovies> CommentsMovies { get; set; }

    }
}