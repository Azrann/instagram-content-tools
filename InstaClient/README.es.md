**Languages:** [English](README.md) · [Español](README.es.md)

# InstaClient

Cliente experimental C# para operaciones web de Instagram mediante RestSharp, cookies y CSRF: inicio de sesión, consulta de usuarios/fotos, seguidores y acciones de seguir, dejar de seguir y dar me gusta.

Documentación basada en los archivos locales revisados el 21 de octubre de 2026.

<!-- BEGIN project-context -->

## Antigüedad y evidencia

**Año de referencia de esta versión: 2020.** Se toma el archivo de código o configuración más reciente con una fecha histórica recuperable; no se ha podido confirmar la fecha exacta de creación.

- Archivo más reciente datable: **2020-05-12**, [`InstaClient/InstaClient/ClientTest/Program.cs`](InstaClient/ClientTest/Program.cs); fecha de modificación conservada.
- Primer registro del repositorio Git original que contiene este código: **2017-11-02**. Puede corresponder a una importación; no prueba cuándo se escribió por primera vez.
- Dependencia fechada más reciente entre las comprobadas: [RestSharp 105.2.3](https://www.nuget.org/packages/RestSharp/105.2.3), publicada el **2015-08-26** (versión declarada), en [`InstaClient/InstaClient/ClientTest/ClientTest.csproj`](InstaClient/ClientTest/ClientTest.csproj).
- Referencia adicional: destino `netcoreapp3.1` en [`InstaClient/InstaClient/ClientTest/ClientTest.csproj`](InstaClient/ClientTest/ClientTest.csproj); su [versión estable se publicó el 2019-12-03](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core). Es una pista de generación tecnológica; pudieron existir versiones preliminares anteriores.

La publicación de una dependencia sitúa la configuración conservada a partir de esa versión, si la referencia es válida. Una actualización de paquetes puede ser mucho más reciente que el inicio del proyecto. No se restauraron paquetes para comprobarlo.

## Por qué merece la pena mirarlo

El cliente modela operaciones web utilizando RestSharp, cookies y CSRF. Es útil para estudiar la encapsulación de una sesión HTTP y la conversión de respuestas en modelos, teniendo presente que corresponde a endpoints históricos.

Puntos de entrada al código:

- [`InstaClient/InstaClient/InstaRestClient.cs`](InstaClient/InstaClient/InstaRestClient.cs)
- [`InstaClient/InstaClient/Followers.cs`](InstaClient/InstaClient/Followers.cs)

<!-- END project-context -->

## Estructura y responsabilidades

| Ruta | Responsabilidad |
| --- | --- |
| `InstaClient/InstaClient/InstaRestClient.cs` | Cliente HTTP y operaciones de Instagram. |
| `InstaClient/InstaClient/` | Modelos de usuarios, seguidores, fotos, localización y respuestas. |
| `InstaClient/ClientTest/` | Programa de prueba del cliente. |
| `InstaClient/InstaClient.sln` | Solución de Visual Studio. |

## Tecnologías y requisitos

RestSharp 105.2.3. La biblioteca apunta a .NET Framework 4.5.2 y ClientTest a .NET Core 3.1: revisar la compatibilidad entre proyectos y referencias antes de ejecutarlos.

Destinos leídos de los proyectos: `netcoreapp3.1`, `v4.5.2`.

## Preparación y arranque

Abrir InstaClient/InstaClient.sln, restaurar NuGet y revisar el programa de prueba. Proporcionar cuentas y configuración propias solo en un entorno de pruebas; no se iniciaron sesiones ni acciones sobre cuentas.

Soluciones presentes: `InstaClient/InstaClient.sln`.

## Configuración e integraciones

El constructor fija encabezados y la URL base. Las credenciales se pasan a LogIn y el cliente mantiene cookies/CSRF en memoria. Revisar proxy y cualquier persistencia antes de reutilizarlo.

## Estado de esta copia

Utiliza endpoints web históricos de Instagram; el análisis local no comprueba su compatibilidad actual. Se conserva el README anterior.

La revisión fue estática: no se restauraron dependencias, no se compiló ni se ejecutaron aplicaciones o pruebas. Los comandos anteriores son instrucciones de preparación, no resultados de validación. Las versiones indicadas son las del código conservado.


## Copia pública

Las credenciales originales se han retirado o sustituido por `REPLACE_WITH_YOUR_VALUE`. Los datos locales, sesiones, certificados privados y perfiles de despliegue se omiten. Configurar servicios y credenciales propios antes de ejecutar. La compilación y el funcionamiento con servicios externos no se han validado.
