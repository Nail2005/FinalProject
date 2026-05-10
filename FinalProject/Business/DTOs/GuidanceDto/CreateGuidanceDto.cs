using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.GuidanceDto
{
    public class CreateGuidanceDto
    {
        public int StudentId { get; set; }  
        public int LessonId { get; set; }
        public int Score { get; set; }  
        public string Note { get; set; }
    }
}
