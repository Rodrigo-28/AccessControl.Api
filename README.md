# 🔐 AccessControlApi

**AccessControlApi** es una **API REST desarrollada en .NET 8**, orientada a la gestión de **usuarios, roles y autenticación**, diseñada siguiendo principios de **Clean Architecture** y pensada como una **base sólida y extensible para sistemas reales de control de acceso**.

El proyecto prioriza **claridad de diseño**, **separación de responsabilidades** y **reglas de negocio explícitas**, evitando sobreingeniería innecesaria.

---

## ⚖️ Criterio de diseño aplicado

La abstracción del sistema **no se aplicó desde el inicio de forma automática**.

Primero se construyó una implementación clara, explícita y directa.  
Luego, una vez identificados patrones repetidos, se extrajeron abstracciones en una rama separada.

Este enfoque refleja una **decisión consciente de diseño**, priorizando:

- Comprensión del dominio
- Código legible
- Evolución progresiva del sistema

---

## 📊 Descripción general del sistema

La API permite:

- Registro de usuarios
- Autenticación mediante JWT (stateless)
- Gestión de roles
- Asignación de roles a usuarios
- Protección de endpoints mediante **políticas de autorización**
- Manejo centralizado y consistente de errores

---

## 🧱 Arquitectura

El proyecto sigue **Clean Architecture**, separando responsabilidades en capas bien definidas.

### 📁 Application

Contiene la **lógica de negocio** y define las reglas del sistema.

Incluye:

- DTOs de request y response
- Servicios de dominio (`UserService`, `RolService`)
- Validaciones de reglas de negocio
- Excepciones personalizadas (`BadRequestException`, `NotFoundException`, etc.)
- Interfaces de servicios y repositorios

👉 Esta capa define **qué se puede hacer** y **bajo qué condiciones**, sin depender de frameworks.

---

### 📁 Infrastructure

Contiene las **implementaciones técnicas**.

Incluye:

- Entity Framework Core
- PostgreSQL
- Implementaciones concretas de repositorios
- Configuración de autenticación JWT
- Configuración de políticas de autorización

👉 Aquí viven los detalles que pueden cambiar sin afectar al dominio.

---

### 📁 API (Web)

Capa de entrada al sistema.

Incluye:

- Controladores
- Endpoints HTTP
- Middleware de manejo de excepciones
- Swagger / OpenAPI

👉 Los controladores **solo delegan**: no contienen lógica de negocio.

---

## 🔒 Autenticación y autorización

### 🔐 Autenticación (JWT)

La API implementa autenticación **stateless** mediante JWT:

- El usuario inicia sesión con email y contraseña
- Se genera un JWT firmado
- El token incluye claims relevantes (`userId`, `email`, `role`, etc.)
- El cliente envía el token en cada request

---

### 🛡️ Autorización por políticas

La autorización se maneja mediante **policies**, no directamente por roles.

Ejemplo:

```csharp
[Authorize(Policy = "Admin")]
[HttpGet("{userId}")]
public async Task<IActionResult> GetUser(int userId)
{
    ...
}
