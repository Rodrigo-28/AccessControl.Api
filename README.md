
# 🔐 AccessControlApi

**AccessControlApi** es una **API REST desarrollada en .NET 8**, orientada a la gestión de **usuarios, roles y autenticación**, diseñada siguiendo principios de **Clean Architecture** y pensada como una **base sólida y extensible para sistemas reales de control de acceso**.

El proyecto prioriza **claridad de diseño**, **separación de responsabilidades** y **reglas de negocio explícitas**, evitando sobreingeniería innecesaria.

## ⚖️ Criterio aplicado

Esta abstracción **no se aplicó desde el inicio** para evitar sobreingeniería.  
Primero se construyó el sistema de forma explícita y clara, y luego se extrajeron patrones repetidos.

Este enfoque refleja una **decisión consciente de diseño**, no automática.

## 📊 Descripción del sistema

La API permite:

- Registrar usuarios
- Autenticar usuarios mediante JWT
- Gestionar roles del sistema
- Asignar roles a usuarios
- Proteger endpoints mediante políticas de autorización
- Manejar errores de forma centralizada y consistente

## 📁 Application

Implementa la lógica de negocio:

- DTOs de request y response
- Servicios (`UserService`, `RolService`)
- Validaciones de reglas de negocio
- Excepciones personalizadas (`BadRequestException`, `NotFoundException`, etc.)
- Interfaces de servicios

> Esta capa define qué se puede hacer y bajo qué condiciones.

## 📁 Infrastructure

Contiene las implementaciones técnicas:

- Entity Framework Core
- PostgreSQL
- Repositorios concretos
- Configuración de autenticación JWT
- Configuración de políticas de autorización

> Aquí viven los detalles que pueden cambiar sin afectar el negocio.

## 📁 API (Web)

Capa de entrada al sistema:

- Controladores
- Endpoints HTTP
- Middleware de excepciones
- Swagger / OpenAPI

## 🔒 Autenticación y autorización

La API implementa autenticación stateless con JWT.

### 🔐 Autenticación

- El usuario inicia sesión con email y contraseña
- Se genera un JWT firmado
- El token incluye claims relevantes (`userId`, `email`, `role`, etc.)
- El cliente debe enviar el token en cada request

### 🛡️ Autorización por políticas

La autorización se maneja mediante policies, no directamente por roles.

Ejemplo de uso:

```csharp
[Authorize(Policy = "Admin")]
[HttpGet("{userId}")]
public async Task<IActionResult> GetUser(int userId)
{
    ...
}
```

Las políticas se configuran en el pipeline de autenticación y permiten:

- Centralizar reglas de acceso
- Escalar permisos sin acoplarlos a los controladores
- Mayor flexibilidad que `[Authorize(Roles = "...")]`

## 🧑‍💼 Gestión de roles

El sistema permite:

- Crear roles
- Listar roles
- Actualizar roles
- Eliminar roles

### Reglas de negocio implementadas

- No se permiten roles duplicados
- Un rol no puede eliminarse si tiene usuarios asignados
- Todas las validaciones se realizan en la capa de Application

## 🧩 UserService y RolService

Los servicios contienen la lógica central del sistema:

### UserService

- Registro de usuarios
- Encriptación de contraseñas
- Asignación de roles
- Cambio y verificación de contraseña
- Validación de reglas como email único

### RolService

- Validación de nombres únicos
- Prevención de eliminación con usuarios asociados
- Exposición de roles para administración

> Los controladores solo delegan: no hay lógica de negocio en la capa Web.

## 👤 Usuario administrador por defecto (configuración)

El sistema permite definir un usuario administrador inicial mediante configuración en `appsettings.json`:

```json
"Credentials": {
  "UserName": "Admin",
  "Email": "admin@admin.com",
  "Password": "1234"
}
```

### Propósito de esta funcionalidad

- Facilita el primer acceso al sistema
- Evita depender de datos iniciales manuales en la base
- Permite levantar la API lista para administrar usuarios y roles

### Consideraciones importantes

- El usuario se crea automáticamente si no existe
- La contraseña puede y debe cambiarse una vez iniciado el sistema
- Esta funcionalidad es especialmente útil en entornos de desarrollo y despliegues iniciales

## 🚀 Configuración y ejecución

### 1️⃣ Configurar la base de datos

En `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=access_control;Username=postgres;Password=****"
}
```

### 2️⃣ Configurar JWT

```json
"Jwt": {
  "Key": "TuClaveJwtSegura",
  "Issuer": "AccessControlApi",
  "Audience": "AccessControlApiUsers"
}
```

### 3️⃣ Aplicar migraciones

```bash
dotnet ef database update --project AccessControlApi.Infrastructure --startup-project AccessControlApi
```

### 4️⃣ Ejecutar la API

```bash
dotnet run --project AccessControlApi
```

### 5️⃣ Probar con Swagger

```
https://localhost:{puerto}/swagger
```

## 🌱 Rama adicional: abstracción de repositorios y servicios

En una rama separada se implementó una abstracción genérica:

- Repositorios base reutilizables
- Servicios base genéricos
- Soporte para soft delete e includes dinámicos

> Esta rama muestra una evolución técnica del proyecto, orientada a reducir duplicación y facilitar el crecimiento del sistema.  
> La rama principal mantiene el diseño explícito y claro.

## 🧠 Enfoque para entrevistas técnicas

Este proyecto me permitió demostrar:

- Uso real de Clean Architecture
- Separación clara de responsabilidades
- Implementación correcta de JWT
- Autorización con policies
- Manejo profesional de errores
- Diseño de servicios con reglas de negocio reales

> No es un proyecto académico: está pensado como base para un sistema productivo.

## 🧾 Tecnologías utilizadas

- **.NET 8** – https://dotnet.microsoft.com/
- **Entity Framework Core** – https://learn.microsoft.com/ef/core/
- **PostgreSQL** – https://www.postgresql.org/
- **JWT Authentication** – https://learn.microsoft.com/aspnet/core/security/authentication/jwt
- **AutoMapper** – https://automapper.org/
- **Swagger / Swashbuckle** – https://github.com/domaindrivendev/Swashbuckle.AspNetCore
