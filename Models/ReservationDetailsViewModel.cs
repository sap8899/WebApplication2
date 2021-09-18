using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ReservationDetailsViewModel
    {
        public string RestaurantName { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int NumberOfPeople { get; set; }
    }
}
