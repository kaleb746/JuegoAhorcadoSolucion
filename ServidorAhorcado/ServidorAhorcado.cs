
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace AhorcadoServidor
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AhorcadoService : IAhorcadoService
    {
        private Dictionary<int, IAhorcadoCallback> clientesConectados = new();

        public UsuarioDTO IniciarSesion(string correo, string password)
        {
            return new UsuarioDTO { IDUsuario = 1, NombreCompleto = "Demo", Correo = correo };
        }

        public bool RegistrarUsuario(UsuarioDTO nuevoUsuario)
        {
            return true;
        }

        public bool EditarPerfil(UsuarioDTO usuarioActualizado)
        {
            return true;
        }

        public UsuarioDTO ObtenerPerfil(int idUsuario)
        {
            return new UsuarioDTO { IDUsuario = idUsuario, NombreCompleto = "Ejemplo" };
        }

        public List<PartidaDTO> ObtenerPartidasDisponibles()
        {
            return new List<PartidaDTO> {
                new PartidaDTO { IDPartida = 1, Creador = "Jugador1", Estado = "En espera" }
            };
        }

        public bool CrearPartida(int idCreador, int idPalabra)
        {
            return true;
        }

        public bool UnirseAPartida(int idPartida, int idJugador)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IAhorcadoCallback>();
            clientesConectados[idJugador] = callback;
            return true;
        }

        public bool AbandonarPartida(int idPartida, int idJugador)
        {
            clientesConectados.Remove(idJugador);
            return true;
        }

        public bool EnviarLetra(int idPartida, int idJugador, char letra)
        {
            foreach (var cliente in clientesConectados.Values)
            {
                cliente.ActualizarEstadoPartida(new PartidaEstadoDTO
                {
                    PalabraConGuiones = "_ a _ a _ _",
                    IntentosRestantes = 5,
                    LetrasUsadas = new List<char> { letra },
                    TurnoActual = "Jugador2"
                });
            }
            return true;
        }

        public PalabraDTO ObtenerPalabraConDescripcion(int idPalabra, string idioma)
        {
            return new PalabraDTO { Texto = "banana", Descripcion = "Fruta amarilla", Categoria = "Frutas" };
        }

        public PartidaEstadoDTO ObtenerEstadoPartida(int idPartida)
        {
            return new PartidaEstadoDTO
            {
                PalabraConGuiones = "_ a _ a _ _",
                IntentosRestantes = 5,
                LetrasUsadas = new List<char> { 'a', 'z' },
                TurnoActual = "Jugador1"
            };
        }

        public List<HistorialPuntajeDTO> ObtenerPuntajeJugador(int idUsuario)
        {
            return new List<HistorialPuntajeDTO>
            {
                new HistorialPuntajeDTO { Tipo = "Victoria", Puntaje = 10, Fecha = DateTime.Now, Palabra = "banana", Rival = "Jugador2" }
            };
        }

        public void EnviarMensajeChat(int idPartida, string nombreJugador, string mensaje)
        {
            foreach (var cliente in clientesConectados.Values)
            {
                cliente.RecibirMensajeChat(nombreJugador, mensaje);
            }
        }
    }
}
