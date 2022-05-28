using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_TI.Models
{
    public partial class Usuario
    {
        public int CodigoEmpleado { get; set; }
        public string CorreoElectronico { get; set; } = null!;
        public string? NombreUsuario { get; set; }
        public string? ContrasenaUsuario { get; set; }
    }
}
