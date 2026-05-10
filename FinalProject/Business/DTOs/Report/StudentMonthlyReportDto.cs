using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Report
{
    public class StudentMonthlyReportDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public int TotalLessons { get; set; }
        public int PresentCount { get; set; }
        public int LateCount { get; set; }
        public int AbsentCount { get; set; }

        public int TotalLateMinutes { get; set; }
        public int TotalEarlyLeaveMinutes { get; set; }

        public decimal AverageScore { get; set; }
    }
}
