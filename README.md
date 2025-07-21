# GrupoEmi

Respuesta Documento
Section 2:
2. Describe how you would implement authentication and authorization in this
API.
R//: Se usa el nugget de Authentication.JwtBearer, configuramos las secretKey para validar el token que se recibe es valido, el tiempo de vencimiento del token, adicional se definen los roles mediante Authorization y se define en los controller el Autorize con el Rol respectivo para todo el controller o endpoint especifico.

3. Explain the concept of middleware in ASP.NET Core. Write a simple custom
middleware that logs the details of each incoming HTTP request.
R//: Es un componente que se puede definir para que se encargue de las solicitudes de las respuesta http y Exception de tal manera que se puedan personalizar o interpretar bajo un standard establecido.

Section 5:
1. What are some common performance issues in .NET applications and how can
you address them?
R//: No hacer uso de AsNoTracking() en consultas que no necesitan seguimiento.
- Darle correcto uso a las peticiones asíncronas con await.
- Cargar solo los registros necesarios en consultas grandes, se puede ayudar con paginación.
- Mala relación entre Entidades sobrecargando las búsquedas.
- Definir muchos Include() en la carga de datos.

2. Describe how you would profile and optimize a slow-running query in an
ASP.NET Core application.
R//: Primero analizaría la consulta e identificaría cuales sería la relación que puede estar generando la lentitud con ayuda del Querystring() analizar la consulta en la BD para optimizarla.
- Daría uso de AsNoTracking().
- Usaría un Select() al query si solo requiero ciertos campos.
- Validar la estructura de las tablas en la Base de Datos si requiero índices que apoyen la consulta.

Para iniciar el proyecto
1. En el archivo appsettings.json está la conexión a la Base de datos y la configuración de JWT
2. Usar el comando "Update-Database" en la Consola de Administrador de paquetes para que se ejecute la migration
3. En la DbContext (EmiDbContext) se cargan algunos datos iniciales en las entidades
4. Se tiene 3 usuarios por defecto 
	- Administrator (Tiene Rol ADM)
	- Admin123
	
	- User1 (Tiene Rol USR)
	- User123*
	
	- User2 (Tiene Rol USR)
	- useR987*
5. Para crear nuevos usuarios se debe crear el token con el Administrator