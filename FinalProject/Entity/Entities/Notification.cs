using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Notification : BaseEntity
    {
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }        
        public bool IsRead { get; set; }

    }
}
