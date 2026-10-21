# instagram-content-tools

Cliente Instagram y bibliotecas auxiliares de contenido y etiquetas.

<!-- BEGIN project-context -->

## Antigüedad y evidencia

**Año de referencia de esta versión: 2021.** Se toma el archivo de código o configuración más reciente con una fecha histórica recuperable; no se ha podido confirmar la fecha exacta de creación.

- Archivo más reciente datable: **2021-04-05**, [`pieces/InstagramLibrary/InstagramMainPage.cs`](pieces/InstagramLibrary/InstagramMainPage.cs); fecha del commit que modificó el archivo, recuperada del Git local.
- Primer registro entre los repositorios Git originales incluidos: **2017-11-02**. Puede corresponder a una importación; no prueba cuándo se escribió por primera vez.
- Dependencia fechada más reciente entre las comprobadas: [Xabe.FFmpeg 4.1.0](https://www.nuget.org/packages/Xabe.FFmpeg/4.1.0), publicada el **2020-06-06** (versión declarada), en [`pieces/TagsGeneratorLibrary.Test/InstagramToolsLibrary.Test.csproj`](pieces/TagsGeneratorLibrary.Test/InstagramToolsLibrary.Test.csproj).
- Referencia adicional: destino `netcoreapp3.1` en [`InstaClient/InstaClient/ClientTest/ClientTest.csproj`](InstaClient/InstaClient/ClientTest/ClientTest.csproj); su [versión estable se publicó el 2019-12-03](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core). Es una pista de generación tecnológica; pudieron existir versiones preliminares anteriores.

La publicación de una dependencia sitúa la configuración conservada a partir de esa versión, si la referencia es válida. Una actualización de paquetes puede ser mucho más reciente que el inicio del proyecto. No se restauraron paquetes para comprobarlo.

Se descartaron las marcas de tiempo de la copia de octubre de 2026 y los documentos generados durante esta revisión. Las fechas recuperables no garantizan que no existan cambios posteriores sin datar.

Años de referencia por componente (archivo más reciente datable):

| Componente | Año |
| --- | --- |
| [`InstaClient`](InstaClient/) | 2020 |
| [`pieces/InstagramLibrary`](pieces/InstagramLibrary/) | 2021 |
| [`pieces/InstagramLibrary.Test`](pieces/InstagramLibrary.Test/) | 2020 |
| [`pieces/TagsGeneratorLibrary`](pieces/TagsGeneratorLibrary/) | 2021 |
| [`pieces/TagsGeneratorLibrary.Test`](pieces/TagsGeneratorLibrary.Test/) | 2021 |

## Por qué merece la pena mirarlo

Destaca la selección de contenido por engagement dentro de bloques temporales, unida a descarga y automatización de publicación. Es un flujo de trabajo concreto que conecta una heurística de selección con tareas de navegador.

Puntos de entrada al código:

- [`pieces/TagsGeneratorLibrary/ImageFilter.cs`](pieces/TagsGeneratorLibrary/ImageFilter.cs)
- [`pieces/TagsGeneratorLibrary/ContentUploader.cs`](pieces/TagsGeneratorLibrary/ContentUploader.cs)
- [`pieces/InstagramLibrary/InstagramMainPage.cs`](pieces/InstagramLibrary/InstagramMainPage.cs)

<!-- END project-context -->

## Contenido

| Directorio | Función |
| --- | --- |
| [`InstaClient/`](InstaClient/) | Cliente experimental C# para operaciones web de Instagram mediante RestSharp, cookies y CSRF: inicio de sesión, consulta de usuarios/fotos, seguidores y acciones de seguir, dejar de seguir y dar me gusta. |
| [`pieces/InstagramLibrary/`](pieces/InstagramLibrary/) | Código fuente |
| [`pieces/InstagramLibrary.Test/`](pieces/InstagramLibrary.Test/) | Código fuente |
| [`pieces/TagsGeneratorLibrary/`](pieces/TagsGeneratorLibrary/) | Código fuente |
| [`pieces/TagsGeneratorLibrary.Test/`](pieces/TagsGeneratorLibrary.Test/) | Código fuente |



## Requisitos y preparación

.NET Core, InstagramApiSharp y NuGet.

Esta es una copia histórica. Consultar los archivos de proyecto y los README de cada componente para sus versiones y dependencias. Restaurar NuGet/npm desde la carpeta correspondiente; las bibliotecas internas pueden necesitar reconstrucción. Preparar las bases de datos y los servicios externos con cuentas propias.

Soluciones conservadas:

- `InstaClient/InstaClient/InstaClient.sln`

## Configuración y seguridad

Los secretos originales se han eliminado o sustituido por `REPLACE_WITH_YOUR_VALUE`. Buscar este marcador antes de ejecutar y aportar los valores mediante configuración local o variables de entorno. Las sesiones, claves privadas, certificados de firma, bases de datos, registros, perfiles de despliegue y paquetes binarios no se publican. El `.gitignore` protege estos archivos locales.

Se publica un historial nuevo: los commits antiguos permanecen únicamente en la copia local original. Los valores de ejemplo no permiten conectarse a los servicios originales.

## Estado de validación

Se realizó una revisión estática y un escaneo de secretos antes de publicar. No se ejecutaron aplicaciones, operaciones de trading, despliegues ni pruebas que contacten servicios externos. No se garantiza la compilación con herramientas actuales. Algunos proyectos necesitan dependencias históricas y activos que no están incluidos.

## Licencias y procedencia

Se conservan las licencias y atribuciones de terceros presentes en el código. No se añade una licencia global a los componentes que no la tenían. Esta importación conserva fuentes locales y no afirma ser una versión actual del proyecto original.
