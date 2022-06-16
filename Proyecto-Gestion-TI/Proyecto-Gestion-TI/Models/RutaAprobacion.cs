using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_TI.Models
{
    public partial class RutaAprobacion
    {
        public RutaAprobacion()
        {
            AprobacionXsolicituds = new HashSet<AprobacionXsolicitud>();
        }

        public int CodigoRuta { get; set; }
        public string? Descripcion { get; set; }
        public int CodigoAprobador { get; set; }
        public int NivelAprobacion { get; set; }
        public int CodigoDepartamento { get; set; }

        public virtual Empleado CodigoAprobadorNavigation { get; set; } = null!;
        public virtual Departamento CodigoDepartamentoNavigation { get; set; } = null!;
        public virtual ICollection<AprobacionXsolicitud> AprobacionXsolicituds { get; set; }
    }
}