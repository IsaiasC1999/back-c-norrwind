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

# Módulo de Órdenes

## Base URL
```
http://localhost:5077/api/Ordenes
```

---

## Endpoints Disponibles

### 1. Obtener todas las órdenes (con filtros opcionales)
**GET** `/api/Ordenes`

#### Sin filtros (todas las órdenes)
```
GET http://localhost:5077/api/Ordenes
```

#### Con filtros
```
GET http://localhost:5077/api/Ordenes?Cliente=Bebidas&FechaInicio=2023-01-01&FechaFin=2023-12-31&EmpleadoId=1
```

#### Parámetros de Query (todos opcionales)
| Parámetro | Tipo | Descripción | Ejemplo |
|-----------|------|-------------|---------|
| Cliente | string | Filtrar por nombre de cliente | `Cliente=Bebidas` |
| FechaInicio | date | Fecha mínima de orden | `FechaInicio=2023-01-01` |
| FechaFin | date | Fecha máxima de orden | `FechaFin=2023-12-31` |
| EmpleadoId | number | Filtrar por empleado | `EmpleadoId=1` |

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": [
    {
      "orderId": 10248,
      "nameCustomer": "Ernst Handel",
      "nameEmployes": "Steven Buchanan",
      "orderDate": "2023-07-04",
      "requiredDate": "2023-08-01",
      "shippedDate": "2023-07-16"
    }
  ],
  "error": ""
}
```

---

### 2. Obtener orden por ID
**GET** `/api/Ordenes/{id}`

```
GET http://localhost:5077/api/Ordenes/10248
```

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": {
    "orderId": 10248,
    "nameCustomer": "Ernst Handel",
    "nameEmployes": "Steven Buchanan",
    "orderDate": "2023-07-04",
    "requiredDate": "2023-08-01",
    "shippedDate": "2023-07-16",
    "shipAddress": "Kirchgasse 6",
    "shipCity": "Graz",
    "shipRegion": null,
    "shipPostalCode": "8010",
    "shipCountry": "Austria",
    "freight": 32.38
  },
  "error": ""
}
```

#### Respuesta Error (404 Not Found)
```json
{
  "success": false,
  "result": null,
  "error": "La orden no existe"
}
```

---

### 3. Obtener detalle de orden
**GET** `/api/Ordenes/{id}/detalle`

```
GET http://localhost:5077/api/Ordenes/10248/detalle
```

#### Respuesta (200 OK)
```json
{
  "success": true,
  "result": {
    "discount": 0,
    "quantity": 12,
    "unitPrice": 14,
    "productDTO": {
      "productId": 11,
      "productName": "Queso Cabrales",
      "companyName": "Cooperativa de Quesos",
      "categoryName": "Dairy Products",
      "description": "Cheeses",
      "quantityPerUnit": "1 kg pkg.",
      "unitPrice": 14,
      "unitsInStock": 22,
      "unitsOnOrder": 30,
      "reorderLevel": 10,
      "discontinued": 0
    },
    "employeeDTO": {
      "firstName": "Steven",
      "lastName": "Buchanan",
      "title": "Sales Manager",
      "homePhone": "(71) 555-4848"
    }
  },
  "error": ""
}
```

---

### 4. Crear nueva orden
**POST** `/api/Ordenes`

```
POST http://localhost:5077/api/Ordenes
Content-Type: application/json
```

#### Request Body
```json
{
  "customerId": "ALFKI",
  "employeeId": 1,
  "orderDate": "2024-01-15T00:00:00",
  "requiredDate": "2024-02-15T00:00:00",
  "shippedDate": "2024-01-20T00:00:00",
  "shipVia": 1,
  "freight": 50.00,
  "shipName": "Alfreds Futterkiste",
  "shipAddress": "Obere Str. 57",
  "shipCity": "Berlin",
  "shipRegion": null,
  "shipPostalCode": "12209",
  "shipCountry": "Germany"
}
```

#### Validaciones
- `customerId`: **Obligatorio** - ID del cliente
- `employeeId`: **Obligatorio** - ID del empleado

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": "Orden creada correctamente",
  "error": ""
}
```

#### Respuesta Error (400 Bad Request)
```json
{
  "success": false,
  "result": null,
  "error": "El cliente es obligatorio"
}
```

---

### 5. Actualizar orden
**PUT** `/api/Ordenes/{id}`

```
PUT http://localhost:5077/api/Ordenes/10248
Content-Type: application/json
```

#### Request Body
```json
{
  "customerId": "ALFKI",
  "employeeId": 2,
  "requiredDate": "2024-03-01T00:00:00",
  "freight": 75.00
}
```

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": "Orden actualizada correctamente",
  "error": ""
}
```

#### Respuesta Error (400 Bad Request)
```json
{
  "success": false,
  "result": null,
  "error": "El cliente es obligatorio"
}
```

---

### 6. Eliminar orden
**DELETE** `/api/Ordenes/{id}`

```
DELETE http://localhost:5077/api/Ordenes/10248
```

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": "Orden eliminada correctamente",
  "error": ""
}
```

#### Respuesta Error (404 Not Found)
```json
{
  "success": false,
  "result": null,
  "error": "La orden no existe o no se pudo eliminar"
}
```

---

## Modelos de Datos

### OrderListDTO (Lista de órdenes)
```typescript
interface OrderListDTO {
  orderId: number;
  nameCustomer: string;
  nameEmployes: string;
  orderDate: string | null;
  requiredDate: string | null;
  shippedDate: string | null;
}
```

### OrderDTO (Detalle de orden)
```typescript
interface OrderDTO {
  orderId: number;
  nameCustomer: string;
  nameEmployes: string;
  orderDate: string | null;
  requiredDate: string | null;
  shippedDate: string | null;
  shipAddress: string | null;
  shipCity: string | null;
  shipRegion: string | null;
  shipPostalCode: string | null;
  shipCountry: string | null;
  freight: number | null;
}
```

### OrderFilter (Query Parameters)
```typescript
interface OrderFilter {
  cliente?: string;
  fechaInicio?: string;
  fechaFin?: string;
  empleadoId?: number;
}
```

### OrderAddDTO (Request - POST/PUT)
```typescript
interface OrderAddDTO {
  customerId: string;
  employeeId: number;
  orderDate?: string;
  requiredDate?: string;
  shippedDate?: string;
  shipVia?: number;
  freight?: number;
  shipName?: string;
  shipAddress?: string;
  shipCity?: string;
  shipRegion?: string;
  shipPostalCode?: string;
  shipCountry?: string;
}
```

---

## Ejemplos de Uso

### Fetch API
```javascript
// Obtener todas las órdenes
const response = await fetch('http://localhost:5077/api/Ordenes');
const data = await response.json();

// Filtrar órdenes
const response = await fetch('http://localhost:5077/api/Ordenes?FechaInicio=2023-01-01&FechaFin=2023-12-31');
const data = await response.json();

// Crear orden
const response = await fetch('http://localhost:5077/api/Ordenes', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    customerId: 'ALFKI',
    employeeId: 1,
    freight: 50.00
  })
});
```

---

## Notas Importantes

1. **Ruta correcta**: Usar `Ordenes` (no `OrdenesControllers`)
2. **Siempre verificar `success`**: Antes de procesar el resultado
3. **Manejo de errores**: El campo `error` contiene el mensaje de error
4. **IDs de cliente/empleado**: Verificar que existan antes de crear una orden
