using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.CORE.ViewModels
{
    public class LoginViewModel
    {

        [Required]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRememberMe { get; set; }
    }
}
