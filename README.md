**Languages:** [English](README.md) · [Español](README.es.md)

# instagram-content-tools

Instagram client and supporting content and tag libraries.

<!-- BEGIN project-context -->

## Historical dates and evidence

**Reference year for this version: 2021.** This uses the most recent source or configuration file with a recoverable historical date; the exact creation date could not be confirmed.

- Most recent datable file: **2021-04-05**, [`pieces/InstagramLibrary/InstagramMainPage.cs`](pieces/InstagramLibrary/InstagramMainPage.cs); date of the commit modifying the file, recovered from local Git history.
- Earliest record among the original Git repositories included: **2017-11-02**. This may be an import and does not establish when the code was first written.
- Most recently published dependency among those checked: [Xabe.FFmpeg 4.1.0](https://www.nuget.org/packages/Xabe.FFmpeg/4.1.0), published on **2020-06-06** (declared version), in [`pieces/TagsGeneratorLibrary.Test/InstagramToolsLibrary.Test.csproj`](pieces/TagsGeneratorLibrary.Test/InstagramToolsLibrary.Test.csproj).
- Additional reference: target `netcoreapp3.1` in [`InstaClient/InstaClient/ClientTest/ClientTest.csproj`](InstaClient/InstaClient/ClientTest/ClientTest.csproj); its [stable release was published on 2019-12-03](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core). This indicates the technology generation; earlier preview releases may have existed.

A dependency's publication places the preserved configuration at or after that version, if the reference is valid. Package updates may be much newer than the project's beginnings. Packages were not restored to verify this.

October 2026 copy timestamps and documents generated during this review were excluded. Recoverable dates do not guarantee that there are no later, undated changes.

Reference years by component (most recent datable file):

| Component | Year |
| --- | --- |
| [`InstaClient`](InstaClient/) | 2020 |
| [`pieces/InstagramLibrary`](pieces/InstagramLibrary/) | 2021 |
| [`pieces/InstagramLibrary.Test`](pieces/InstagramLibrary.Test/) | 2020 |
| [`pieces/TagsGeneratorLibrary`](pieces/TagsGeneratorLibrary/) | 2021 |
| [`pieces/TagsGeneratorLibrary.Test`](pieces/TagsGeneratorLibrary.Test/) | 2021 |

## Why this is worth exploring

Content selection by engagement within time-based blocks stands out, combined with downloading and publication automation. This concrete workflow connects a selection heuristic to browser tasks.

Code entry points:

- [`pieces/TagsGeneratorLibrary/ImageFilter.cs`](pieces/TagsGeneratorLibrary/ImageFilter.cs)
- [`pieces/TagsGeneratorLibrary/ContentUploader.cs`](pieces/TagsGeneratorLibrary/ContentUploader.cs)
- [`pieces/InstagramLibrary/InstagramMainPage.cs`](pieces/InstagramLibrary/InstagramMainPage.cs)

<!-- END project-context -->

## Contents

| Directory | Function |
| --- | --- |
| [`InstaClient/`](InstaClient/) | Experimental C# client for Instagram web operations using RestSharp, cookies, and CSRF: login, user/photo queries, followers, and follow, unfollow, and like actions. |
| [`pieces/InstagramLibrary/`](pieces/InstagramLibrary/) | Source code |
| [`pieces/InstagramLibrary.Test/`](pieces/InstagramLibrary.Test/) | Source code |
| [`pieces/TagsGeneratorLibrary/`](pieces/TagsGeneratorLibrary/) | Source code |
| [`pieces/TagsGeneratorLibrary.Test/`](pieces/TagsGeneratorLibrary.Test/) | Source code |



## Requirements and setup

.NET Core, InstagramApiSharp, and NuGet.

This is a historical copy. Consult each component's project files and README for versions and dependencies. Restore NuGet/npm from the appropriate folder; internal libraries may need rebuilding. Prepare databases and external services using your own accounts.

Preserved solutions:

- `InstaClient/InstaClient/InstaClient.sln`

## Configuration and security

Original secrets have been removed or replaced with `REPLACE_WITH_YOUR_VALUE`. Search for this marker before running and provide values through local configuration or environment variables. Sessions, private keys, signing certificates, databases, logs, deployment profiles, and binary packages are not published. The `.gitignore` protects these local files.

A fresh history is published: old commits remain only in the original local copy. Example values cannot connect to the original services.

## Validation status

A static review and secret scan were performed before publication. Applications, trading operations, deployments, and tests contacting external services were not run. Building with current tools is not guaranteed. Some projects need historical dependencies and assets that are not included.

## Licenses and provenance

Third-party licenses and attributions present in the code are retained. No blanket license is added to components that lacked one. This import preserves local sources and does not claim to be a current version of the original project.
