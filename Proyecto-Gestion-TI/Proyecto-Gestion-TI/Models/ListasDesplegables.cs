using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_TI.Models;


namespace Proyecto_Gestion_TI.Models
{
    public partial class ListasDesplegables
    {
        public ListasDesplegables()
        {
        }
        public virtual List<Models.Puesto> PuestosDeTrabajo { get; set; }
        public virtual ICollection<VacacionesXempleado> VacacionesXempleados { get; set; }
        public virtual List<Models.Departamento> DepartamentosDeLaEmpresa { get; set; }
        public virtual List<Models.TipoUsuario> TiposDeUsuario { get; set; }
        public virtual List<Models.Empleado> JefesDeEmpleados { get; set; }
        public virtual List<Models.Empleado> Empleados { get; set; }

        public List<SelectListItem> GenerarlistaDesplegableDePuestos()
        {
            using (GestionRRHHContext BasedeDatos = new GestionRRHHContext())
            {
                PuestosDeTrabajo = (from registro in BasedeDatos.Puestos
                                    select new Puesto
                                    {
                                        CodigoPuesto = registro.CodigoPuesto,
                                        NombrePuesto = registro.NombrePuesto
                                    }).ToList();
            }

            //Creacion de Listas Desplegables
            List<SelectListItem> listaDesplegableDePuestos = PuestosDeTrabajo.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.NombrePuesto.ToString(),
                    Value = d.CodigoPuesto.ToString(),
                    Selected = false

                };
            });
            return listaDesplegableDePuestos;
        }

        public List<SelectListItem> GenerarListaDesplegableDeDepartamentos()
        {
            using (GestionRRHHContext BasedeDatos = new GestionRRHHContext())
            {
                DepartamentosDeLaEmpresa = (from registro in BasedeDatos.Departamentos
                                            select new Departamento
                                            {
                                                CodigoDepartamento = registro.CodigoDepartamento,
                                                NombreDepartamento = registro.NombreDepartamento
                                            }).ToList();
            }

            //Creacion de Listas Desplegables
            List<SelectListItem> listaDesplegableDeDepartamentos = DepartamentosDeLaEmpresa.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.NombreDepartamento.ToString(),
                    Value = d.CodigoDepartamento.ToString(),
                    Selected = false
                };
            });
            return listaDesplegableDeDepartamentos;
        }

        public List<SelectListItem> GenerarlistaDesplegableDeTiposDeUsuario()
        {
            using (GestionRRHHContext BasedeDatos = new GestionRRHHContext())
            {
                TiposDeUsuario = (from registro in BasedeDatos.TipoUsuarios
                                  select new TipoUsuario
                                  {
                                      CodigoTipoUsuario = registro.CodigoTipoUsuario,
                                      NombreTipoUsurio = registro.NombreTipoUsurio
                                  }).ToList();
            }

            //Creacion de Listas Desplegables
            List<SelectListItem> listaDesplegableDeTiposDeUsuario = TiposDeUsuario.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.NombreTipoUsurio.ToString(),
                    Value = d.CodigoTipoUsuario.ToString(),
                    Selected = false
                };
            });
            return listaDesplegableDeTiposDeUsuario;
        }

        public List<SelectListItem> GenerarlistaDesplegableDeJefes()
        {
            using (GestionRRHHContext BasedeDatos = new GestionRRHHContext())
            {
                JefesDeEmpleados = (from empleados in BasedeDatos.Empleado
                                    join supervisores in BasedeDatos.Empleado
                                    on empleados.CodigoJefe equals supervisores.CodigoEmpleado

                                    select new Empleado
                                    {
                                        CodigoJefe = supervisores.CodigoEmpleado,
                                        NombreEmpleado = supervisores.NombreEmpleado
                                    }).Distinct().ToList();
            }

            //Creacion de Listas Desplegables
            List<SelectListItem> listaDesplegableDeJefes = JefesDeEmpleados.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.NombreEmpleado.ToString(),
                    Value = d.CodigoJefe.ToString(),
                    Selected = false
                };
            });
            return listaDesplegableDeJefes;
        }


    }
}
