using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_TI.Models
{
    public partial class Puesto
    {
        public Puesto()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int CodigoPuesto { get; set; }
        public string? NombrePuesto { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
