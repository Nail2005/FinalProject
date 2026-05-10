using DAL.Abstract;
using DAL.Context;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
    {
        private readonly AppDbContext _context;

        public AttendanceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Attendance>> GetByTeacherAndMonthAsync(int teacherId, int month, int year)
        {
            return await _context.Attendances
                .Include(x => x.Lesson)
                .ThenInclude(l => l.Course)
                .Where(x =>
                x.Lesson.Course.TeacherId == teacherId &&
                x.JoinTime.Month == month &&
                x.JoinTime.Year == year)
                .ToListAsync();
        }
    }
}
