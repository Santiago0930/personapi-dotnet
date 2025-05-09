# Laboratorio 1

# Integrantes

- Santiago Guerrero
- Santiago Barajas
  
## Configuración del ambiente

### Requisitos previos

- Visual Studio 2022 (con el workload "Desarrollo ASP.NET y web")
- SQL Server Express (u otra versión compatible)
- SQL Server Management Studio (SSMS)
- .NET 6 SDK

## Pasos para configurar el ambiente

1. **Clona el repositorio**
   
   Opción 1 desde visual studio: 
     - Desde Visual studio puedes clonar el repositorio dando click en Archivo -> Clonar repositorio.
     - Luego escoges la ubicación donde quieres que se clone y pones el link del repo: https://github.com/Santiago0930/personapi-dotnet.git
       
   Opción 2 desde la terminal (cmd)
     - Ubicate en la carpeta donde quieres que se clone el proyecto
     - Ejecuta el comando: git clone https://github.com/Santiago0930/personapi-dotnet.git
       
2. **Configura la conexión a la base de datos**

   - Ubicate en la carpeta raiz del proyecto clonado, y buscar el archivo appsettings.json
   - Aquí vas a encontrar esta linea de codigo:
     "ConnectionStrings": {
          "DefaultConnection": "Server=TU_SERVIDOR_SQL;Database=persona_db;Trusted_Connection=True;TrustServerCertificate=true"
     }
   - Reemplaza "TU_SERVIDOR_SQL" por el nombre que tiene tu servidor en SQL Server

3. **Crea la base de datos**

   - Desde SSMS con el script proporcionado llamado **persona_db_ddl.sql**, abre SSMS y dale click en Archivo -> Abrir -> Archivo
   - Ubica el script que descargaste y seleccionalo
   - Ejecuta la query que se creo y se creara la base de datos
  
## Compliación y ejecución 

   Opción 1 desde visual: 
     - Ubicate en visual con el proyecto abierto
     - Dale click en la barra superior donde esta el boton de play, asegurate de que si este seleccionada la solución llamada personapi_dotnet o otra opción seria presionando F5
     - Automaticamente se abrira una pestaña en el navegador con la pantalla inicial del sistema con la url: http://localhost:5049/
   Opción 2 desde la terminal (cmd):
     - Ubicate en la carpeta raiz del proyecto y ejecuta el comando: dotnet run
     - Abre en el navegador una pestaña y pon esta url: http://localhost:5049/

  Ya el proyecto quedo desplegado en http://localhost:5049/ donde se podran probar todas las funcionalidades que brinda el sistema!

