# AGENTS.md - Guía para Agentes IA

Este archivo contiene las directrices y convenciones del proyecto para agentes de IA que trabajen en este código.

---

## 1. Comandos del Proyecto

### Comandos principales
| Comando | Descripción |
|---------|-------------|
| `dotnet build` | Compilar el proyecto |
| `dotnet run` | Ejecutar la aplicación |
| `dotnet restore` | Restaurar dependencias |
| `dotnet test` | Ejecutar todas las pruebas |

### Ejecutar una prueba específica
```bash
dotnet test --filter "FullyQualifiedName~NombreClase.NombreMetodo"
```

### Entity Framework
```bash
dotnet ef migrations add <Nombre>    # Crear migración
dotnet ef database update            # Aplicar migraciones
```

---

## 2. Estructura del Proyecto

```
├── Controllers/       # Controladores API (rutas)
├── services/          # Lógica de negocio
├── repositorio/      # Acceso a datos (Repository Pattern)
├── DTOs/             # Objetos de transferencia de datos
├── mappers/          # Clases de mapeo entidad↔DTO
├── dbContext/        # Entity Framework context y entidades
├── interfacez/       # Interfaces
├── models/           # Modelos auxiliares
└── docs/             # Documentación API
```

---

## 3. Convenciones de Código

### Nombrado
- **Clases/Interfaces**: PascalCase (ej: `ProductosControllers`, `IRepositorioProdcutos`)
- **Métodos**: PascalCase (ej: `GetAllProducts`, `DeleteProduct`)
- **Campos privados**: camelCase (ej: `productsServices`, `repo`)
- **Interfaces**: Prefijo `I` (ej: `IRepositorioProdcutos`)
- **DTOs**: Sufijo `DTO`, `AddDTO`, `Filter` (ej: `ProductDTO`, `ProductFilter`)

### Imports (orden recomendado)
```csharp
using System...;
using Microsoft...;
using ef_nortwith.dbContext;
using ef_nortwith.DTOs;
using ef_nortwith.repositorio;
```

### Arquitectura
El proyecto sigue el patrón: **Controller → Service → Repository → Entity Framework**

```
Controller (API) → Service (lógica) → Repository (datos) → EF Core (DB)
```

### Rutas de API
- Usar prefijo `api/` en todas las rutas
- Nombre del controlador en español (ej: `Productos`, `Ordenes`)
- Ejemplo: `[Route("api/Productos")]`
- No usar `[Route("[controller]")]` - genera nombres incorrectos

### Respuestas API
Usar la clase `ResponseServices` para respuestas:
```csharp
return new ResponseServices
{
    Success = true,
    Result = datos,
    Error = ""
};
```

### Manejo de Errores
- Validaciones en Controller antes de llamar al Service
- Retornar `BadRequest` con mensaje de error específico
- Retornar `NotFound` cuando el recurso no existe
- Verificar `Success` antes de procesar resultados

### Null Safety (OBLIGATORIO)
- **SIEMPRE** usar operadores `?.` y `??` para evitar errores con objetos nulos
- Ejemplo: `CompanyName = p.Supplier?.CompanyName ?? ""`
- Antes de hacer commit, verificar que no haya warnings de null

---

## 4. Reglas para Trabajo Autónomo

### Antes de cada cambio
1. Leer y entender el código existente
2. Identificar patrones y convenciones del archivo
3. Verificar qué archivos necesitan modificarse

### Después de cada cambio
1. **SIEMPRE** ejecutar `dotnet build` para verificar
2. Actualizar `docs/api_documentacion.md` si se agregaron/modificaron endpoints
3. No dejar warnings de compilación sin resolver

### Para nuevos módulos
1. Crear Controller con ruta `api/NombreModulo`
2. Crear Service con lógica de negocio
3. Crear Repository para acceso a datos
4. Crear Interfaz `IRepositorioNombre`
5. Registrar en `Program.cs` con `AddTransient`
6. Documentar endpoints en `docs/api_documentacion.md`

---

## 5. DTOs y Modelos

### Ubicación
- Todos los DTOs van en la carpeta `DTOs/`
- Usar sufijos: `DTO` para respuestas, `AddDTO` o `Filter` para solicitudes

### Ejemplos
```csharp
// DTO de respuesta
public class ProductDTO { ... }

// DTO para crear/actualizar
public class ProducAddDTO { ... }

// DTO para filtros
public class ProductFilter { ... }
```

---

## 6. Mappers

- Ubicación: `mappers/Mappers.cs`
- Clase estática con métodos estáticos
- Naming: `EntityToDTO` y `DTOToEntity`
- **Siempre usar null safety** (`?.` y `??`)

```csharp
public static class Mappers
{
    public static ProductDTO ProducEntityToProductoDTO(Product p) { ... }
    public static Product ProductDtoByProducEntity(ProducAddDTO p) { ... }
}
```

---

## 7. Entity Framework

### DbContext
- Ubicación: `dbContext/NorthwindContext.cs`
- Configuración en `Program.cs`

### Relaciones
- Usar `Include()` para cargar datos relacionados
- Ejemplo: `db.Products.Include(p => p.Supplier).Include(p => p.Category)`

---

## 8. Dependencias

### Paquetes NuGet
- `Microsoft.EntityFrameworkCore.Design`
- `Npgsql.EntityFrameworkCore.PostgreSQL`
- `Dapper`
- `Swashbuckle` (Swagger)

### Inyección de Dependencias
Registrar en `Program.cs`:
```csharp
builder.Services.AddTransient<ProductsServices>();
builder.Services.AddTransient<IRepositorioProdcutos, RepositorioProductos>();
```

---

## 9. Documentación

### Estructura de documentación
- `docs/api_documentacion.md` - Documentación de endpoints
- Actualizar cuando se agreguen/modifiquen endpoints

### Al agregar un nuevo endpoint
1. Agregar descripción del endpoint
2. Incluir método HTTP y ruta
3. Mostrar ejemplo de request
4. Mostrar ejemplos de response (success y error)
5. Documentar parámetros si aplica

---

## 10. Notas Importantes

- Este proyecto usa **.NET 8.0** con **Entity Framework Core** y **PostgreSQL**
- No eliminar la carpeta `bin/` ni `obj/` del repo (ya están en .gitignore)
- **Siempre** ejecutar `dotnet build` después de hacer cambios
- **Siempre** actualizar documentación al modificar endpoints
- Verificar que no haya advertencias de nullables antes de commit
