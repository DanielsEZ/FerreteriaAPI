# FerreteriaAPI

API RESTful para la gestión de clientes y operaciones de una ferretería, desarrollada con ASP.NET Core (.NET 8).

## Características

- CRUD de clientes
- Autenticación JWT
- Documentación Swagger
- Entity Framework Core con SQL Server

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (local o remoto)
- Visual Studio 2022 (opcional)

## Configuración

1. **Clona el repositorio:**

   
2. **Configura la cadena de conexión y la clave JWT:**

   - Copia el archivo `appsettings.example.json` a `appsettings.json`.
   - Modifica los valores de `DefaultConnection` y `Token` según tu entorno.

3. **Restaura los paquetes NuGet:**

   
4. **Aplica las migraciones y crea la base de datos (opcional):**

   
5. **Ejecuta la API:**

   
6. **Accede a la documentación Swagger:**

   Visita [https://localhost:7166/swagger](https://localhost:7166/swagger) en tu navegador.

## Estructura del proyecto

- `FerreteriaAPI/Controllers` - Controladores de la API
- `FerreteriaAPI/DTOs` - Objetos de transferencia de datos
- `FerreteriaAPI/Services` - Lógica de negocio y servicios
- `FerreteriaAPI/appsettings.json` - Configuración de la aplicación (no subir datos sensibles)

## Notas

- No subas tu archivo `appsettings.json` con datos sensibles. Usa el archivo de ejemplo para compartir la estructura de configuración.
- Si tienes dudas sobre cómo configurar el entorno, revisa la documentación oficial de .NET y SQL Server.

## Licencia

[MIT](LICENSE)

---

## appsettings.example.json

---

**Recomendaciones:**
- Cambia la URL del repositorio por la tuya.
- Personaliza los valores de ejemplo según tu entorno.
- Agrega `appsettings.json` a tu `.gitignore` para evitar subir información sensible.

#Parte de la base de datos

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

