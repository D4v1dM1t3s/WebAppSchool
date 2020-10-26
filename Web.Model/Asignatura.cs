using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Model
{
    public class Asignatura
    {
        public decimal ID { get; set; }

        public string CodigoMateria { get; set; }

        public string Materia { get; set; }

        public string AlumnoCedula { get; set; }

        public string Alumno { get; set; }

        public string ProfesorCedula { get; set; }

        public string Profesor { get; set; }

        [Range(1, 10)]
        [Column(TypeName = "decimal(2, 2)")]
        public double NotaTareas { get; set; }

        [Range(1, 10)]
        [Column(TypeName = "decimal(2, 2)")]
        public double NotaExamen { get; set; }

        [Column(TypeName = "decimal(2, 2)")]
        public double Promedio
        {
            get
            {
                return (this.NotaTareas + this.NotaExamen) / 2;
            }
        }


        public string EstadoStr
        {
            get
            {
                if (((this.NotaTareas + this.NotaExamen) / 2) < 7)
                    return "REPROBADO";
                else
                    return "APROBADO";
            }
        }

        public bool Aprobo
        {
            get
            {
                return ((this.NotaTareas + this.NotaExamen) / 2) >= 7;
            }
        }
    }
}