**Languages:** [English](README.md) · [Español](README.es.md)

# InstaClient

Experimental C# client for Instagram web operations using RestSharp, cookies, and CSRF: login, user/photo queries, followers, and follow, unfollow, and like actions.

Documentation based on local files reviewed on 21 October 2026.

<!-- BEGIN project-context -->

## Historical dates and evidence

**Reference year for this version: 2020.** This uses the most recent source or configuration file with a recoverable historical date; the exact creation date could not be confirmed.

- Most recent datable file: **2020-05-12**, [`InstaClient/InstaClient/ClientTest/Program.cs`](InstaClient/ClientTest/Program.cs); preserved modification timestamp.
- First record in the original Git repository containing this code: **2017-11-02**. This may be an import and does not establish when the code was first written.
- Most recently published dependency among those checked: [RestSharp 105.2.3](https://www.nuget.org/packages/RestSharp/105.2.3), published on **2015-08-26** (declared version), in [`InstaClient/InstaClient/ClientTest/ClientTest.csproj`](InstaClient/ClientTest/ClientTest.csproj).
- Additional reference: target `netcoreapp3.1` in [`InstaClient/InstaClient/ClientTest/ClientTest.csproj`](InstaClient/ClientTest/ClientTest.csproj); its [stable release was published on 2019-12-03](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core). This indicates the technology generation; earlier preview releases may have existed.

A dependency's publication places the preserved configuration at or after that version, if the reference is valid. Package updates may be much newer than the project's beginnings. Packages were not restored to verify this.

## Why this is worth exploring

The client models web operations using RestSharp, cookies, and CSRF. It is useful for studying HTTP session encapsulation and conversion of responses into models, while remembering that it targets historical endpoints.

Code entry points:

- [`InstaClient/InstaClient/InstaRestClient.cs`](InstaClient/InstaClient/InstaRestClient.cs)
- [`InstaClient/InstaClient/Followers.cs`](InstaClient/InstaClient/Followers.cs)

<!-- END project-context -->

## Structure and responsibilities

| Path | Responsibility |
| --- | --- |
| `InstaClient/InstaClient/InstaRestClient.cs` | HTTP client and Instagram operations. |
| `InstaClient/InstaClient/` | User, follower, photo, location, and response models. |
| `InstaClient/ClientTest/` | Client test program. |
| `InstaClient/InstaClient.sln` | Visual Studio solution. |

## Technologies and requirements

RestSharp 105.2.3. The library targets .NET Framework 4.5.2, while ClientTest targets .NET Core 3.1: review project and reference compatibility before running them.

Targets read from project files: `netcoreapp3.1`, `v4.5.2`.

## Setup and startup

Open InstaClient/InstaClient.sln, restore NuGet packages, and review the test program. Provide your own accounts and configuration only in a test environment; no logins or account actions were performed.

Solutions present: `InstaClient/InstaClient.sln`.

## Configuration and integrations

The constructor sets headers and the base URL. Credentials are passed to LogIn, and the client keeps cookies/CSRF in memory. Review proxy settings and any persistence before reuse.

## Status of this copy

Uses historical Instagram web endpoints; local analysis does not verify current compatibility. The previous README is retained.

The review was static: dependencies were not restored, and applications and tests were neither built nor run. The commands above are setup instructions, not validation results. Listed versions describe the preserved code.


## Public copy

Original credentials have been removed or replaced with `REPLACE_WITH_YOUR_VALUE`. Local data, sessions, private certificates, and deployment profiles are omitted. Configure your own services and credentials before running. Building and operation with external services have not been validated.
