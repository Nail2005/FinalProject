using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.AttendanceDto
{
    public class AttendanceDto
    {
        public int StudentId { get; set; }
        public int LessonId { get; set; }   
        public AttendanceStatus Status { get; set; }    
    }
}
