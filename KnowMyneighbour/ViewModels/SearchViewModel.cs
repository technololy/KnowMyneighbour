using KnowMyneighbour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowMyneighbour.ViewModels
{
    public class SearchViewModel
    {
        public Trip trips { get; set; }
        public TripNetwork tripnetwork { get; set; }

        public City city { get; set; }
    }
}