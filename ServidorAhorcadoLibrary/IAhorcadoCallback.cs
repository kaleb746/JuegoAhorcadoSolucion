using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ServidorAhorcadoLibrary;

namespace ServidorAhorcadoLibrary
{
    [ServiceContract]
    public interface IAhorcadoCallback
    {
            [OperationContract(IsOneWay = true)]
            void ActualizarEstadoPartida(PartidaEstadoDTO estadoActual);

            [OperationContract(IsOneWay = true)]
            void RecibirMensajeChat(string nombreJugador, string mensaje);

            [OperationContract(IsOneWay = true)]
            void NotificarFinPartida(string resultado, string palabra);
      
    }
}

