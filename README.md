KamiYomu Crawler Agent Core — Create Crawler Agents for KamiYomu
================================================================

A small foundational library and set of conventions for building, extending, and integrating custom crawler agents within the KamiYomu ecosystem. It provides lifecycle abstractions, utilities, and optional integrations for scraping HTML and controlling headless browsers.

Key features
------------
- Agent lifecycle hooks for crawling and metadata extraction.
- Integration-ready with the KamiYomu runtime.
- Built-in examples and helpers for `HtmlAgilityPack` and `PuppeteerSharp`.
- Compatible with `.net-8.0` to maximize host compatibility (works from modern .NET SDKs, including .NET 8).

Quick start
-----------
1. Create a class library project (target `net-8.0` for widest compatibility):
    
    Create project:
    
        dotnet new classlib -n MyKamiYomuAgent -f net-8.0

2. Add NuGet.Config in the solution folder to ensure standard feeds:

    NuGet.Config content (place next to your `.sln`):
    
        <?xml version="1.0" encoding="utf-8"?>
        <configuration>
          <packageSources>
            <clear />
            <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
          </packageSources>
        </configuration>

3. Install the core package:

        dotnet add package KamiYomu.CrawlerAgents.Core

4. Make your package discoverable by KamiYomu (add `PackageTags` to your `.csproj`):

    Add inside your `.csproj`:

        <PropertyGroup>
		    <PackageTags>crawler-agents;manga-download</PackageTags>
        </PropertyGroup>

5. Implement your agent
    - Create a class that implements `ICrawlerAgent` from the `KamiYomu.CrawlerAgents.Core` namespace.
    - Implement required lifecycle methods (crawl, extract metadata, etc.). The interface defines how KamiYomu will call your agent.

Validate and test locally
-------------------------
- Use the validator repository to confirm your implementation meets KamiYomu requirements:
  - https://github.com/KamiYomu/KamiYomu.CrawlerAgents.Validator

- Local testing: you do not need to publish to a remote feed. You can build a `.nupkg` and upload it into KamiYomu.Web for testing.

Packaging and publishing
------------------------
- Build a distributable package:

        dotnet pack -c Release

- To automatically generate a NuGet package for Debug builds, add to your `.csproj`:

        <PropertyGroup Condition="'$(Configuration)' == 'Debug'">
            <GeneratePackageOnBuild>True</GeneratePackageOnBuild>
        </PropertyGroup>

- Publish to a feed accessible by KamiYomu.Web (Nuget.org, GitHub Packages, Azure Artifacts, private feed, or local folder).
- To test without a feed, upload the generated `.nupkg` directly into KamiYomu.Web.

Debugging an installed agent
----------------------------
- Place the `.pdb` alongside the agent DLL inside the agent folder (e.g. `/AppData/agents/{your.package}/lib/net8.0/`) to enable source-level debugging when running inside KamiYomu.Web.

Packaging notes
---------------
- Ensure your package includes necessary runtime assets and dependencies.
- Keep the public API surface minimal and document required configuration and permissions.

Commands summary
----------------
- Create project:
    
        dotnet new classlib -n MyKamiYomuAgent -f net8.0

- Add package:

        dotnet add package KamiYomu.CrawlerAgents.Core

- Build Release package:

        dotnet pack -c Release

- Enable package on Debug build:

        Add `<GeneratePackageOnBuild>True</GeneratePackageOnBuild>` under Debug condition in `.csproj`

Dependencies
------------
| Package         | Version |
|-----------------|---------|
| HtmlAgilityPack | 1.12.4  |
| PuppeteerSharp  | 20.2.4  |

Contributing
------------
- Follow repository coding conventions and include unit tests for new behavior.
- Use the validator repo above to confirm compliance before publishing.
- Open issues or pull requests against the core repository with clear descriptions and reproducible examples.

License
-------
This project is licensed under the GNU General Public License v3.0 (GPL-3.0). See the `LICENSE` file for full terms.

Support / Contact
-----------------
- Repo: https://github.com/KamiYomu/KamiYomu.CrawlerAgents.Core
- For integration or runtime questions, open an issue on the repository.

Changelog
---------
- See repository Releases for version-specific notes.

Copyright
---------
© KamiYomu. Licensed under GPL-3.0.