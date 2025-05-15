using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorAhorcadoService.Model
{
    public class DescripcionPalabra
    {
        public int IDIdioma { get; set; }
        public string PalabraTexto { get; set; }
        public string Descripcion { get; set; }

        public virtual Palabra Palabra { get; set; }
        public virtual Idioma Idioma { get; set; }

        public int IDPalabra {  get; set; } 
    }
}
