using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class AuditLog : BaseEntity  
    {
        public string Action { get; set; }  
        public string TableName { get; set; }
        public string UserEmail { get; set; }
        public DateTime ActionDate { get; set; }
    }
}
