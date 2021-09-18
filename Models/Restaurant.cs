using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        public String Category { get; set; }
        public String City { get; set; }
        public string Address { get; set; }
        public Manager Manager { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
