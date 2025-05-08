using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace AhorcadoServidor.Model
{
    public class AhorcadoContext : DbContext
    {
        public AhorcadoContext() : base("name=AhorcadoDB") { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Idioma> Idiomas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Palabra> Palabras { get; set; }
        public DbSet<DescripcionPalabra> DescripcionesPalabras { get; set; }
        public DbSet<Partida> Partidas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<HistorialPuntaje> HistorialPuntaje { get; set; }
    }
}

