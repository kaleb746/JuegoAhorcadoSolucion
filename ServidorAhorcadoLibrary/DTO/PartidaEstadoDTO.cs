using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServidorAhorcadoLibrary;


namespace ServidorAhorcadoLibrary.DTO
{
    public class PartidaEstadoDTO
    {
        public string PalabraConGuiones { get; set; }
        public int IntentosRestantes { get; set; }
        public List<char> LetrasUsadas { get; set; }
        public string TurnoActual { get; set; }
    }
}

