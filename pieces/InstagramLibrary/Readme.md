# InstagramLibrary

<!-- BEGIN project-context -->

## Antigüedad y evidencia

**Año de referencia de esta versión: 2021.** Se toma el archivo de código o configuración más reciente con una fecha histórica recuperable; no se ha podido confirmar la fecha exacta de creación.

- Archivo más reciente datable: **2021-04-05**, [`pieces/InstagramLibrary/InstagramMainPage.cs`](InstagramMainPage.cs); fecha del commit que modificó el archivo, recuperada del Git local.
- Primer registro del repositorio Git original que contiene este código: **2020-05-10**. Puede corresponder a una importación; no prueba cuándo se escribió por primera vez.
- Dependencia fechada más reciente entre las comprobadas: [PuppeteerSharp.Contrib.Extensions 2.0.0](https://www.nuget.org/packages/PuppeteerSharp.Contrib.Extensions/2.0.0), publicada el **2020-05-08** (versión declarada), en [`pieces/InstagramLibrary/InstagramLibrary.csproj`](InstagramLibrary.csproj).
- Referencia adicional: destino `netcoreapp3.1` en [`pieces/InstagramLibrary/InstagramLibrary.csproj`](InstagramLibrary.csproj); su [versión estable se publicó el 2019-12-03](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core). Es una pista de generación tecnológica; pudieron existir versiones preliminares anteriores.

La publicación de una dependencia sitúa la configuración conservada a partir de esa versión, si la referencia es válida. Una actualización de paquetes puede ser mucho más reciente que el inicio del proyecto. No se restauraron paquetes para comprobarlo.

Se descartaron las marcas de tiempo de la copia de octubre de 2026 y los documentos generados durante esta revisión. Las fechas recuperables no garantizan que no existan cambios posteriores sin datar.

## Por qué merece la pena mirarlo

InstagramMainPage separa pasos de automatización como login, subida, contenido y programación de publicaciones. Es interesante estudiar esa descomposición de una interacción de navegador en operaciones de mayor nivel.

Puntos de entrada al código:

- [`InstagramMainPage.cs`](InstagramMainPage.cs)
- [`PuppeteerExtensions.cs`](PuppeteerExtensions.cs)

<!-- END project-context -->

