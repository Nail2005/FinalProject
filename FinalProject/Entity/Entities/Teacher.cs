using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Teacher : BaseEntity
    {
        public int UserId { get; set; }
        public AppUser User { get; set; }
        
        public decimal HourlySalary { get; set; }
        public int LateMinutes { get; set; }    
        public ICollection<Course> Courses { get; set; }    
    }
}
