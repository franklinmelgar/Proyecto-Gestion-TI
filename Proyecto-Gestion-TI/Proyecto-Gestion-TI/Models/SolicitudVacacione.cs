using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_TI.Models
{
    public partial class SolicitudVacacione
    {
        public SolicitudVacacione()
        {
            AprobacionXsolicituds = new HashSet<AprobacionXsolicitud>();
        }

        public int CodigoSolicitud { get; set; }
        public string? DescripcionSolicitud { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public int? CantidadDias { get; set; }
        public int CodigoEmpleado { get; set; }

        public virtual Empleado CodigoEmpleadoNavigation { get; set; } = null!;
        public virtual ICollection<AprobacionXsolicitud> AprobacionXsolicituds { get; set; }
    }
}
