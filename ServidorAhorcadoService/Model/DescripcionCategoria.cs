using ServidorAhorcadoService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ServidorAhorcadoService.Model
{
    public class DescripcionCategoria
    {
        [Key]
        public int IDDescripcionCategoria { get; set; }
        public int IDCategoria { get; set; }
        public int IDIdioma { get; set; }
        public string NombreCategoria { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual Idioma Idioma { get; set; }
    }

}
