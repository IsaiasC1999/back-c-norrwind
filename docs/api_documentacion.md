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

---

# Módulo de Empleados (RRHH)

## Base URL
```
http://localhost:5077/api/Empleados
```

---

## Endpoints Disponibles

### 1. Obtener todos los empleados (con filtros opcionales)
**GET** `/api/Empleados`

#### Sin filtros (todos los empleados)
```
GET http://localhost:5077/api/Empleados
```

#### Con filtros
```
GET http://localhost:5077/api/Empleados?Nombre=Ana&FechaIngreso=2023-01-15
```

#### Parámetros de Query (todos opcionales)
| Parámetro | Tipo | Descripción | Ejemplo |
|-----------|------|-------------|---------|
| Nombre | string | Filtrar por nombre o apellido | `Nombre=Ana` |
| FechaIngreso | date | Filtrar por fecha de contratación | `FechaIngreso=2023-01-15` |

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": [
    {
      "employeeId": 1,
      "lastName": "Davolio",
      "firstName": "Nancy",
      "title": "Sales Representative",
      "titleOfCourtesy": "Ms.",
      "birthDate": "1968-12-08",
      "hireDate": "1992-05-01",
      "address": "507 - 20th Ave. E. Apt. 2A",
      "city": "Seattle",
      "region": "WA",
      "postalCode": "98122",
      "country": "USA",
      "homePhone": "(206) 555-9857",
      "extension": "5467",
      "notes": null,
      "reportsTo": 2,
      "username": "ndavolio",
      "role": "usuario",
      "hasUser": true
    }
  ],
  "error": ""
}
```

---

### 2. Obtener empleado por ID
**GET** `/api/Empleados/{id}`

```
GET http://localhost:5077/api/Empleados/1
```

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": {
    "employeeId": 1,
    "lastName": "Davolio",
    "firstName": "Nancy",
    "title": "Sales Representative",
    "titleOfCourtesy": "Ms.",
    "birthDate": "1968-12-08",
    "hireDate": "1992-05-01",
    "address": "507 - 20th Ave. E. Apt. 2A",
    "city": "Seattle",
    "region": "WA",
    "postalCode": "98122",
    "country": "USA",
    "homePhone": "(206) 555-9857",
    "extension": "5467",
    "notes": null,
    "reportsTo": 2,
    "username": "ndavolio",
    "role": "usuario",
    "hasUser": true
  },
  "error": ""
}
```

#### Respuesta Error (404 Not Found)
```json
{
  "success": false,
  "result": null,
  "error": "Empleado no encontrado"
}
```

---

### 3. Crear nuevo empleado
**POST** `/api/Empleados`

```
POST http://localhost:5077/api/Empleados
Content-Type: application/json
```

#### Request Body
```json
{
  "lastName": "Pérez",
  "firstName": "Juan",
  "title": "Analista",
  "titleOfCourtesy": "Sr.",
  "birthDate": "1990-03-15",
  "hireDate": "2024-01-10",
  "address": "Calle Principal 123",
  "city": "Madrid",
  "region": "MD",
  "postalCode": "28001",
  "country": "España",
  "homePhone": "+34 612 345 678",
  "extension": "123",
  "reportsTo": 1,
  "username": "jperez",
  "password": "password123",
  "role": "admin"
}
```

#### Validaciones
- `lastName`: **Obligatorio** - Apellido del empleado
- `firstName`: **Obligatorio** - Nombre del empleado
- `role`: Opcional - Valores permitidos: "admin", "usuario" (por defecto "usuario")

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": {
    "employeeId": 10,
    "lastName": "Pérez",
    "firstName": "Juan",
    "title": "Analista",
    "hasUser": true,
    "username": "jperez"
  },
  "error": ""
}
```

#### Respuesta Error (400 Bad Request)
```json
{
  "success": false,
  "result": null,
  "error": "El nombre es obligatorio"
}
```

---

### 4. Actualizar empleado
**PUT** `/api/Empleados/{id}`

```
PUT http://localhost:5077/api/Empleados/1
Content-Type: application/json
```

#### Request Body
```json
{
  "lastName": "Davolio",
  "firstName": "Nancy Actualizada",
  "title": "Gerente de Ventas",
  "city": "Seattle",
  "homePhone": "(206) 555-9999"
}
```

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": {
    "employeeId": 1,
    "lastName": "Davolio",
    "firstName": "Nancy Actualizada",
    "title": "Gerente de Ventas",
    "city": "Seattle",
    "hasUser": true,
    "username": "ndavolio"
  },
  "error": ""
}
```

