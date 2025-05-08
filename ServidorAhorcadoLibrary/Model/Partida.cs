using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhorcadoServidor.Model
{
    public class Partida
    {
        public int IDPartida { get; set; }
        public int IDCreador { get; set; }
        public int? IDRetador { get; set; }
        public int IDPalabra { get; set; }
        public string Estado { get; set; }
        public int IntentosRestantes { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? Ganador { get; set; }
    }
}
