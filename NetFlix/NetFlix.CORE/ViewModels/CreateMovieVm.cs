using Microsoft.AspNetCore.Http;
using NetFlix.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.CORE.ViewModels
{
    public class CreateMovieVm
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public List<int>? ActorsIds { get; set; }
        public int Year { get; set; }
        public int DurationMinutes { get; set; } 

        public double ImdbRating { get; set; } 

        public IFormFile File { get; set; }  

        public string TrailerUrl { get; set; } = string.Empty; 

        public string Quality { get; set; } = string.Empty; 
    }
}
