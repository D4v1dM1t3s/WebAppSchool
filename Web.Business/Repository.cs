using DataContext;
using System.Collections.Generic;
using System.Linq;
using Web.Model;

namespace Web.Business
{
    public class Repository
    {
        public GetAsignaturasModel GetAsignaturas()
        {
            var model = new GetAsignaturasModel();
            using (SchoolDataContext ctx = new SchoolDataContext())
            {
                var asignaturas = (from x in ctx.Asignaturas
                                   orderby x.Id, x.MateriaCod
                                   select new Model.Asignatura
                                   {
                                       ID = x.Id,
                                       CodigoMateria = x.MateriaCod,
                                       Materia = x.Materia.Titulo,
                                       AlumnoCedula = x.AlumnoDni,
                                       Alumno = x.Alumno.Nombres,
                                       ProfesorCedula = x.ProfesorDni,
                                       Profesor = x.Profesor.Nombres,
                                       NotaTareas = x.Tareas,
                                       NotaExamen = x.Examen
                                   }).ToList();
                model.Asignaturas = asignaturas;

                model.Alumnos = this.GetAlumnos();
                model.Profesores = this.GetProfesores();
                model.PromedioGeneral = (from x in asignaturas
                                         select x.Promedio).Average();
            }
            return model;
        }

        public List<Model.Asignatura> GetAsignaturasPorProfesor(string profesorDNI)
        {
            using (SchoolDataContext ctx = new SchoolDataContext())
            {
                var rows = (from x in ctx.Asignaturas
                            where x.ProfesorDni.Equals(profesorDNI)
                            orderby x.Id, x.MateriaCod
                            select new Model.Asignatura
                            {
                                ID = x.Id,
                                CodigoMateria = x.MateriaCod,
                                Materia = x.Materia.Titulo,
                                AlumnoCedula = x.AlumnoDni,
                                Alumno = x.Alumno.Nombres,
                                ProfesorCedula = x.ProfesorDni,
                                Profesor = x.Profesor.Nombres,
                                NotaTareas = x.Tareas,
                                NotaExamen = x.Examen
                            }).ToList();

                return rows;
            }
        }

        public List<Model.Asignatura> GetAsignaturasPorProfesorYMateria(string profesorDNI, string codMateria)
        {
            using (SchoolDataContext ctx = new SchoolDataContext())
            {
                var rows = (from x in ctx.Asignaturas
                            where x.ProfesorDni.Equals(profesorDNI)
                            && x.MateriaCod.Equals(codMateria)
                            orderby x.Id, x.MateriaCod
                            select new Model.Asignatura
                            {
                                ID = x.Id,
                                CodigoMateria = x.MateriaCod,
                                Materia = x.Materia.Titulo,
                                AlumnoCedula = x.AlumnoDni,
                                Alumno = x.Alumno.Nombres,
                                ProfesorCedula = x.ProfesorDni,
                                Profesor = x.Profesor.Nombres,
                                NotaTareas = x.Tareas,
                                NotaExamen = x.Examen
                            }).ToList();

                return rows;
            }
        }

        public GetAsignaturasModel GetAsignaturas(string filtro)
        {
            var model = new GetAsignaturasModel();
            using (SchoolDataContext ctx = new SchoolDataContext())
            {
                var rows = (from x in ctx.Asignaturas
                            orderby x.Id, x.MateriaCod
                            select new Model.Asignatura
                            {
                                ID = x.Id,
                                CodigoMateria = x.MateriaCod,
                                Materia = x.Materia.Titulo,
                                AlumnoCedula = x.AlumnoDni,
                                Alumno = x.Alumno.Nombres,
                                ProfesorCedula = x.ProfesorDni,
                                Profesor = x.Profesor.Nombres,
                                NotaTareas = x.Tareas,
                                NotaExamen = x.Examen
                            }).ToList();

                //if (string.IsNullOrWhiteSpace(filtro) == false)
                //{
                //    rows = from x in rows
                //           where filtro.Contains(x.AlumnoCedula) ||
                //           filtro.Contains(x.Alumno) ||
                //           filtro.Contains(x.Materia) ||
                //           filtro.Contains(x.Profesor)
                //           select x;
                //}
                if (string.IsNullOrWhiteSpace(filtro) == false && filtro.ToUpper().Equals("APROBADO"))
                {
                    rows = (from x in rows
                            where x.Aprobo == true
                            select x).ToList();
                }
                if (string.IsNullOrWhiteSpace(filtro) == false && filtro.ToUpper().Equals("REPROBADO"))
                {
                    rows = (from x in rows
                            where x.Aprobo == false
                            select x).ToList();
                }
                var rowsFiltered = (from x in rows
                                    orderby x.ID, x.CodigoMateria
                                    select x).ToList();
                model.Asignaturas = rowsFiltered;

                model.Alumnos = this.GetAlumnos();
                model.Profesores = this.GetProfesores();
                model.PromedioGeneral = (from x in rowsFiltered
                                         select x.Promedio).Average();

                return model;
            }
        }

