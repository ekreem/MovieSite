using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.CORE.ViewModels
{
    public class CreateActorVm
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; } = string.Empty;
        public IFormFile PhotoFile { get; set; } // Aktyorun şəkli üçün
        public List<int> MovieIds { get; set; } = new List<int>(); // Seçilmiş filmlər
    }
}
