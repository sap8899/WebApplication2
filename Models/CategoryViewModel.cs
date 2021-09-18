using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models
{
    public class CategoryViewModel
    {
        public List<Restaurant> Restaurants { get; set; }
        public SelectList Categories { get; set; }
        public string RestaurantCategory { get; set; }
        public string SearchString { get; set; }
    }
}
