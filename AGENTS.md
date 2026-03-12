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
├── repositorio/       # Acceso a datos (Repository Pattern)
├── DTOs/              # Objetos de transferencia de datos
├── mappers/           # Clases de mapeo entidad↔DTO
├── dbContext/         # Entity Framework context y entidades
├── interfacez/        # Interfaces
├── models/            # Modelos auxiliares
└── docs/             # Documentación API
```

---

## 3. Convenciones de Código

### Nombrado
- **Clases/Interfaces**: PascalCase (ej: `ProductosControllers`, `IRepositorioProdcutos`)
- **Métodos**: PascalCase (ej: `GetAllProducts`, `DeleteProduct`)
- **Campos privados**: camelCase (ej: `productsServices`, `repo`)
- **Interfaces**: Prefijo `I` (ej: `IRepositorioProdcutos`)

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

### Null Safety
- Usar operadores `?.` y `?? ""` para evitar errores con objetos nulos
- Ejemplo: `CompanyName = p.Supplier?.CompanyName ?? ""`

---

## 4. DTOs y Modelos

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

## 5. Mappers

- Ubicación: `mappers/Mappers.cs`
- Clase estática con métodos estáticos
- Naming: `EntityToDTO` y `DTOToEntity`

```csharp
public static class Mappers
{
    public static ProductDTO ProducEntityToProductoDTO(Product p) { ... }
    public static Product ProductDtoByProducEntity(ProducAddDTO p) { ... }
}
```

---

## 6. Entity Framework

### DbContext
- Ubicación: `dbContext/NorthwindContext.cs`
- Configuración en `Program.cs`

### Relaciones
- Usar `Include()` para cargar datos relacionados
- Ejemplo: `db.Products.Include(p => p.Supplier).Include(p => p.Category)`

---

## 7. Dependencias

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

## 8. Documentación

### Documentación API
- Ubicación: `docs/api_documentacion.md`
- Actualizar cuando se agreguen/modifiquen endpoints

---

## 9. Notas Importantes

- Este proyecto usa **.NET 8.0** con **Entity Framework Core** y **PostgreSQL**
- No eliminar la carpeta `bin/` ni `obj/` del repo (ya están en .gitignore)
- Sempre ejecutar `dotnet build` después de hacer cambios
- Verificar que no haya advertencias de nullables antes de commit
