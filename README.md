# ADO.NET Helper para Oracle (Update)

Todos los programas que utilizan las clases e interfaces del proveedor predeterminado de ADO.NET para Oracle que se encuentran en el ensamblado System.Data.Oraclient deben actualizarse (a partir de la version del framework 4.0) por las clases e interfaces que se encuentran en el ensamblado Oracle.DataAccess.Client, que es el proveedor nativo de Oracle para ADO.NET.

A continuación el código actualizado de las clases Helper de ADO.NET que publique en este post.

Como ejemplo del uso del helper, tenemos el código de la clase CustomerDAC cuya funcionalidad es tener los métodos CRUD para una entidad Customer.


Para finalizar el código de una aplicación de consola que muestra el uso de la clases del Helper para implementar todos los metodos CRUD (Create,Retrieve, Update, Delete).

