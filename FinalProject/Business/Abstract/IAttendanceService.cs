using Business.DTOs.AttendanceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAttendanceService
    {
        Task MarkAsync(AttendanceDto dto);
        Task LeaveAsync(int attendanceId);
        Task<List<AttendanceDto>> GetByStudentAsync(int userId);
    }
}
