using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.CORE.ViewModels
{
    public class UpdateActorVm
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; } = string.Empty;
        public IFormFile PhotoFile { get; set; } // Yeni şəkil üçün (opsional)
        public string PhotoFileName { get; set; } = string.Empty; // Mövcud şəkil
        public List<int>? MovieIds { get; set; } = new List<int>(); // Seçilmiş filmlər
    }
}
