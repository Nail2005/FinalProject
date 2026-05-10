using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.AuditLogDto
{
    public class AuditLogDto
    {
        public string Action { get; set; }      
        public string TableName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
