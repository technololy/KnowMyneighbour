using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KnowMyneighbour.Models
{
    public class VehicleDetails
    {
        public int Id { get; set; }
        public string VehicleType { get; set; }
        public string Make { get; set; }

        public string Model { get; set; }
        public string year { get; set; }
        //public int Id { get; set; }

        //[ForeignKey("StandardRefId")]
        //public Standard Standard { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}