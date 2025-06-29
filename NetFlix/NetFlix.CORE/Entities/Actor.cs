using NetFlix.CORE.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.CORE.Entities
{
    public class Actor : BaseEntity
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Imgurl { get; set; }

        // Oxunur, databazada saxlanmaz
        public int Age => DateTime.Now.Year - BirthDate.Year;

        public ICollection<Movie>? Movies { get; set; }

    }
}
