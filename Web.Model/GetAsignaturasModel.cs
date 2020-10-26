using System;
using System.Collections.Generic;

namespace Web.Model
{
    public class GetAsignaturasModel
    {
        public List<Asignatura> Asignaturas { get; set; }

        public string SelectedAlumnoCod { get; set; }

        public List<Alumno> Alumnos { get; set; }

        public string SelectedProfesorCod { get; set; }

        public List<Profesor>  Profesores { get; set; }

        public double PromedioGeneral { get; set; }
    }
}