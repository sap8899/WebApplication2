using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Reservation
    {
        [Display(Name = "Number")]
        public int ReservationID { get; set; }
        
        public int UserID { get; set; }
        [Display(Name = "User Name")]
        public User user { get; set; }
        public int RestaurantID { get; set; }
        [Display(Name = "Restaurant")]

        public Restaurant Restaurant { get; set; }

        public int NumberOfPeople { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; }
    }
}
