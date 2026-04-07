using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Course : BaseEntity
    {
        public string CourseName { get; set; }

        public int TeacherId { get; set; }  
        public Teacher Teacher { get;set; }

        public ICollection<Lesson> Lessons { get; set; }    
    }
}
