using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IAttendanceRepository : IGenericRepository<Attendance> 
    {
        Task<List<Attendance>> GetByTeacherAndMonthAsync(int teacherId, int month, int year);
    }
}
