using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Attendance : BaseEntity
    {
        public int StudentId { get; set; }  
        public Student Student { get; set; }

        public int LessonId { get; set; }   
        public Lesson Lesson { get; set; }

        public AttendanceStatus Status { get; set; }

        public DateTime JoinTime { get; set; }  
        public DateTime LeaveTime { get; set; }

        public int LateMinutes { get; set; }
        public int EarlyLeaveMinutes { get; set; }
        public int DurationMinutes { get; set; }   

    }
}
