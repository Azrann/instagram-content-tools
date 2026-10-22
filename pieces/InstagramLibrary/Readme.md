**Languages:** [English](Readme.md) · [Español](README.es.md)

# InstagramLibrary

<!-- BEGIN project-context -->

## Historical dates and evidence

**Reference year for this version: 2021.** This uses the most recent source or configuration file with a recoverable historical date; the exact creation date could not be confirmed.

- Most recent datable file: **2021-04-05**, [`pieces/InstagramLibrary/InstagramMainPage.cs`](InstagramMainPage.cs); date of the commit modifying the file, recovered from local Git history.
- First record in the original Git repository containing this code: **2020-05-10**. This may be an import and does not establish when the code was first written.
- Most recently published dependency among those checked: [PuppeteerSharp.Contrib.Extensions 2.0.0](https://www.nuget.org/packages/PuppeteerSharp.Contrib.Extensions/2.0.0), published on **2020-05-08** (declared version), in [`pieces/InstagramLibrary/InstagramLibrary.csproj`](InstagramLibrary.csproj).
- Additional reference: target `netcoreapp3.1` in [`pieces/InstagramLibrary/InstagramLibrary.csproj`](InstagramLibrary.csproj); its [stable release was published on 2019-12-03](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core). This indicates the technology generation; earlier preview releases may have existed.

A dependency's publication places the preserved configuration at or after that version, if the reference is valid. Package updates may be much newer than the project's beginnings. Packages were not restored to verify this.

October 2026 copy timestamps and documents generated during this review were excluded. Recoverable dates do not guarantee that there are no later, undated changes.

## Why this is worth exploring

InstagramMainPage separates automation steps such as login, upload, content entry, and publication scheduling. It is useful to study this decomposition of browser interaction into higher-level operations.

Code entry points:

- [`InstagramMainPage.cs`](InstagramMainPage.cs)
- [`PuppeteerExtensions.cs`](PuppeteerExtensions.cs)

<!-- END project-context -->

