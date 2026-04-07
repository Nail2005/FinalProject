using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Parent : BaseEntity    
    {
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public string PhoneNumber { get; set; } 
        public ICollection<Student> Students { get; set; }  

    }
}
