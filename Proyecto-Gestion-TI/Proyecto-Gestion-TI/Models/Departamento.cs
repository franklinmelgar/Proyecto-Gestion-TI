using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_TI.Models
{
    public partial class Departamento
    {
        public Departamento()
        {
            Empleados = new HashSet<Empleado>();
            RutaAprobacions = new HashSet<RutaAprobacion>();
        }

        public int CodigoDepartamento { get; set; }
        public string? NombreDepartamento { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
        public virtual ICollection<RutaAprobacion> RutaAprobacions { get; set; }
    }
}
