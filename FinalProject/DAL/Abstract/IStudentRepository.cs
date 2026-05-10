using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<Student> GetWithParentAsync(int id);
        Task<List<Student>> GetByParentIdAsync(int parentId);
    }
}
