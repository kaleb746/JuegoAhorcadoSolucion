using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ServidorAhorcadoService.Model
{
    public class HistorialPuntaje
    {
        [Key]
        public int IDHistorial { get; set; }
        public int IDUsuario { get; set; }
        public int IDPartida { get; set; }
        public string Tipo { get; set; }
        public int Puntaje { get; set; }
        public DateTime Fecha { get; set; }
    }
}
