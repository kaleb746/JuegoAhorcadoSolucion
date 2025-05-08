using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorAhorcadoLibrary.DTO
{
    public class PartidaDTO
    {
        public int IDPartida { get; set; }
        public string Creador { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}

