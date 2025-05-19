using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ServidorAhorcadoService.Model
{
    public class Estado
    {
        [Key]
        public int IDEstado { get; set; }

        public string Nombre { get; set; }

        public virtual ICollection<Partida> Partidas { get; set; }
    }
}
