using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_TI.Models
{
    public partial class VacacionesXempleado
    {
        public int CodigoEmpleado { get; set; }
        public int CodigoVacacion { get; set; }
        public int? DiasDisponibles { get; set; }

        public virtual Empleado CodigoEmpleadoNavigation { get; set; } = null!;
        public virtual Vacacione CodigoVacacionNavigation { get; set; } = null!;
    }
}
