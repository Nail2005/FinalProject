using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Lesson : BaseEntity
    {
        public int CourseId { get; set; }   
        public Course Course { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public ICollection<Attendance> Attendances { get; set; }    
        public ICollection<Guidance> Guidances { get; set; }


    }
}
