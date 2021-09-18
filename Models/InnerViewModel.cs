using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class InnerViewModel
    {
        public string RestaurantName { get; set; }
        public IReadOnlyList<ReservationDetailsViewModel> Items { get; set; }
    }
}
