using System;
using System.ComponentModel.DataAnnotations;

namespace ServidorAhorcadoService.Model
{
    [Key]
    public int IDJugador { get; set; }

    public string Nombre { get; set; }
    public string Correo { get; set; }
    public string Contraseña { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Telefono { get; set; }
    public int PuntajeGlobal { get; set; }

    public virtual ICollection<Partida> PartidasCreadas { get; set; }
    public virtual ICollection<Partida> PartidasRetadas { get; set; }

}