        public GetAsignaturasModel GetAsignaturasPorFiltro(GetAsignaturasPorFiltro filtro)
        {
            var model = new GetAsignaturasModel();
            using (SchoolDataContext ctx = new SchoolDataContext())
            {
                var rows = from x in ctx.Asignaturas
                           orderby x.Id, x.MateriaCod
                           select new Model.Asignatura
                           {
                               ID = x.Id,
                               CodigoMateria = x.MateriaCod,
                               Materia = x.Materia.Titulo,
                               AlumnoCedula = x.AlumnoDni,
                               Alumno = x.Alumno.Nombres,
                               ProfesorCedula = x.ProfesorDni,
                               Profesor = x.Profesor.Nombres,
                               NotaTareas = x.Tareas,
                               NotaExamen = x.Examen
                           };
                if (filtro != null)
                {
                    if (string.IsNullOrWhiteSpace(filtro.SelectedProfesorCod) == false)
                    {
                        rows = from x in rows
                               where filtro.SelectedProfesorCod.Equals(x.ProfesorCedula)
                               select x;
                    }
                    if (string.IsNullOrWhiteSpace(filtro.SelectedAlumnoCod) == false)
                    {
                        rows = from x in rows
                               where filtro.SelectedAlumnoCod.Equals(x.AlumnoCedula)
                               select x;
                    }
                }
                var rowsFiltered = (from x in rows
                                    orderby x.ID, x.CodigoMateria
                                    select x).ToList();

                model.Asignaturas = rowsFiltered;

                model.Alumnos = this.GetAlumnos();
                model.Profesores = this.GetProfesores();
                model.PromedioGeneral = (from x in rowsFiltered
                                         select x.Promedio).Average();

                return model;
            }
        }

        public List<Model.Profesor> GetProfesores()
        {
            using (SchoolDataContext ctx = new SchoolDataContext())
            {
                var rows = (from x in ctx.Profesors
                            orderby x.Cedula
                            select new Model.Profesor
                            {
                                Cedula = x.Cedula,
                                Nombres = x.Nombres,
                                Telefono = x.Telefono
                            }).ToList();

                return rows;
            }
        }

        public List<Model.Alumno> GetAlumnos()
        {
            using (SchoolDataContext ctx = new SchoolDataContext())
            {
                var rows = (from x in ctx.Alumnos
                            orderby x.Cedula
                            select new Model.Alumno
                            {
                                Cedula = x.Cedula,
                                Nombres = x.Nombres,
                                Telefono = x.Telefono,
                                Direccion = x.Direccion
                            }).ToList();

                return rows;
            }
        }

        public List<Model.Materia> GetMaterias()
        {
            using (SchoolDataContext ctx = new SchoolDataContext())
            {
                var rows = (from x in ctx.Materias
                            orderby x.Codigo
                            select new Model.Materia
                            {
                                Codigo = x.Codigo,
                                Area = x.Area,
                                Titulo = x.Titulo
                            }).ToList();

                return rows;
            }
        }

        public Model.Asignatura GetAsignatura(decimal ID)
        {
            using (SchoolDataContext ctx = new SchoolDataContext())
            {
                var asignatura = (from x in ctx.Asignaturas
                                  where x.Id == ID
                                  select new Model.Asignatura
                                  {
                                      ID = x.Id,
                                      CodigoMateria = x.MateriaCod,
                                      Materia = x.Materia.Titulo,
                                      AlumnoCedula = x.AlumnoDni,
                                      Alumno = x.Alumno.Nombres,
                                      ProfesorCedula = x.ProfesorDni,
                                      Profesor = x.Profesor.Nombres,
                                      NotaTareas = x.Tareas,
                                      NotaExamen = x.Examen
                                  }).FirstOrDefault();
                return asignatura;
            }
        }

        public bool SaveAsignatura(Model.Asignatura materia)
        {
            using (SchoolDataContext ctx = new SchoolDataContext())
            {
                var asignatura = (from x in ctx.Asignaturas
                                  where x.Id == materia.ID
                                  select x).FirstOrDefault();
                if (asignatura is null)
                    return false;
                else
                {
                    asignatura.Tareas = materia.NotaTareas;
                    asignatura.Examen = materia.NotaExamen;

                    ctx.SubmitChanges();

                    return true;
                }
            }
        }
    }
}