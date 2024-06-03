# Linktic.Ecommerce

Este proyecto simula un e-commerce simplificado que permite manejar un catálogo de productos y pedidos de los clientes sobre esos mismos productos.

A continuación, se realizará la simulación y ejecución de la parte backend de la arquitectura propuesta, lo que incluye el balanceador de carga y la ejecución de las lambdas, junto con el consumo a DynamoDB. Para realizar la simulación completa, siga los siguientes pasos:

## 1. Descargar y configurar Nginx

Nginx es un servidor web que se puede usar de forma local para simular el funcionamiento de un balanceador de carga. Con él, se usará la direccion http://localhost como si fuera una direccion DNS de un load balancer, que al consumirla permite redireccionar a cualquiera de los servicios que estén asociados a él. Dependiendo de la ruta que se consuma, el balanceador de carga redirigirá a un servicio o al otro.

Para poder hacer esta simulación, siga los siguientes pasos:

**1.** Descargue Nginx y guarde el contenido del .zip descargado en el disco local C, o en alguna otra dirección accesible. Puede descargarlo desde https://nginx.org/en/download.html

**2.** Dirijase a la ruta Nginx/conf, y reemplace el archivo nginx.conf por el archivo adjunto en el repositorio.

**3.** Dentro de la carpeta Nginx, ejecute el instalable, o abra un CMD y ejecute el comando **start nginx**. Cuando quiera detenerlo, abra el Administrador de tareas y detenga el programa desde allí.


## 2. Ejecutar LocalStack

Para ejecutar LocalStack, primero necesita tener instalado Docker en su sistema. Luego, abra una consola de comandos (cmd) y ejecute lo siguiente:

**docker run --rm -it -p 127.0.0.1:4566:4566 -p 127.0.0.1:4510-4559:4510-4559 -v /var/run/docker.sock:/var/run/docker.sock localstack/localstack**

Con esto se descargará la imagen de LocalStack y se ejecutará un contenedor que permitirá simular los servicios de AWS necesarios para realizar la prueba. Cuando el contenedor se detenga, toda la información almacenada en él se eliminará, como los datos guardados en base de datos y tablas entre otros.


## 3. Ejecutar los proyectos

Ejecute ambos proyectos de forma simultánea. Ambos usan .NET 8.0, así que asegúrese de tenerlo instalado. Es necesario tener instalado un IDE configurado con .NET 8.0, o por el contrario, vaya a la carpeta Api de cada proyecto y ejecute los siguientes comandos:

`dotnet restore`
`dotnet build `
`dotnet run`

El proyecto de ProductCatalog corre en el puerto 50001, mientras que el de OrderManagement corre en el puerto 50002. Los proyectos contienen un archivo llamado launchSettings.json, en el cual está realizada esta configuración. De no correr en estos puertos, cambie la configuración de ejecución para que el proyecto utilice este archivo por defecto. Es importante que ambos proyectos corran en estos puertos, ya que Nginx usa estas direcciones para hacer la configuración.

Una vez hecho esto, podrá usar la colección de Postman adjunta en el repositorio para consumir los diferentes endpoints. Importe la colección desde Postman, y sobre la misma de clic derecho y seleccione "View Documentation". Allí podrá ver la forma de uso de los diferentes métodos con sus posibles respuestas.

## Información adicional

Aunque esto no es esencial para que el proyecto se ejecute localmente, los proyectos tienen la configuración necesaria para desplegarse en lambdas normales, en un caso hipotético en el que se usara ECR para el almacenamiento del código. Esto puede verse en la configuración de los Dockerfile de cada proyecto, y el LambdaEntryPoint, que es la configuración que el proyecto usaría para ejecutarse si estuviera desplegado en una lambda de AWS.