#### Respuesta Error (400 Bad Request)
```json
{
  "success": false,
  "result": null,
  "error": "El apellido es obligatorio"
}
```

---

### 5. Eliminar empleado
**DELETE** `/api/Empleados/{id}`

```
DELETE http://localhost:5077/api/Empleados/10
```

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": true,
  "error": ""
}
```

#### Respuesta Error (404 Not Found)
```json
{
  "success": false,
  "result": null,
  "error": "Empleado no encontrado"
}
```

---

## Modelos de Datos

### EmployeeDTO (Respuesta)
```typescript
interface EmployeeDTO {
  employeeId: number;
  lastName: string;
  firstName: string;
  title: string | null;
  titleOfCourtesy: string | null;
  birthDate: string | null;
  hireDate: string | null;
  address: string | null;
  city: string | null;
  region: string | null;
  postalCode: string | null;
  country: string | null;
  homePhone: string | null;
  extension: string | null;
  notes: string | null;
  reportsTo: number | null;
  username: string | null;
  role: string | null;
  hasUser: boolean;
}
```

### EmployeeAddDTO (Request - POST/PUT)
```typescript
interface EmployeeAddDTO {
  lastName: string;
  firstName: string;
  title?: string;
  titleOfCourtesy?: string;
  birthDate?: string;
  hireDate?: string;
  address?: string;
  city?: string;
  region?: string;
  postalCode?: string;
  country?: string;
  homePhone?: string;
  extension?: string;
  notes?: string;
  reportsTo?: number;
  username?: string;
  password?: string;
  role?: string; // "admin" | "usuario" (por defecto "usuario")
}
```

### EmployeeFilter (Query Parameters)
```typescript
interface EmployeeFilter {
  nombre?: string;
  fechaIngreso?: string;
}
```

---

## Notas Importantes

1. **Usuario asociado**: El campo `hasUser` indica si el empleado tiene acceso al sistema
2. **Password**: No se retorna en las respuestas por seguridad
3. **Crear usuario**: Al crear un empleado, se puede incluir `username`, `password` y `role` para generar acceso
4. **Roles permitidos**: Solo "admin" o "usuario" (por defecto "usuario")
5. **Empleados sin usuario**: Se muestran igual con `hasUser: false`
6. **Eliminación en cascada**: Al eliminar un empleado, también se eliminan sus usuarios asociados

---

# Módulo de Clientes

## Base URL
```
http://localhost:5077/api/Clientes
```

---

## Endpoints Disponibles

### 1. Obtener todos los clientes (con filtros opcionales y paginación)
**GET** `/api/Clientes`

#### Sin filtros (primeros 10 clientes)
```
GET http://localhost:5077/api/Clientes
```

#### Con filtros y paginación
```
GET http://localhost:5077/api/Clientes?CompanyName=Alfred&City=Berlin&Country=Germany&Limit=5&Offset=0
```

#### Parámetros de Query (todos opcionales)
| Parámetro | Tipo | Descripción | Ejemplo |
|-----------|------|-------------|---------|
| CompanyName | string | Filtrar por nombre de empresa | `CompanyName=Alfred` |
| ContactName | string | Filtrar por nombre de contacto | `ContactName=Maria` |
| City | string | Filtrar por ciudad | `City=Berlin` |
| Country | string | Filtrar por país | `Country=Germany` |
| Region | string | Filtrar por región | `Region=SP` |
| PostalCode | string | Filtrar por código postal | `PostalCode=12209` |
| Limit | number | Límite de resultados | `Limit=10` |
| Offset | number | Desplazamiento (página) | `Offset=0` |

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": {
    "data": [
      {
        "customerId": "ALFKI",
        "companyName": "Alfreds Futterkiste",
        "contactName": "Maria Anders",
        "contactTitle": "Sales Representative",
        "address": "Obere Str. 57",
        "city": "Berlin",
        "region": null,
        "postalCode": "12209",
        "country": "Germany",
        "phone": "030-0074321",
        "fax": "030-0076545"
      }
    ],
    "total": 91
  },
  "error": ""
}
```

