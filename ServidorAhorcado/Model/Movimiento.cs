using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhorcadoServidor.Model
{
    public class Movimiento
    {
        public int IDMovimiento { get; set; }
        public int IDPartida { get; set; }
        public int IDJugador { get; set; }
        public string Letra { get; set; }
        public bool EsCorrecta { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
