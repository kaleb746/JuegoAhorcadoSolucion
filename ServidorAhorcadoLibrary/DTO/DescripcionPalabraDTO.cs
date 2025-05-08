using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorAhorcadoLibrary.DTO
{
    public class DescripcionPalabraDTO
    {
        public int IDPalabra { get; set; }        // Relación con la tabla Palabra
        public int IDIdioma { get; set; }         // Relación con la tabla Idioma
        public string TextoPalabra { get; set; }  // Texto de la palabra en el idioma (ej. "Cat", "Gato")
        public string Descripcion { get; set; }   // Descripción en el idioma (ej. "Domestic feline", "Felino doméstico")
        

    }
}