---

### 2. Obtener cliente por ID
**GET** `/api/Clientes/{id}`

```
GET http://localhost:5077/api/Clientes/ALFKI
```

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": {
    "customerId": "ALFKI",
    "companyName": "Alfreds Futterkiste",
    "contactName": "Maria Anders",
    "contactTitle": "Sales Representative",
    "address": "Obere Str. 57",
    "city": "Berlin",
    "region": null,
    "postalCode": "12209",
    "country": "Germany",
    "phone": "030-0074321",
    "fax": "030-0076545"
  },
  "error": ""
}
```

#### Respuesta Error (404 Not Found)
```json
{
  "success": false,
  "result": null,
  "error": "El cliente no existe"
}
```

---

### 3. Obtener cliente con sus órdenes
**GET** `/api/Clientes/{id}/ordenes`

```
GET http://localhost:5077/api/Clientes/ALFKI/ordenes
```

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": {
    "customerId": "ALFKI",
    "companyName": "Alfreds Futterkiste",
    "totalOrders": 6,
    "orders": [
      {
        "orderId": 10835,
        "orderDate": "2024-01-15",
        "requiredDate": "2024-02-12",
        "shippedDate": "2024-01-21",
        "shipCountry": "Germany",
        "employeeName": "Nancy Davolio",
        "totalItems": 28,
        "totalAmount": 267.50
      },
      {
        "orderId": 10692,
        "orderDate": "2023-10-03",
        "requiredDate": "2023-10-31",
        "shippedDate": "2023-10-13",
        "shipCountry": "Germany",
        "employeeName": "Andrew Fuller",
        "totalItems": 14,
        "totalAmount": 878.00
      }
    ]
  },
  "error": ""
}
```

#### Respuesta Error (404 Not Found)
```json
{
  "success": false,
  "result": null,
  "error": "El cliente no existe"
}
```

---

### 4. Crear nuevo cliente
**POST** `/api/Clientes`

```
POST http://localhost:5077/api/Clientes
Content-Type: application/json
```

#### Request Body
```json
{
  "customerId": "TEST1",
  "companyName": "Empresa de Prueba",
  "contactName": "Juan Pérez",
  "contactTitle": "Gerente de Ventas",
  "address": "Calle Principal 123",
  "city": "Madrid",
  "region": "MD",
  "postalCode": "28001",
  "country": "España",
  "phone": "+34 912 345 678",
  "fax": "+34 912 345 679"
}
```

#### Validaciones
- `customerId`: **Obligatorio** - Exactamente 5 caracteres
- `companyName`: **Obligatorio** - Nombre de la empresa

#### Respuesta Exitosa (201 Created)
```json
{
  "success": true,
  "result": {
    "customerId": "TEST1",
    "companyName": "Empresa de Prueba",
    "contactName": "Juan Pérez",
    "contactTitle": "Gerente de Ventas",
    "address": "Calle Principal 123",
    "city": "Madrid",
    "region": "MD",
    "postalCode": "28001",
    "country": "España",
    "phone": "+34 912 345 678",
    "fax": "+34 912 345 679"
  },
  "error": ""
}
```

#### Respuesta Error (400 Bad Request)
```json
{
  "success": false,
  "result": null,
  "error": "El ID del cliente debe tener exactamente 5 caracteres"
}
```

---

### 5. Actualizar cliente
**PUT** `/api/Clientes/{id}`

```
PUT http://localhost:5077/api/Clientes/ALFKI
Content-Type: application/json
```

