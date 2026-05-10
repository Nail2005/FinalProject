using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IUnitOfWork
    {
        IStudentRepository Students { get; }
        IParentRepository Parents { get; }
        ITeacherRepository Teachers { get; }

        IGenericRepository<Course> Courses { get; }
        IGenericRepository<Lesson> Lessons { get; }

        IAttendanceRepository Attendances { get; }
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<Guidance> Guidances { get; }

        IGenericRepository<Notification> Notifications { get; }
        IGenericRepository<OtpCode> OtpCodes { get; }

        IGenericRepository<TeacherSalary> TeacherSalaries { get; }
        IGenericRepository<AuditLog> AuditLogs { get; }

        Task<int> SaveAsync();
    }
}
