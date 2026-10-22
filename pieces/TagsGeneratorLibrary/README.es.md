**Languages:** [English](Readme.md) · [Español](README.es.md)

# TagsGeneratorLibrary

<!-- BEGIN project-context -->

## Antigüedad y evidencia

**Año de referencia de esta versión: 2021.** Se toma el archivo de código o configuración más reciente con una fecha histórica recuperable; no se ha podido confirmar la fecha exacta de creación.

- Archivo más reciente datable: **2021-04-05**, [`pieces/TagsGeneratorLibrary/InstagramItemContentData.cs`](InstagramItemContentData.cs); fecha del commit que modificó el archivo, recuperada del Git local.
- Primer registro del repositorio Git original que contiene este código: **2020-05-10**. Puede corresponder a una importación; no prueba cuándo se escribió por primera vez.
- Dependencia fechada más reciente entre las comprobadas: [System.Drawing.Common 4.7.0](https://www.nuget.org/packages/System.Drawing.Common/4.7.0), publicada el **2019-12-03** (versión declarada), en [`pieces/TagsGeneratorLibrary/InstagramToolsLibrary.csproj`](InstagramToolsLibrary.csproj).
- Referencia adicional: destino `netcoreapp3.1` en [`pieces/TagsGeneratorLibrary/InstagramToolsLibrary.csproj`](InstagramToolsLibrary.csproj); su [versión estable se publicó el 2019-12-03](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core). Es una pista de generación tecnológica; pudieron existir versiones preliminares anteriores.

La publicación de una dependencia sitúa la configuración conservada a partir de esa versión, si la referencia es válida. Una actualización de paquetes puede ser mucho más reciente que el inicio del proyecto. No se restauraron paquetes para comprobarlo.

Se descartaron las marcas de tiempo de la copia de octubre de 2026 y los documentos generados durante esta revisión. Las fechas recuperables no garantizan que no existan cambios posteriores sin datar.

## Por qué merece la pena mirarlo

ImageFilter selecciona los mejores posts dentro de grupos ordenados por fecha y compara su engagement con el promedio del grupo. Esa normalización temporal, junto con las herramientas de descarga y publicación, es más interesante que una simple lista de hashtags.

Puntos de entrada al código:

- [`ImageFilter.cs`](ImageFilter.cs)
- [`ContentFilter.cs`](ContentFilter.cs)
- [`Generator.cs`](Generator.cs)

<!-- END project-context -->

