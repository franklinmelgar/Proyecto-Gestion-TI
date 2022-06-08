using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_TI.Models
{
    public partial class ComentariosConsulta
    {
        public int CodigoComentario { get; set; }
        public int? CodigoConsulta { get; set; }
        public int? CodigoEmpleadoComentario { get; set; }
        public string? Comentario { get; set; }
        public DateTime? FechaComentario { get; set; }

        public virtual Consulta? CodigoConsultaNavigation { get; set; }
        public virtual Empleado? CodigoEmpleadoComentarioNavigation { get; set; }
    }
}
