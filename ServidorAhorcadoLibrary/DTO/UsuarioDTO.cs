using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorAhorcadoLibrary.DTO
{
    public class UsuarioDTO
    {
        public int IDUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Idioma { get; set; }
        public int PuntajeGlobal { get; set; }
    }
}
