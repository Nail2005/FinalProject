using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Payment : BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }   
        public DateTime PaidDate { get; set; }

        public bool IsPaid { get; set; }    
    }
}
