using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.CORE.Entities
{
    public class AppUser : IdentityUser
    {

       
        public string Name { get; set; }
        public string Surname { get; set; }
       
    }
}
