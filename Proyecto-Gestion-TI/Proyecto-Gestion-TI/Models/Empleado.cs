using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_TI.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            ComentariosConsulta = new HashSet<ComentariosConsulta>();
            Consulta = new HashSet<Consulta>();
            SolicitudVacaciones = new HashSet<SolicitudVacacione>();
            VacacionesXempleados = new HashSet<VacacionesXempleado>();
        }

        public int CodigoEmpleado { get; set; }
        public string NombreEmpleado { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public DateTime FechaIngreso { get; set; }
        public decimal? Antiguedad { get; set; }
        public int? CodigoPuesto { get; set; }
        public int? CodigoJefe { get; set; }
        public int? CodigoDepartamento { get; set; }
        public int? CodigoTipoUsuario { get; set; }

        public virtual Departamento? CodigoDepartamentoNavigation { get; set; }
        public virtual Puesto? CodigoPuestoNavigation { get; set; }
        public virtual TipoUsuario? CodigoTipoUsuarioNavigation { get; set; }
        public virtual ICollection<ComentariosConsulta> ComentariosConsulta { get; set; }
        public virtual ICollection<Consulta> Consulta { get; set; }
        public virtual ICollection<SolicitudVacacione> SolicitudVacaciones { get; set; }
        public virtual ICollection<VacacionesXempleado> VacacionesXempleados { get; set; }

        public virtual ICollection<RutaAprobacion> RutaAprobacions { get; set; }
    }
}
