using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ServidorAhorcadoService.Model
{
    public class Idioma
    {
        [Key]
        public int IDIdioma { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    }
}
