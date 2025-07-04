using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.CORE.ViewModels
{
    public class DashboardVm
    {
        public List<GetMovieVm> Movies { get; set; } = new List<GetMovieVm>();
        public List<GetActorVm> Actors { get; set; } = new List<GetActorVm>();
    }
}