#### Request Body
```json
{
  "companyName": "Alfreds Futterkiste Actualizado",
  "contactName": "Maria García",
  "contactTitle": "Directora de Ventas",
  "city": "Barcelona",
  "phone": "+34 93 555 1234"
}
```

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": {
    "customerId": "ALFKI",
    "companyName": "Alfreds Futterkiste Actualizado",
    "contactName": "Maria García",
    "contactTitle": "Directora de Ventas",
    "city": "Barcelona",
    "phone": "+34 93 555 1234"
  },
  "error": ""
}
```

#### Respuesta Error (400 Bad Request)
```json
{
  "success": false,
  "result": null,
  "error": "El cliente no existe"
}
```

---

### 6. Eliminar cliente
**DELETE** `/api/Clientes/{id}`

```
DELETE http://localhost:5077/api/Clientes/TEST1
```

#### Respuesta Exitosa (200 OK)
```json
{
  "success": true,
  "result": "Cliente eliminado correctamente",
  "error": ""
}
```

#### Respuesta Error (400 Bad Request)
```json
{
  "success": false,
  "result": null,
  "error": "El cliente no existe o no se pudo eliminar"
}
```

---

## Modelos de Datos

### CustomerDTO (Respuesta)
```typescript
interface CustomerDTO {
  customerId: string;
  companyName: string;
  contactName: string | null;
  contactTitle: string | null;
  address: string | null;
  city: string | null;
  region: string | null;
  postalCode: string | null;
  country: string | null;
  phone: string | null;
  fax: string | null;
}
```

### CustomerAddDTO (Request - POST/PUT)
```typescript
interface CustomerAddDTO {
  customerId: string;
  companyName: string;
  contactName?: string;
  contactTitle?: string;
  address?: string;
  city?: string;
  region?: string;
  postalCode?: string;
  country?: string;
  phone?: string;
  fax?: string;
}
```

### CustomerFilter (Query Parameters)
```typescript
interface CustomerFilter {
  companyName?: string;
  contactName?: string;
  city?: string;
  country?: string;
  region?: string;
  postalCode?: string;
  limit?: number;
  offset?: number;
}
```

### CustomerOrdersDTO (Cliente con órdenes)
```typescript
interface CustomerOrdersDTO {
  customerId: string;
  companyName: string;
  totalOrders: number;
  orders: OrderSummaryDTO[];
}
```

### OrderSummaryDTO (Resumen de orden)
```typescript
interface OrderSummaryDTO {
  orderId: number;
  orderDate: string | null;
  requiredDate: string | null;
  shippedDate: string | null;
  shipCountry: string | null;
  employeeName: string;
  totalItems: number;
  totalAmount: number | null;
}
```

---

## Ejemplos de Uso

### Fetch API
```javascript
// Obtener todos los clientes
const response = await fetch('http://localhost:5077/api/Clientes');
const data = await response.json();

// Filtrar y paginar
const response = await fetch('http://localhost:5077/api/Clientes?Country=Germany&Limit=5&Offset=0');
const data = await response.json();

// Obtener cliente con órdenes
const response = await fetch('http://localhost:5077/api/Clientes/ALFKI/ordenes');
const data = await response.json();

// Crear cliente
const response = await fetch('http://localhost:5077/api/Clientes', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    customerId: 'TEST1',
    companyName: 'Empresa de Prueba',
    contactName: 'Juan Pérez'
  })
});

// Actualizar cliente
const response = await fetch('http://localhost:5077/api/Clientes/ALFKI', {
  method: 'PUT',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    companyName: 'Empresa Actualizada',
    city: 'Madrid'
  })
});

// Eliminar cliente
await fetch('http://localhost:5077/api/Clientes/TEST1', {
  method: 'DELETE'
});
```

---

## Notas Importantes

1. **ID de cliente**: String de exactamente 5 caracteres (ej: "ALFKI", "TEST1")
2. **Paginación**: Usar `Limit` y `Offset` para paginar resultados
3. **Total de resultados**: El campo `total` en la respuesta indica el total de registros
4. **Órdenes del cliente**: Usar el endpoint `/{id}/ordenes` para ver las órdenes asociadas
5. **Total de órdenes**: El campo `totalOrders` indica cuántas órdenes tiene el cliente
6. **Monto total**: `totalAmount` calcula la suma de (cantidad × precio × descuento) de cada orden
