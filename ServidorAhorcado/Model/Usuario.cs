using System;

namespace AhorcadoServidor.Model
{
    public class Usuario
    {
        public int IDUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdiomaPreferido { get; set; }
        public int PuntajeGlobal { get; set; }
    }
}
