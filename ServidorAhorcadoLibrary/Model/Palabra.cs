using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhorcadoServidor.Model
{
    public class Palabra
    {
        public int IDPalabra { get; set; }
        public string  TextoPalabra { get; set; }
        public string Dificultad { get; set; }
        public int IDCategoria { get; set; }
        public int IDIdioma { get; set; }
    }
}
