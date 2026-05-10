using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.LessonDto
{
    public class CreateLessonDto
    {
        public int CourseId { get; set; }   
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; } 
    }
}
