using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_TI.Models
{
    public partial class Consulta
    {
        public Consulta()
        {
            ComentariosConsulta = new HashSet<ComentariosConsulta>();
        }

        public int CodigoConsulta { get; set; }
        public string? TituloConsulta { get; set; }
        public string? DescripcionConsulta { get; set; }
        public int? CodigoEmpleado { get; set; }
        public int? EstadoConsulta { get; set; }

        public DateTime? FechaConsulta { get; set; }

        public virtual Empleado? CodigoEmpleadoNavigation { get; set; }
        public virtual ICollection<ComentariosConsulta> ComentariosConsulta { get; set; }
    }
}
