using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KnowMyneighbour.Models
{
    public class MoreUserDetails
    {
        public int id { get; set; }
        [DisplayName("Home")]
        public string HomeStreetAddress { get; set; }
        [DisplayName("City")]
        public string HomeCityName { get; set; }
        [DisplayName("State")]
        public string HomeLivingStateName { get; set; }
        [DisplayName("Work")]
        public string WorkStreetAddress { get; set; }
        public string WorkCityName { get; set; }
        public string WorkStateName { get; set; }
       
      
        public virtual ApplicationUser user { get; set; }


    }
}