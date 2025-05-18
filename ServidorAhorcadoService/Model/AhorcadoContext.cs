using ServidorAhorcadoService.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;


namespace ServidorAhorcadoService.Model
{
    public class AhorcadoContext : DbContext
    {
        static AhorcadoContext()
        {
            Database.SetInitializer<AhorcadoContext>(null);
        }
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
            modelBuilder.Entity<Usuario>().ToTable("Jugador"); // <-- Esta línea realiza el mapeo correcto
            modelBuilder.Entity<Usuario>().HasKey(u => u.IDUsuario);
            modelBuilder.Entity<Usuario>().Property(u => u.IDUsuario).HasColumnName("IDJugador");
            modelBuilder.Entity<Usuario>().Property(u => u.NombreCompleto).HasColumnName("Nombre");
            modelBuilder.Entity<Usuario>().Property(u => u.Password).HasColumnName("Contraseña");
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

