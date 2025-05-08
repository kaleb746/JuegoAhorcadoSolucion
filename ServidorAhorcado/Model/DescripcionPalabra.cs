using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhorcadoServidor.Model
{
    public class DescripcionPalabra
    {
        public int IDDescripcion { get; set; }
        public int IDPalabra { get; set; }
        public int IDIdioma { get; set; }
        public string Descripcion { get; set; }
    }
}
