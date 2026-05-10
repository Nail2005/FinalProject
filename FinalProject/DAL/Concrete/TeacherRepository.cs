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
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        private readonly AppDbContext _context;

        public TeacherRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Teacher>> GetAllWithUserAsync()
        {
            return await _context.Teachers
                .Include(x => x.User)
                .ToListAsync();
        }
    }
}
