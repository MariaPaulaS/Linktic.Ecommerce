# Linktic.Ecommerce

Este proyecto simula un e-commerce simplificado que permite manejar un catálogo de productos y pedidos de los clientes sobre esos mismos productos.

A continuación, se realizará la simulación y ejecución de la parte backend de la arquitectura propuesta, lo que incluye el balanceador de carga y la ejecución de las lambdas, junto con el consumo a DynamoDB. Para realizar la simulación completa, siga los siguientes pasos:

## 1. Descargar y configurar Nginx

Nginx es un servidor web que se puede usar localmente para simular el funcionamiento de un balanceador de carga. Con Nginx, se puede utilizar la dirección http://localhost como si fuera una dirección DNS de un balanceador de carga. Esto permite redirigir las solicitudes a cualquiera de los servicios asociados a él. Dependiendo de la ruta que se consuma, Nginx redirigirá la solicitud al servicio correspondiente.

Para poder hacer esta simulación, siga los siguientes pasos:

**1.** Descargue Nginx y guarde el contenido del .zip descargado en el disco local C, o en alguna otra dirección accesible. Puede descargarlo desde https://nginx.org/en/download.html

**2.** Dirijase a la ruta Nginx/conf, y reemplace el archivo nginx.conf por el archivo adjunto en el repositorio.

**3.** Dentro de la carpeta Nginx, ejecute la aplicación dando doble clic al archivo nginx, o abra un CMD y ejecute el comando **start nginx**. Cuando quiera detenerlo, abra el Administrador de tareas y detenga el programa desde allí.

El archivo nginx.config contiene la siguiente configuración:

![image](https://github.com/MariaPaulaS/Linktic.Ecommerce/assets/37190986/9bdc6339-7f91-43aa-84af-9efab44419de)

En Nginx, se especifica la dirección a la que responde cada servidor (ProductCatalog y OrderManagement) y se configura el servidor principal que gestionará todas las peticiones, en este caso localhost en el puerto 80. Dependiendo de la ruta a la que se haga la petición, Nginx redirigirá la solicitud al servidor correspondiente.

## 2. Ejecutar LocalStack

Para ejecutar LocalStack, primero necesita tener instalado Docker en su sistema. Luego, abra una consola de comandos (cmd) y ejecute lo siguiente:

**docker run --rm -it -p 127.0.0.1:4566:4566 -p 127.0.0.1:4510-4559:4510-4559 -v /var/run/docker.sock:/var/run/docker.sock localstack/localstack**

Con esto se descargará la imagen de LocalStack y se ejecutará un contenedor que permitirá simular los servicios de AWS necesarios para realizar la prueba. Cuando el contenedor se detenga, toda la información almacenada en él se eliminará, como los datos guardados en base de datos y tablas entre otros. Cada servicio backend se encarga de crear información y poblar datos en LocalStack, por lo que es importante que el contenedor esté corriendo antes de la ejecución de estos.


## 3. Ejecutar los servicios backend

Ejecute ambos proyectos de forma simultánea. Ambos usan .NET 8.0, así que asegúrese de tenerlo instalado. De no tenerlo, puede instalarlo desde aqui: https://dotnet.microsoft.com/es-es/download/dotnet/8.0

Es necesario tener instalado un IDE configurado con .NET 8.0, o por el contrario, vaya a la carpeta Api de cada proyecto y ejecute los siguientes comandos:

`dotnet restore` -> Restaura las dependencias del proyecto

`dotnet build ` -> Compila el proyecto

`dotnet run` -> Ejecuta el proyecto

El proyecto ProductCatalog se ejecuta en el puerto 50001, mientras que OrderManagement se ejecuta en el puerto 50002. Estos puertos están configurados en el archivo launchSettings.json dentro de cada proyecto.

Si los proyectos no están corriendo en estos puertos, necesita asegurarse de que la configuración de ejecución esté utilizando el archivo launchSettings.json. Para hacerlo, siga estos pasos en el IDE:

**1.**  Antes de ejecutar el proyecto, haga clic en "Editar configuración de ejecución".

**2.** Asegurese de que la configuración de ejecución esté utilizando el archivo launchSettings.json.

Es crucial que ambos proyectos se ejecuten en estos puertos, ya que Nginx está configurado para redirigir las solicitudes a estas direcciones.

Una vez hecho esto, podrá usar la colección de Postman adjunta en el repositorio para consumir los diferentes endpoints. Importe la colección desde Postman, y sobre la misma de clic en los tres puntos adyacentes, para luego seleccioanr "View Documentation". Allí podrá ver la forma de uso de los diferentes métodos con sus posibles respuestas.

## Información adicional

Aunque esto no es esencial para que el proyecto se ejecute localmente, los proyectos tienen la configuración necesaria para desplegarse en lambdas normales de AWS, en un caso hipotético en el que se usara ECR para el almacenamiento del código. Esto puede verse en la configuración de los Dockerfile de cada proyecto, y el LambdaEntryPoint, que es la configuración que el proyecto usaría para ejecutarse si estuviera desplegado en una lambda de AWS.
