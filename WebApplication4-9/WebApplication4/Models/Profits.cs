using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class Profits
    {

        public int ID { get; set; }
        public string NameUser { get; set; }
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public DateTime DateTimeDownload { get; set; }
        public int PriceMovie { get; set; }
    }
}