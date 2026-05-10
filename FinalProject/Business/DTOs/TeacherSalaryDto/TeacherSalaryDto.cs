using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.TeacherSalaryDto
{
    public class TeacherSalaryDto
    {
        public int TeacherId { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal Deduction { get; set; }    
        public decimal FinalSalary { get; set; }    
    }
}
