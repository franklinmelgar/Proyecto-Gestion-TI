using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_TI.Models
{
    public partial class Vacacione
    {
        public Vacacione()
        {
            VacacionesXempleados = new HashSet<VacacionesXempleado>();
        }

        public int CodigoVacacion { get; set; }
        public int Anios { get; set; }
        public int DiaSegunAnio { get; set; }

        public virtual ICollection<VacacionesXempleado> VacacionesXempleados { get; set; }
    }
}
