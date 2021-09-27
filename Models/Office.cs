using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Office
    {
        public int LocationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Office(int locid, string title, string desc, double latitude, double longitude)
        {
            this.LocationId = locid;
            this.Title = title;
            this.Description = desc;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }

    public class OfficeLists
    {
        public IEnumerable<Office> OfficeList { get; set; }
    }
}