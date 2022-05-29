using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_TI.Models
{
    public partial class TipoUsuario
    {
        public TipoUsuario()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int CodigoTipoUsuario { get; set; }
        public string? NombreTipoUsurio { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
