using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.CORE.ViewModels
{
    public class UpdateMovieVm
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public List<int> ActorsIds { get; set; } = new List<int>();
        public int Year { get; set; }
        public int DurationMinutes { get; set; }
        public double ImdbRating { get; set; }
        public IFormFile File { get; set; }
        public string? PosterFileName { get; set; } = string.Empty;
        public string TrailerUrl { get; set; } = string.Empty;
        public string Quality { get; set; } = string.Empty;
    }
}
