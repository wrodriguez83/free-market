
# FreeMarket
Esta aplicación fué creada con **.Net 9.0** y hace uso de las siguientes tecnologías: 
* Inyección de dependencias.
* Acceso a una base de datos **MS SqlServer** a través de la libreria **SqlClient**.
* Acceso a la api `https://fakestoreapi.com` para el listado de productos.
* Unit tests con **Moq** como librería de Mock.

## Prerequisitos
* Tener acceso a una instancia de SqlServer y configurarla en el archivo `appsettings.json` y en `FreeMarket.DB.publish.xml`

## Instrucciones
* Recompilar la solución (Esto impactará el modelo de datos a la instancia de SqlServer)
* Asegurarse que el proyecto FreeMarket sea el proyecto de inicio
* Ejecutar

## Apis endpoints
### Obtener todos los productos
**GET** `http://localhost:5024/api/products`

### Obtener un producto por Id
**GET** `http://localhost:5024/api/products/{id del producto}`

### Obtener todos los carritos de compra
**GET** `http://localhost:5024/api/carts`

### Obtener un carrito de compra
**GET** `http://localhost:5024/api/carts/{id del carrito}`

### Insertar un carrito de compra
**POST** `http://localhost:5024/api/carts`
* El body debe tener esta estructura:
```json
{
  "name":{nombre del carrito -> string},
  "items":[
    {
      "Price":{precio -> double},
      "Quantity":{cantidad -> int},
        "Product":{producto -> el que se obtiene en el GET de productos (campo Data)}
    }
  ]
}
```
* Ejemplo:
```json
{
  "name":"Carrito de prueba",
  "items":[
    {
      "Price":1129.95,
      "Quantity":3,
      "Product":{
        "id":1,
        "title":"Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops",
        "price":109.95,
        "description":"Your perfect pack for everyday use and walks in the forest. Stash your laptop (up to 15 inches) in the padded sleeve, your everyday",
        "category":"men's clothing",
        "image":"https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg",
        "rating":{
          "rate":3.9,
          "count":120
        }
      }
    },
    {
      "Price":11.95,
      "Quantity":1,
      "Product":{
        "id":3,
        "title":"Mens Cotton Jacket",
        "price":55.99,
        "description":"great outerwear jackets for Spring/Autumn/Winter, suitable for many occasions, such as working, hiking, camping, mountain/rock climbing, cycling, traveling or other outdoors. Good gift choice for you or your family member. A warm hearted love to Father, husband or son in this thanksgiving or Christmas Day.",
        "category":"men's clothing",
        "image":"https://fakestoreapi.com/img/71li-ujtlUL._AC_UX679_.jpg",
        "rating":{
          "rate":4.7,
          "count":500
        }
      }
    }
  ]
}
```

### Actualizar un carrito de compra
**POST** `http://localhost:5024/api/carts`
* El body debe tener esta estructura:
```json
{
  "id":{id del carrito -> int},
  "name":{nombre del carrito -> string},
  "items":[
    {
      "Price":{precio -> double},
      "Quantity":{cantidad -> int},
        "Product":{producto -> el que se obtiene en el GET de productos (campo Data)}
    }
  ]
}
```

### Borrar un carrito
**DELETE** `http://localhost:5024/api/carts/{id del carrito}`
