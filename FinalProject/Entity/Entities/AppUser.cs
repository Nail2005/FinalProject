using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; }    
        public bool IsActive { get; set; } = false;

        public Parent Parent { get; set; }  
        public Teacher Teacher { get; set; }    

    }
}
