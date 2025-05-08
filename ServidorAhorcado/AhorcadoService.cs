using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace AhorcadoServidor
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AhorcadoService : IAhorcadoService
    {
        private Dictionary<int, IAhorcadoCallback> clientesConectados = new();

        // ---------------------------
        // AUTENTICACIÓN
        // ---------------------------
        public UsuarioDTO IniciarSesion(string correo, string password)
        {
            using (var db = new AhorcadoContext())
            {
                var usuario = db.Usuarios.FirstOrDefault(u => u.Correo == correo && u.Password == password);
                if (usuario != null)
                {
                    return new UsuarioDTO
                    {
                        IDUsuario = usuario.IDUsuario,
                        NombreCompleto = usuario.NombreCompleto,
                        Correo = usuario.Correo,
                        Idioma = db.Idiomas.Where(i => i.IDIdioma == usuario.IdiomaPreferido)
                                           .Select(i => i.Codigo).FirstOrDefault(),
                        PuntajeGlobal = usuario.PuntajeGlobal
                    };
                }
                return null;
            }
        }

        public bool RegistrarUsuario(UsuarioDTO nuevoUsuario)
        {
            using (var db = new AhorcadoContext())
            {
                if (db.Usuarios.Any(u => u.Correo == nuevoUsuario.Correo))
                    return false;

                int idiomaId = db.Idiomas.Where(i => i.Codigo == nuevoUsuario.Idioma)
                                         .Select(i => i.IDIdioma)
                                         .FirstOrDefault();

                var nuevo = new Usuario
                {
                    NombreCompleto = nuevoUsuario.NombreCompleto,
                    Correo = nuevoUsuario.Correo,
                    Password = nuevoUsuario.Password,
                    Telefono = nuevoUsuario.Telefono,
                    FechaNacimiento = nuevoUsuario.FechaNacimiento,
                    IdiomaPreferido = idiomaId,
                    PuntajeGlobal = 0
                };

                db.Usuarios.Add(nuevo);
                db.SaveChanges();
                return true;
            }
        }

        public bool EditarPerfil(UsuarioDTO usuarioActualizado)
        {
            using (var db = new AhorcadoContext())
            {
                var usuario = db.Usuarios.FirstOrDefault(u => u.IDUsuario == usuarioActualizado.IDUsuario);
                if (usuario == null)
                    return false;

                usuario.NombreCompleto = usuarioActualizado.NombreCompleto;
                usuario.Telefono = usuarioActualizado.Telefono;
                usuario.FechaNacimiento = usuarioActualizado.FechaNacimiento;
                db.SaveChanges();
                return true;
            }
        }

        public UsuarioDTO ObtenerPerfil(int idUsuario)
        {
            using (var db = new AhorcadoContext())
            {
                var usuario = db.Usuarios.FirstOrDefault(u => u.IDUsuario == idUsuario);
                if (usuario != null)
                {
                    return new UsuarioDTO
                    {
                        IDUsuario = usuario.IDUsuario,
                        NombreCompleto = usuario.NombreCompleto,
                        Correo = usuario.Correo,
                        Telefono = usuario.Telefono,
                        FechaNacimiento = usuario.FechaNacimiento,
                        Idioma = db.Idiomas.Where(i => i.IDIdioma == usuario.IdiomaPreferido)
                                           .Select(i => i.Codigo).FirstOrDefault(),
                        PuntajeGlobal = usuario.PuntajeGlobal
                    };
                }
                return null;
            }
        }

        // ---------------------------
        // PARTIDAS
        // ---------------------------
        public List<PartidaDTO> ObtenerPartidasDisponibles()
        {
            using (var db = new AhorcadoContext())
            {
                return db.Partidas
                    .Where(p => p.Estado == "En espera" && p.IDRetador == null)
                    .Select(p => new PartidaDTO
                    {
                        IDPartida = p.IDPartida,
                        Creador = db.Usuarios.Where(u => u.IDUsuario == p.IDCreador).Select(u => u.NombreCompleto).FirstOrDefault(),
                        Estado = p.Estado,
                        FechaCreacion = p.FechaCreacion
                    }).ToList();
            }
        }

        public bool CrearPartida(int idCreador, int idPalabra)
        {
            using (var db = new AhorcadoContext())
            {
                var nueva = new Partida
                {
                    IDCreador = idCreador,
                    IDPalabra = idPalabra,
                    Estado = "En espera",
                    FechaCreacion = DateTime.Now,
                    IntentosRestantes = 6
                };

                db.Partidas.Add(nueva);
                db.SaveChanges();
                return true;
            }
        }

        public bool UnirseAPartida(int idPartida, int idJugador)
        {
            using (var db = new AhorcadoContext())
            {
                var partida = db.Partidas.FirstOrDefault(p => p.IDPartida == idPartida && p.Estado == "En espera");
                if (partida == null)
                    return false;

                partida.IDRetador = idJugador;
                partida.Estado = "En juego";
                db.SaveChanges();

                // Guardar canal de callback
                var callback = OperationContext.Current.GetCallbackChannel<IAhorcadoCallback>();
                clientesConectados[idJugador] = callback;
                return true;
            }
        }

        public bool AbandonarPartida(int idPartida, int idJugador)
        {
            using (var db = new AhorcadoContext())
            {
                var partida = db.Partidas.FirstOrDefault(p => p.IDPartida == idPartida);
                if (partida == null)
                    return false;

                partida.Estado = "Cancelada";
                db.SaveChanges();

                clientesConectados.Remove(idJugador);

                var puntaje = new HistorialPuntaje
                {
                    IDUsuario = idJugador,
                    IDPartida = idPartida,
                    Tipo = "Penalización",
                    Puntaje = -3,
                    Fecha = DateTime.Now
                };

                db.HistorialPuntaje.Add(puntaje);
                db.SaveChanges();

                return true;
            }
        }

        public bool EnviarLetra(int idPartida, int idJugador, char letra)
        {
            using (var db = new AhorcadoContext())
            {
                var partida = db.Partidas.FirstOrDefault(p => p.IDPartida == idPartida);
                if (partida == null || partida.Estado != "En juego")
                    return false;

                var palabra = db.Palabras.FirstOrDefault(p => p.IDPalabra == partida.IDPalabra);
                if (palabra == null)
                    return false;

                bool esCorrecta = palabra.Palabra.ToLower().Contains(letra);

                var movimiento = new Movimiento
                {
                    IDPartida = idPartida,
                    IDJugador = idJugador,
                    Letra = letra.ToString(),
                    EsCorrecta = esCorrecta,
                    FechaHora = DateTime.Now
                };
                db.Movimientos.Add(movimiento);

                if (!esCorrecta)
                    partida.IntentosRestantes--;

                if (partida.IntentosRestantes <= 0)
                {
                    partida.Estado = "Finalizada";
                    partida.Ganador = partida.IDCreador == idJugador ? partida.IDRetador : partida.IDCreador;

                    NotificarFinPartidaTodos(idPartida, "¡Perdiste!", palabra.Palabra);
                }
                else if (TodasLetrasAdivinadas(palabra.Palabra, idPartida, db))
                {
                    partida.Estado = "Finalizada";
                    partida.Ganador = idJugador;

                    NotificarFinPartidaTodos(idPartida, "¡Ganaste!", palabra.Palabra);
                }

                db.SaveChanges();

                NotificarEstadoPartidaTodos(idPartida, db);

                return true;
            }
        }

        private bool TodasLetrasAdivinadas(string palabra, int idPartida, AhorcadoContext db)
        {
            var letrasAdivinadas = db.Movimientos
                .Where(m => m.IDPartida == idPartida && m.EsCorrecta)
                .Select(m => m.Letra.ToLower())
                .Distinct()
                .ToList();

            return palabra.ToLower().Distinct().All(c => letrasAdivinadas.Contains(c.ToString()));
        }

        public PalabraDTO ObtenerPalabraConDescripcion(int idPalabra, string idioma)
        {
            using (var db = new AhorcadoContext())
            {
                var desc = db.DescripcionesPalabras.FirstOrDefault(d => d.IDPalabra == idPalabra && db.Idiomas.Where(i => i.Codigo == idioma).Select(i => i.IDIdioma).FirstOrDefault() == d.IDIdioma);
                var palabra = db.Palabras.FirstOrDefault(p => p.IDPalabra == idPalabra);

                return new PalabraDTO
                {
                    Texto = palabra.Palabra,
                    Descripcion = desc.Descripcion,
                    Categoria = db.Categorias.Where(c => c.IDCategoria == palabra.IDCategoria)
                                             .Select(c => c.Nombre).FirstOrDefault()
                };
            }
        }

        public PartidaEstadoDTO ObtenerEstadoPartida(int idPartida)
        {
            using (var db = new AhorcadoContext())
            {
                var partida = db.Partidas.FirstOrDefault(p => p.IDPartida == idPartida);
                if (partida == null) return null;

                var palabra = db.Palabras.FirstOrDefault(p => p.IDPalabra == partida.IDPalabra);
                var movimientos = db.Movimientos.Where(m => m.IDPartida == idPartida && m.EsCorrecta)
                                                .Select(m => m.Letra.ToLower()).ToList();

                string palabraGuiones = string.Concat(palabra.Palabra.Select(c => movimientos.Contains(c.ToString().ToLower()) ? c : '_'));

                return new PartidaEstadoDTO
                {
                    PalabraConGuiones = palabraGuiones,
                    IntentosRestantes = partida.IntentosRestantes,
                    LetrasUsadas = db.Movimientos.Where(m => m.IDPartida == idPartida)
                                                 .Select(m => m.Letra[0])
                                                 .Distinct().ToList(),
                    TurnoActual = db.Usuarios.Where(u => u.IDUsuario == partida.IDRetador).Select(u => u.NombreCompleto).FirstOrDefault()
                };
            }
        }

        public List<HistorialPuntajeDTO> ObtenerPuntajeJugador(int idUsuario)
        {
            using (var db = new AhorcadoContext())
            {
                return db.HistorialPuntaje.Where(h => h.IDUsuario == idUsuario)
                    .Select(h => new HistorialPuntajeDTO
                    {
                        Tipo = h.Tipo,
                        Puntaje = h.Puntaje,
                        Fecha = h.Fecha,
                        Palabra = db.Palabras.Where(p => p.IDPalabra == db.Partidas.Where(pa => pa.IDPartida == h.IDPartida).Select(pa => pa.IDPalabra).FirstOrDefault())
                                             .Select(p => p.Palabra).FirstOrDefault(),
                        Rival = db.Usuarios.Where(u => u.IDUsuario == db.Partidas.Where(pa => pa.IDPartida == h.IDPartida).Select(pa => pa.IDRetador).FirstOrDefault())
                                           .Select(u => u.NombreCompleto).FirstOrDefault()
                    }).ToList();
            }
        }

        public void EnviarMensajeChat(int idPartida, string nombreJugador, string mensaje)
        {
            foreach (var callback in clientesConectados.Values)
            {
                callback.RecibirMensajeChat(nombreJugador, mensaje);
            }
        }

        private void NotificarEstadoPartidaTodos(int idPartida, AhorcadoContext db)
        {
            var estado = ObtenerEstadoPartida(idPartida);
            foreach (var callback in clientesConectados.Values)
            {
                callback.ActualizarEstadoPartida(estado);
            }
        }

        private void NotificarFinPartidaTodos(int idPartida, string resultado, string palabra)
        {
            foreach (var callback in clientesConectados.Values)
            {
                callback.NotificarFinPartida(resultado, palabra);
            }
        }
    }
}
