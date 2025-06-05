# 🏗️ Ferreteria API

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

API RESTful desarrollada con ASP.NET Core 8 para la gestión integral de una ferretería. Incluye módulos para clientes, artículos, ventas, empleados y más.

## 🚀 Características

- **Autenticación segura** con JWT (JSON Web Tokens)
- **CRUD completo** para todas las entidades principales
- **Documentación interactiva** con Swagger/OpenAPI
- **Arquitectura limpia** siguiendo mejores prácticas
- **Entity Framework Core** con SQL Server
- **Manejo de transacciones** para operaciones críticas
- **Validación de datos** robusta
- **Sistema de roles y permisos**

## 📋 Requisitos

- .NET 8.0 SDK o superior
- SQL Server 2019+ (local o en la nube)
- Visual Studio 2022 o VS Code (recomendado)

## 🛠️ Configuración

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/DanielsEZ/FerreteriaAPI.git
   cd FerreteriaAPI
   ```

2. **Configurar la base de datos**
   - Copiar `appsettings.example.json` a `appsettings.json`
   - Configurar la cadena de conexión en `ConnectionStrings.DefaultConnection`
   - Establecer `Token:Key` para JWT (mínimo 16 caracteres)

3. **Restaurar paquetes**
   ```bash
   dotnet restore
   ```

4. **Aplicar migraciones**
   ```bash
   dotnet ef database update
   ```

5. **Ejecutar la aplicación**
   ```bash
   dotnet run
   ```

6. **Acceder a la documentación**
   Abre tu navegador en: [https://localhost:7166/swagger](https://localhost:7166/swagger)

## 🗄️ Estructura del Proyecto

```
FerreteriaAPI/
├── Controllers/      # Controladores de la API
├── Data/              # Contexto de base de datos y configuraciones
├── DTOs/              # Objetos de transferencia de datos
├── Models/            # Modelos de entidades
├── Services/          # Lógica de negocio
├── Migrations/        # Migraciones de Entity Framework
└── appsettings.json   # Configuración de la aplicación
```

## 📊 Esquema de Base de Datos

El sistema utiliza las siguientes tablas principales:

- `Usuario`: Gestión de usuarios del sistema
- `Articulo`: Catálogo de productos
- `Cliente`: Información de clientes
- `Empleado`: Datos del personal
- `Venta` y `DetalleVenta`: Registro de transacciones
- `FormaPago`: Métodos de pago disponibles

## 🔐 Seguridad

- Autenticación basada en JWT
- Hash de contraseñas con BCrypt
- Protección contra ataques CSRF
- Validación de entrada en todos los endpoints

## 📝 Licencia

Este proyecto está bajo la [Licencia MIT](LICENSE).

## 🤝 Contribuciones

Las contribuciones son bienvenidas. Por favor, lee las [pautas de contribución](CONTRIBUTING.md) antes de enviar un pull request.

## 📧 Contacto

Daniel - [@DanieleZ](https://github.com/DanielsEZ)

---

<div align="center">
  Hecho con ❤️ usando .NET Core
</div>
