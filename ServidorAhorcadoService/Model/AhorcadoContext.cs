using ServidorAhorcadoService.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;


namespace ServidorAhorcadoService.Model
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
        public DbSet<HistorialPuntaje> HistorialPuntaje { get; set; }
        public DbSet<DescripcionCategoria> DescripcionCategorias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasKey(u => u.IDUsuario);
            modelBuilder.Entity<Categoria>().HasKey(c => c.IDCategoria);
            modelBuilder.Entity<Palabra>().HasKey(p => p.IDPalabra);
            modelBuilder.Entity<DescripcionPalabra>().HasKey(dp => dp.IDIdioma);
            modelBuilder.Entity<DescripcionCategoria>().HasKey(dc => dc.IDDescripcionCategoria);
            modelBuilder.Entity<Partida>().HasKey(p => p.IDPartida);
            modelBuilder.Entity<HistorialPuntaje>().HasKey(h => h.IDHistorial);
            modelBuilder.Entity<Idioma>().HasKey(i => i.IDIdioma);

            // Repite para las demás entidades...
        }
    }
}

