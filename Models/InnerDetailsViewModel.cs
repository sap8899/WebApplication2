using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class InnerDetailsViewModel
    {
        public string RestaurantName { get; set; }
        public string UserName { get; set; }
        public int NumberOfPeople { get; set; }
        public String Category { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
    }
}
