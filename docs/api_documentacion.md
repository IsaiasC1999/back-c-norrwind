# Documentación API - Módulo de Productos

## Base URL
```
http://localhost:5077/api/Productos
```

---

## Endpoints Disponibles

### 1. Obtener todos los productos (con filtros opcionales)
**GET** `/api/Productos`

#### Sin filtros (todos los productos)
```
GET http://localhost:5077/api/Productos
```

#### Con filtros
```
GET http://localhost:5077/api/Productos?Category=Beverages&PriceMin=10&PriceMax=50&Supplier=Exotic+Liquids
```

#### Parámetros de Query (todos opcionales)
| Parámetro | Tipo | Descripción | Ejemplo |
|-----------|------|-------------|---------|
| Category | string | Filtrar por categoría | `Category=Beverages` |
| PriceMin | number | Precio mínimo | `PriceMin=10` |
| PriceMax | number | Precio máximo | `PriceMax=50` |
| Supplier | string | Filtrar por proveedor | `Supplier=Exotic+Liquids` |

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": [
    {
      "productId": 1,
      "productName": "Chai",
      "companyName": "Exotic Liquids",
      "categoryName": "Beverages",
      "description": "Soft drinks, coffees, teas, beers, and ales",
      "quantityPerUnit": "10 boxes x 20 bags",
      "unitPrice": 18,
      "unitsInStock": 39,
      "unitsOnOrder": 0,
      "reorderLevel": 10,
      "discontinued": 0
    }
  ],
  "error": ""
}
```

---

### 2. Obtener producto por ID
**GET** `/api/Productos/{id}`

```
GET http://localhost:5077/api/Productos/1
```

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": {
    "productId": 1,
    "productName": "Chai",
    "companyName": "Exotic Liquids",
    "categoryName": "Beverages",
    "description": "Soft drinks, coffees, teas, beers, and ales",
    "quantityPerUnit": "10 boxes x 20 bags",
    "unitPrice": 18,
    "unitsInStock": 39,
    "unitsOnOrder": 0,
    "reorderLevel": 10,
    "discontinued": 0
  },
  "error": ""
}
```

#### Respuesta Error (404 Not Found)
```json
{
  "success": false,
  "result": null,
  "error": "El producto con el id que ingreso no existe"
}
```

---

### 3. Listar todas las categorías
**GET** `/api/Productos/categorias`

```
GET http://localhost:5077/api/Productos/categorias
```

#### Respuesta (200 OK)
```json
{
  "success": true,
  "result": [
    {
      "categoryId": 1,
      "categoryName": "Beverages",
      "description": "Soft drinks, coffees, teas, beers, and ales"
    },
    {
      "categoryId": 2,
      "categoryName": "Condiments",
      "description": "Sweet and savory sauces, relishes, spreads"
    }
  ],
  "error": ""
}
```

---

### 4. Crear nuevo producto
**POST** `/api/Productos`

```
POST http://localhost:5077/api/Productos
Content-Type: application/json
```

#### Request Body
```json
{
  "productId": 78,
  "productName": "Nuevo Producto",
  "supplierId": 1,
  "categoryId": 1,
  "quantityPerUnit": "12 boxes",
  "unitPrice": 25.50,
  "unitsInStock": 100,
  "unitsOnOrder": 0,
  "reorderLevel": 10,
  "discontinued": 0
}
```

#### Validaciones
- `productName`: **Obligatorio** - No puede estar vacío
- `unitPrice`: No puede ser negativo

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": "Producto agregado correctamente",
  "error": ""
}
```

#### Respuesta Error (400 Bad Request)
```json
{
  "success": false,
  "result": null,
  "error": "El nombre del producto es obligatorio"
}
```

---

### 5. Actualizar producto
**PUT** `/api/Productos/{id}`

```
PUT http://localhost:5077/api/Productos/1
Content-Type: application/json
```

#### Request Body
```json
{
  "productName": "Chai Actualizado",
  "supplierId": 1,
  "categoryId": 1,
  "quantityPerUnit": "10 boxes x 30 bags",
  "unitPrice": 20.00,
  "unitsInStock": 50,
  "unitsOnOrder": 10,
  "reorderLevel": 15,
  "discontinued": 0
}
```

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": "Producto actualizado correctamente",
  "error": ""
}
```

#### Respuesta Error (400 Bad Request)
```json
{
  "success": false,
  "result": null,
  "error": "El nombre del producto es obligatorio"
}
```

---

### 6. Eliminar producto
**DELETE** `/api/Productos/{id}`

```
DELETE http://localhost:5077/api/Productos/78
```

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": "Producto eliminado correctamente",
  "error": ""
}
```

#### Respuesta Error (404 Not Found)
```json
{
  "success": false,
  "result": null,
  "error": "El producto no existe o no se pudo eliminar"
}
```

---

## Modelos de Datos

### ProductDTO (Respuesta)
```typescript
interface ProductDTO {
  productId: number;
  productName: string;
  companyName: string;
  categoryName: string;
  description: string;
  quantityPerUnit: string | null;
  unitPrice: number | null;
  unitsInStock: number | null;
  unitsOnOrder: number | null;
  reorderLevel: number | null;
  discontinued: number;
}
```

### ProducAddDTO (Request - POST/PUT)
```typescript
interface ProducAddDTO {
  productId: number;
  productName: string;
  supplierId: number | null;
  categoryId: number | null;
  quantityPerUnit: string | null;
  unitPrice: number | null;
  unitsInStock: number | null;
  unitsOnOrder: number | null;
  reorderLevel: number | null;
  discontinued: number;
}
```

### ProductFilter (Query Parameters)
```typescript
interface ProductFilter {
  category?: string;
  priceMin?: number;
  priceMax?: number;
  supplier?: string;
}
```

### ResponseServices (Respuesta estándar)
```typescript
interface ResponseServices<T> {
  success: boolean;
  result: T | null;
  error: string;
}
```

---

## Códigos de Estado HTTP

| Código | Descripción |
|--------|-------------|
| 200 | OK - Solicitud exitosa |
| 400 | Bad Request - Error en validación |
| 404 | Not Found - Recurso no encontrado |

---

## Ejemplos de Uso en Frontend

### Fetch API
```javascript
// Obtener todos los productos
const response = await fetch('http://localhost:5077/api/Productos');
const data = await response.json();

// Filtrar productos
const response = await fetch('http://localhost:5077/api/Productos?Category=Beverages&PriceMin=10&PriceMax=50');
const data = await response.json();

// Crear producto
const response = await fetch('http://localhost:5077/api/Productos', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    productId: 78,
    productName: 'Nuevo Producto',
    supplierId: 1,
    categoryId: 1,
    unitPrice: 25.50
  })
});
```

### Axios
```javascript
import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5077/api/Productos'
});

// Listar productos
const { data } = await api.get('');

// Filtrar
const { data } = await api.get('', { 
  params: { category: 'Beverages', priceMin: 10 } 
});

// Crear
const { data } = await api.post('', productData);
```

---

## Notas Importantes

1. **Ruta correcta**: Usar `Productos` (no `ProductosControllers`)
2. **Siempre verificar `success`**: Antes de procesar el resultado
3. **Manejo de errores**: El campo `error` contiene el mensaje de error
4. **IDs de categoría/supplier**: Consultar primero las categorías disponibles
