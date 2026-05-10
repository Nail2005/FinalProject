using DAL.Abstract;
using DAL.Context;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IStudentRepository Students => new StudentRepository(_context);
        public IParentRepository Parents => new ParentRepository(_context);

        public ITeacherRepository Teachers => new TeacherRepository(_context);

        public IGenericRepository<Course> Courses => new GenericRepository<Course>(_context);

        public IGenericRepository<Lesson> Lessons => new GenericRepository<Lesson>(_context);

        public IAttendanceRepository Attendances => new AttendanceRepository(_context);

        public IGenericRepository<Payment> Payments => new GenericRepository<Payment>(_context);

        public IGenericRepository<Guidance> Guidances => new GenericRepository<Guidance>(_context);

        public IGenericRepository<Notification> Notifications => new GenericRepository<Notification>(_context);

        public IGenericRepository<OtpCode> OtpCodes => new GenericRepository<OtpCode>(_context);

        public IGenericRepository<TeacherSalary> TeacherSalaries => new GenericRepository<TeacherSalary>(_context);

        public IGenericRepository<AuditLog> AuditLogs => new GenericRepository<AuditLog>(_context);

      

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
