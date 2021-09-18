using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Enrollment
    {
            public int EnrollmentId { get; set; }
            public int CityId { get; set; }        
            public int RestaurantId { get; set; }
            public int? CategoryId { get; set; }
            
            public virtual City City { get; set; }
            public virtual Restaurant Restaurant { get; set; }
            [ForeignKey("CategoryId")]

            public virtual Category Category { get; set; }
    }
}
