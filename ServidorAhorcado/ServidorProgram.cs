
using System;
using System.ServiceModel;
using AhorcadoServidor;

namespace ServidorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(ServidorProgram));
            host.Open();
            Console.WriteLine("Servidor del Ahorcado corriendo en http://localhost:8080/AhorcadoService/");
            Console.WriteLine("Presiona ENTER para salir...");
            Console.ReadLine();
            host.Close();
        }
    }
}
