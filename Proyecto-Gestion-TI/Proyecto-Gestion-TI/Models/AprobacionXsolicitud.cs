using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_TI.Models
{
    public partial class AprobacionXsolicitud
    {
        public int CodigoRuta { get; set; }
        public int CodigoSolicitud { get; set; }
        public int? NivelAprobacion { get; set; }
        public string? Estado { get; set; }

        public virtual RutaAprobacion CodigoRutaNavigation { get; set; } = null!;
        public virtual SolicitudVacacione CodigoSolicitudNavigation { get; set; } = null!;
    }
}
