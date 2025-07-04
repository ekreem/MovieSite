using NetFlix.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.CORE.ViewModels
{
        public class GetMovieVm
        {
        public int Id { get; set; }
            public string Title { get; set; } = string.Empty;

            public string Category { get; set; } = string.Empty; // Tür

            public string Description { get; set; } = string.Empty; // Film özeti

            public string Director { get; set; } = string.Empty;

            public List<Actor> Actors { get; set; }  // Oyuncular

            public int Year { get; set; } // Yayın yılı

            public int DurationMinutes { get; set; } // Film süresi dakika cinsinden

            public double ImdbRating { get; set; } // IMDb puanı, örn: 8.3

            public string PosterFileName { get; set; } = string.Empty;  // GUID ile dosya adı tutulacak

            public string TrailerUrl { get; set; } = string.Empty; // Fragman linki
        public int TmdbId { get; set; }

            public string Quality { get; set; } = string.Empty; // 1080p, 4K, vb.
        }
    }
