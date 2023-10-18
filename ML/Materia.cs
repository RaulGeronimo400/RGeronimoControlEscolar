using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Materia
    {
        public int IdMateria { get; set; }

        public string Nombre { get; set; }

        
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Se debe colocar solo numeros")]
        public decimal Costo { get; set; }
        public List<object> Materias { get; set; }
    }
}
