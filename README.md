FerreteriaAPI
API RESTful desarrollada con ASP.NET Core (.NET 8) para la gestión de clientes, artículos, ventas y operaciones generales de una ferretería.

🚀 Características
Autenticación con JWT

CRUD completo de clientes

Documentación interactiva con Swagger

Integración con Entity Framework Core y SQL Server

Arquitectura limpia: separación en Controllers, DTOs y Services

✅ Requisitos
.NET 8 SDK

SQL Server (local o en la nube)

Visual Studio 2022 o superior (opcional)

⚙️ Configuración del proyecto
Clona el repositorio

bash
Copiar
Editar
git clone https://github.com/tu-usuario/FerreteriaAPI.git
cd FerreteriaAPI
Configura appsettings.json

Copia el archivo appsettings.example.json y renómbralo como appsettings.json.

Ajusta:

La cadena de conexión DefaultConnection

La clave secreta Token:Key para JWT

Restaura los paquetes NuGet

bash
Copiar
Editar
dotnet restore
Aplica las migraciones y crea la base de datos

bash
Copiar
Editar
dotnet ef database update
Ejecuta la API

bash
Copiar
Editar
dotnet run
Accede a Swagger

Abre en tu navegador: https://localhost:7166/swagger

📁 Estructura del proyecto
Controllers/ → Controladores de la API

DTOs/ → Objetos de transferencia de datos

Services/ → Lógica de negocio

appsettings.json → Configuración (¡no subir datos sensibles!)

📝 Notas importantes
No subas tu appsettings.json al repositorio. Usa .gitignore.

El archivo appsettings.example.json sirve como guía de configuración para otros desarrolladores.

La autenticación usa JWT: asegúrate de proteger tu clave secreta.

Para dudas, consulta la documentación oficial de .NET y SQL Server.

🧪 Esquema de base de datos
Aquí se presenta una parte del modelo relacional:

sql
Copiar
Editar
-- Tabla Usuario
CREATE TABLE Usuario (
    UsuarioID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    Contrasena VARCHAR(100) NOT NULL,
    Activo BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE()
);

-- Tabla Articulo
CREATE TABLE Articulo (
    ArticuloID INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(20) NOT NULL UNIQUE,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255),
    PrecioCompra DECIMAL(10,2) NOT NULL,
    PrecioVenta DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL DEFAULT 0,
    Categoria VARCHAR(50),
    Proveedor VARCHAR(100),
    Ubicacion VARCHAR(50),
    Activo BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE()
);

-- Tabla Cliente
CREATE TABLE Cliente (
    ClienteID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Direccion VARCHAR(255),
    Telefono VARCHAR(20),
    Email VARCHAR(100),
    NIT VARCHAR(20),
    Activo BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE()
);

-- Tabla Empleado
CREATE TABLE Empleado (
    EmpleadoID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Direccion VARCHAR(255),
    Telefono VARCHAR(20),
    Email VARCHAR(100),
    DPI VARCHAR(20) NOT NULL UNIQUE,
    Puesto VARCHAR(50),
    Salario DECIMAL(10,2),
    Activo BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE()
);

-- Tabla FormaPago
CREATE TABLE FormaPago (
    FormaPagoID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Descripcion VARCHAR(255),
    Activo BIT DEFAULT 1
);

-- Tabla Venta
CREATE TABLE Venta (
    VentaID INT IDENTITY(1,1) PRIMARY KEY,
    ClienteID INT NOT NULL,
    EmpleadoID INT NOT NULL,
    FormaPagoID INT NOT NULL,
    FechaVenta DATETIME DEFAULT GETDATE(),
    Subtotal DECIMAL(10,2) NOT NULL,
    Impuesto DECIMAL(10,2) NOT NULL,
    Total DECIMAL(10,2) NOT NULL,
    Estado VARCHAR(20) DEFAULT 'Completada',
    FOREIGN KEY (ClienteID) REFERENCES Cliente(ClienteID),
    FOREIGN KEY (EmpleadoID) REFERENCES Empleado(EmpleadoID),
    FOREIGN KEY (FormaPagoID) REFERENCES FormaPago(FormaPagoID)
);

-- Tabla DetalleVenta
CREATE TABLE DetalleVenta (
    DetalleVentaID INT IDENTITY(1,1) PRIMARY KEY,
    VentaID INT NOT NULL,
    ArticuloID INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    Subtotal DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (VentaID) REFERENCES Venta(VentaID),
    FOREIGN KEY (ArticuloID) REFERENCES Articulo(ArticuloID)
);
📄 Licencia
Este proyecto está bajo la licencia MIT.
