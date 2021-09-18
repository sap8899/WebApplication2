using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
}
