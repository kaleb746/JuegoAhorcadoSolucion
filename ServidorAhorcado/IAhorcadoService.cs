
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace AhorcadoServidor

{
    [ServiceContract(CallbackContract = typeof(IAhorcadoCallback))]
    public interface IAhorcadoService
    {
        // Autenticación
        [OperationContract]
        UsuarioDTO IniciarSesion(string correo, string password);

        [OperationContract]
        bool RegistrarUsuario(UsuarioDTO nuevoUsuario);

        [OperationContract]
        bool EditarPerfil(UsuarioDTO usuarioActualizado);

        [OperationContract]
        UsuarioDTO ObtenerPerfil(int idUsuario);

        // Partidas
        [OperationContract]
        List<PartidaDTO> ObtenerPartidasDisponibles();

        [OperationContract]
        bool CrearPartida(int idCreador, int idPalabra);

        [OperationContract]
        bool UnirseAPartida(int idPartida, int idJugador);

        [OperationContract]
        bool AbandonarPartida(int idPartida, int idJugador);

        [OperationContract]
        bool EnviarLetra(int idPartida, int idJugador, char letra);

        [OperationContract]
        PalabraDTO ObtenerPalabraConDescripcion(int idPalabra, string idioma);

        [OperationContract]
        PartidaEstadoDTO ObtenerEstadoPartida(int idPartida);

        // Puntaje
        [OperationContract]
        List<HistorialPuntajeDTO> ObtenerPuntajeJugador(int idUsuario);

        // Chat
        [OperationContract(IsOneWay = true)]
        void EnviarMensajeChat(int idPartida, string nombreJugador, string mensaje);
    }

    public interface IAhorcadoCallback
    {
        [OperationContract(IsOneWay = true)]
        void RecibirMensajeChat(string nombreJugador, string mensaje);

        [OperationContract(IsOneWay = true)]
        void ActualizarEstadoPartida(PartidaEstadoDTO estadoActual);

        [OperationContract(IsOneWay = true)]
        void NotificarFinPartida(string resultado, string palabra);
    }

    // DTOs
    public class UsuarioDTO
    {
        public int IDUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string Idioma { get; set; }
        public int PuntajeGlobal { get; set; }
    }

    public class PartidaDTO
    {
        public int IDPartida { get; set; }
        public string Creador { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class PalabraDTO
    {
        public string Texto { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
    }

    public class PartidaEstadoDTO
    {
        public string PalabraConGuiones { get; set; }
        public int IntentosRestantes { get; set; }
        public List<char> LetrasUsadas { get; set; }
        public string TurnoActual { get; set; }
    }

    public class HistorialPuntajeDTO
    {
        public string Tipo { get; set; }
        public int Puntaje { get; set; }
        public DateTime Fecha { get; set; }
        public string Palabra { get; set; }
        public string Rival { get; set; }
    }
}
