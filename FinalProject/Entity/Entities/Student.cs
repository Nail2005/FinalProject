using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Student : BaseEntity
    {
        public string FullName { get; set; }    
        public int ParentId { get; set; }   
        public Parent Parent { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Attendance> Attendances { get; set; }    
        public ICollection<Guidance> Guidances { get; set; }    
        public ICollection<Payment> Payments { get; set; }   
    }

}
