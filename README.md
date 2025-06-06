# 🛍️ Ecommerce API + Blazor App

**Proyecto de comercio electrónico con backend en ASP.NET Core y frontend en Blazor WebAssembly**

## 📋 Requisitos previos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [SQL Server 2022](https://www.microsoft.com/es-es/sql-server/sql-server-downloads) (opcional, se usa en Docker)
- IDE recomendado:
  - [Visual Studio 2022](https://visualstudio.microsoft.com/)
  - [JetBrains Rider](https://www.jetbrains.com/rider/)
  - [VS Code](https://code.visualstudio.com/) con extensiones C# y Docker

## 🚀 Configuración inicial

1. **Clonar el repositorio**:
   ```bash
   git clone [URL_DEL_REPOSITORIO]
   cd BlazorApp1
   ```

2. **Configurar variables de entorno**:
   - Copiar `appsettings.Development.example.json` a `appsettings.Development.json`
   - Configurar las cadenas de conexión y secrets según tu entorno

3. **Ejecutar con Docker (recomendado)**:
   ```bash
   docker-compose up -d
   ```
   Esto levantará:
   - SQL Server en `localhost:1433`
   - API en `http://localhost:5000`
   - Blazor App en `http://localhost:5001`


## 🔧 Comandos útiles

### Para la API (EcommerceApi)
```bash
# Crear nueva migración
dotnet ef migrations add [NombreMigracion] --project EcommerceApi

# Aplicar migraciones
dotnet ef database update --project EcommerceApi

# Ejecutar tests (si existen)
dotnet test EcommerceApi.Tests/
```

## 🌐 Endpoints principales
```
swagger http://localhost:5201/index.html
```
## 🛠️ Tecnologías utilizadas

### Backend
- ASP.NET Core 9.0
- Entity Framework Core 9.0
- SQL Server 2022
- Swagger/OpenAPI
- JWT Authentication


