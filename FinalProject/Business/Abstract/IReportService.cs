using Business.DTOs.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IReportService
    {
        Task<StudentMonthlyReportDto> GetStudentMonthlyReport(int studentId);
    }
}
