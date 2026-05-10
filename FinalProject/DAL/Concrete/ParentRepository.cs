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
    public class ParentRepository : GenericRepository<Parent> ,IParentRepository
    {
        private readonly AppDbContext _context;

        public ParentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Parent?> GetWithUserAsync(string phoneNumber)
        {
            return await _context.Parents
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        }
    }
}
