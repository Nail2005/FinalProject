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
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetByParentIdAsync(int parentId)
        {
            var data = await _context.Students.Where(x => x.ParentId == parentId).ToListAsync();
            return data;
        }

        public async Task<Student> GetWithParentAsync(int id)
        {
            return await _context.Students
                .Include(x => x.Parent)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
