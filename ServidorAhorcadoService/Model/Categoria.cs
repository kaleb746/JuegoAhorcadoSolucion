using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ServidorAhorcadoService.Model
{
    public class Categoria
    {
        [Key] 
        public int IDCategoria { get; set; }
        public string Nombre { get; set; }
        public int IDIdioma { get; set; }
    }
}
