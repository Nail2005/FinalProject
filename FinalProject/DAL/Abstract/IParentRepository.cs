using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IParentRepository : IGenericRepository<Parent>
    {
        Task<Parent?> GetWithUserAsync(string phoneNumber);
    }
}
