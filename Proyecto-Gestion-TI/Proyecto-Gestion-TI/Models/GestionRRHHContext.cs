using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Proyecto_Gestion_TI.Models
{
    public partial class GestionRRHHContext : DbContext
    {
        public GestionRRHHContext()
        {
        }

        public GestionRRHHContext(DbContextOptions<GestionRRHHContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AprobacionXsolicitud> AprobacionXsolicituds { get; set; } = null!;
        public virtual DbSet<ComentariosConsulta> ComentariosConsulta { get; set; } = null!;
        public virtual DbSet<Consulta> Consulta { get; set; } = null!;
        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Empleado> Empleado { get; set; } = null!;
        public virtual DbSet<Puesto> Puestos { get; set; } = null!;
        public virtual DbSet<RutaAprobacion> RutaAprobacions { get; set; } = null!;
        public virtual DbSet<SolicitudVacacione> SolicitudVacaciones { get; set; } = null!;
        public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; } = null!;
        public virtual DbSet<Vacacione> Vacaciones { get; set; } = null!;
        public virtual DbSet<VacacionesXempleado> VacacionesXempleados { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-7EK03LQ\\SQLEXPRESS; Database=Gestion-RRHH;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AprobacionXsolicitud>(entity =>
            {
                entity.HasKey(e => new { e.CodigoRuta, e.CodigoSolicitud })
                    .HasName("PK__Aprobaci__062246BB809F8F84");

                entity.ToTable("AprobacionXSolicitud");

                entity.Property(e => e.CodigoRuta).HasColumnName("codigoRuta");

                entity.Property(e => e.CodigoSolicitud).HasColumnName("codigoSolicitud");

                entity.Property(e => e.Estado)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.NivelAprobacion).HasColumnName("nivelAprobacion");

                entity.HasOne(d => d.CodigoRutaNavigation)
                    .WithMany(p => p.AprobacionXsolicituds)
                    .HasForeignKey(d => d.CodigoRuta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AprobacionXSolicitud_RutaAprobacion_fk");

                entity.HasOne(d => d.CodigoSolicitudNavigation)
                    .WithMany(p => p.AprobacionXsolicituds)
                    .HasForeignKey(d => d.CodigoSolicitud)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AprobacionXSolicitud_SolicitudVacaciones_fk");
            });

            modelBuilder.Entity<ComentariosConsulta>(entity =>
            {
                entity.HasKey(e => e.CodigoComentario);

                entity.Property(e => e.CodigoComentario).HasColumnName("codigoComentario");

                entity.Property(e => e.CodigoConsulta).HasColumnName("codigoConsulta");

                entity.Property(e => e.CodigoEmpleadoComentario).HasColumnName("codigoEmpleadoComentario");

                entity.Property(e => e.Comentario)
                    .IsUnicode(false)
                    .HasColumnName("comentario");

                entity.Property(e => e.FechaComentario)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaComentario");

                entity.HasOne(d => d.CodigoConsultaNavigation)
                    .WithMany(p => p.ComentariosConsulta)
                    .HasForeignKey(d => d.CodigoConsulta)
                    .HasConstraintName("FK_ComentariosConsulta_Consulta");

                entity.HasOne(d => d.CodigoEmpleadoComentarioNavigation)
                    .WithMany(p => p.ComentariosConsulta)
                    .HasForeignKey(d => d.CodigoEmpleadoComentario)
                    .HasConstraintName("FK_ComentariosConsulta_Empleado");
            });

            modelBuilder.Entity<Consulta>(entity =>
            {
                entity.HasKey(e => e.CodigoConsulta);

                entity.Property(e => e.CodigoConsulta).HasColumnName("codigoConsulta");

                entity.Property(e => e.CodigoEmpleado).HasColumnName("codigoEmpleado");

                entity.Property(e => e.DescripcionConsulta)
                    .IsUnicode(false)
                    .HasColumnName("descripcionConsulta");

                entity.Property(e => e.EstadoConsulta).HasColumnName("estadoConsulta");

                entity.Property(e => e.FechaConsulta)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaConsulta");

                entity.Property(e => e.TituloConsulta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tituloConsulta");

                entity.HasOne(d => d.CodigoEmpleadoNavigation)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.CodigoEmpleado)
                    .HasConstraintName("FK_Consulta_Empleado");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.CodigoDepartamento)
                    .HasName("PK__Departam__7884392395DC88E7");

                entity.ToTable("Departamento");

                entity.Property(e => e.CodigoDepartamento).HasColumnName("codigoDepartamento");

                entity.Property(e => e.NombreDepartamento)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombreDepartamento");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.CodigoEmpleado)
                    .HasName("PK__Empleado__18674A5B42B4F277");

                entity.ToTable("Empleado");

                entity.HasIndex(e => e.Dni, "UQ__Empleado__D87608A70A1C03AD")
                    .IsUnique();

                entity.Property(e => e.CodigoEmpleado).HasColumnName("codigoEmpleado");

                entity.Property(e => e.Antiguedad)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("antiguedad");

                entity.Property(e => e.CodigoDepartamento).HasColumnName("codigoDepartamento");

                entity.Property(e => e.CodigoJefe).HasColumnName("codigoJefe");

                entity.Property(e => e.CodigoPuesto).HasColumnName("codigoPuesto");

                entity.Property(e => e.CodigoTipoUsuario).HasColumnName("codigoTipoUsuario");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contrasena");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Dni)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("dni");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaIngreso");

                entity.Property(e => e.NombreEmpleado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombreEmpleado");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.CodigoDepartamentoNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.CodigoDepartamento)
                    .HasConstraintName("Empleado_Departamento_fk");

                entity.HasOne(d => d.CodigoPuestoNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.CodigoPuesto)
                    .HasConstraintName("Empleado_Puesto_fk");

                entity.HasOne(d => d.CodigoTipoUsuarioNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.CodigoTipoUsuario)
                    .HasConstraintName("Empleado_Tipo_Usuario_fk");
            });

            modelBuilder.Entity<Puesto>(entity =>
            {
                entity.HasKey(e => e.CodigoPuesto)
                    .HasName("PK__Puesto__171E43991F8AF1E0");

                entity.ToTable("Puesto");

                entity.Property(e => e.CodigoPuesto).HasColumnName("codigoPuesto");

                entity.Property(e => e.NombrePuesto)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombrePuesto");
            });

            modelBuilder.Entity<RutaAprobacion>(entity =>
            {
                entity.HasKey(e => e.CodigoRuta)
                    .HasName("PK__RutaApro__55175D000BA3BFAC");

                entity.ToTable("RutaAprobacion");

                entity.Property(e => e.CodigoRuta).HasColumnName("codigoRuta");

                entity.Property(e => e.CodigoAprobador).HasColumnName("codigoAprobador");

                entity.Property(e => e.CodigoDepartamento).HasColumnName("codigoDepartamento");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.NivelAprobacion).HasColumnName("nivelAprobacion");

                entity.HasOne(d => d.CodigoAprobadorNavigation)
                    .WithMany(p => p.RutaAprobacions)
                    .HasForeignKey(d => d.CodigoAprobador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RutaAprobacion_Empleado");

                entity.HasOne(d => d.CodigoDepartamentoNavigation)
                    .WithMany(p => p.RutaAprobacions)
                    .HasForeignKey(d => d.CodigoDepartamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RutaAprobacion_Departamento_fk");
            });

            modelBuilder.Entity<SolicitudVacacione>(entity =>
            {
                entity.HasKey(e => e.CodigoSolicitud)
                    .HasName("PK__Solicitu__3351BBB49F917F46");

                entity.Property(e => e.CodigoSolicitud).HasColumnName("codigoSolicitud");

                entity.Property(e => e.CantidadDias)
                    .HasColumnName("cantidadDias")
                    .HasComputedColumnSql("(CONVERT([int],[fechaFinal])-CONVERT([int],[fechaInicial]))", false);

                entity.Property(e => e.CodigoEmpleado).HasColumnName("codigoEmpleado");

                entity.Property(e => e.DescripcionSolicitud)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcionSolicitud");

                entity.Property(e => e.FechaFinal)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaFinal");

                entity.Property(e => e.FechaInicial)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaInicial");

                entity.HasOne(d => d.CodigoEmpleadoNavigation)
                    .WithMany(p => p.SolicitudVacaciones)
                    .HasForeignKey(d => d.CodigoEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SolicitudVacaciones_Empleado");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.CodigoTipoUsuario)
                    .HasName("PK__TipoUsua__3716EE2EC5161979");

                entity.ToTable("TipoUsuario");

                entity.Property(e => e.CodigoTipoUsuario).HasColumnName("codigoTipoUsuario");

                entity.Property(e => e.NombreTipoUsurio)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombreTipoUsurio");
            });

            modelBuilder.Entity<Vacacione>(entity =>
            {
                entity.HasKey(e => e.CodigoVacacion)
                    .HasName("PK__Vacacion__851BD3BFD07F7B6D");

                entity.Property(e => e.CodigoVacacion).HasColumnName("codigoVacacion");

                entity.Property(e => e.Anios).HasColumnName("anios");

                entity.Property(e => e.DiaSegunAnio).HasColumnName("diaSegunAnio");
            });

            modelBuilder.Entity<VacacionesXempleado>(entity =>
            {
                entity.HasKey(e => new { e.CodigoEmpleado, e.CodigoVacacion })
                    .HasName("PK__Vacacion__F036F760AB443BC3");

                entity.ToTable("VacacionesXEmpleado");

                entity.Property(e => e.CodigoEmpleado).HasColumnName("codigoEmpleado");

                entity.Property(e => e.CodigoVacacion).HasColumnName("codigoVacacion");

                entity.Property(e => e.DiasDisponibles).HasColumnName("diasDisponibles");

                entity.HasOne(d => d.CodigoEmpleadoNavigation)
                    .WithMany(p => p.VacacionesXempleados)
                    .HasForeignKey(d => d.CodigoEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VacacionesXEmpleado_Empleado");

                entity.HasOne(d => d.CodigoVacacionNavigation)
                    .WithMany(p => p.VacacionesXempleados)
                    .HasForeignKey(d => d.CodigoVacacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("VacacionesXEmpleado_Vacaciones");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
