using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowMyneighbour.Models
{
    public class StuffsBuysAndSales
    {
        public int id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string BuyOrSell { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

        public ApplicationUserManager appUser { get; set; }
    }
}