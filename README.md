# JuegoAhorcadoSolucion
## 📌 Descripción
El juego permite a dos jugadores participar a través de una arquitectura cliente-servidor implementada con **WCF**, ofreciendo una experiencia gráfica fluida e interactiva mediante **WPF**.

El jugador 1 crea una partida con una palabra secreta y el jugador 2 debe adivinarla. Si falla seis veces, pierde. El sistema también incluye manejo de usuarios, puntajes, penalizaciones y soporte multilenguaje.
## ✨ Características
🎮 Modo de juego multijugador (retador y adivinador)
🔐 Inicio de sesión y registro de usuarios
🕹️ Creación y unión a partidas disponibles
📈 Sistema de puntajes y penalizaciones
👤 Visualización y edición de perfil (excepto correo)
🌐 Soporte multilenguaje (Español / Inglés)
📚 Catálogo de palabras por categoría e idioma
## 🔧 Tecnologías Utilizadas
* 🖥️ Lenguaje: C#
* 🛠️ Framework: .NET Framework
* 🖼️ Interfaz Gráfica: WPF
* 🔗 Comunicación: WCF (Windows Communication Foundation)
* 🗃️ Base de Datos: SQL Server
* ⚙️ Arquitectura: Cliente-Servidor
## 📋 Requisitos
* Sistema Operativo: Windows 10 o superior
* .NET Framework compatible instalado
* SQL Server en ejecución con la base de datos configurada
* Servidor WCF disponible para la comunicación
## 🚀 Instalación y Configuración
1. 📥 Clonar el repositorio:
   ```bash
   git clone https://github.com/usuario/ahorcado-cliente.git
   ```
2. 🛠️ Abrir la solución en **Visual Studio**.
3. ⚙️ Configurar la URL del servicio WCF en el archivo `App.config`.
4. 📄 Asegúrate de tener el servidor de base de datos y el servicio WCF corriendo.
5. ▶️ Compilar y ejecutar el proyecto.
