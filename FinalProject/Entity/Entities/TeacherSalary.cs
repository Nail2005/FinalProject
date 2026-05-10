using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class TeacherSalary : BaseEntity 
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public decimal TotalLessonHours { get; set; }  
        public decimal GrossSalary { get; set; }      

        public int TotalLateMinutes { get; set; }
        public int TotalEarlyLeaveMinutes { get; set; }

        public decimal Deduction { get; set; }
        public decimal FinalSalary { get; set; }

        public DateTime Month { get; set; }

    }
}
