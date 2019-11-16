using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class Actors
    {

        public int Id { get; set; }
        public  string Name { get; set; }
        public string Image { get; set; }

        public virtual ICollection<MoviesToActors> MoviesToActors { get; set; }


    }
}