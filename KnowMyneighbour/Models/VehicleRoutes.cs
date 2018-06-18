using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowMyneighbour.Models
{
    public class VehicleRoutes
    {
        public int id { get; set; }
        public string StartLocation { get; set; }
        public string DestinationLocation { get; set; }

    }
}