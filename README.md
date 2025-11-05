KamiYomu Crawler Agent Core – Create Crawler Agents for KamiYomu
====================================================

A foundational library for building, extending, and integrating custom crawler agents within the KamiYomu ecosystem. It provides essential abstractions, utilities, and lifecycle hooks to streamline agent development and ensure seamless interoperability across the platform.

Features
--------

- Agent lifecycle hooks for crawling and metadata extraction
- Integration-ready with the KamiYomu runtime
- Built-in support for HtmlAgilityPack and PuppeteerSharp
- Compatible with .NET Standard 2.0 for broad platform support

Getting Started
---------------

### Prerequisites

- .NET SDK 8.0
- Git (for cloning the repository)

### How to Create and Deploy a Custom KamiYomu Crawler Agent
 
1. Create a Class Library Project
2. Start by creating a new Class Library targeting .NET Standard 2.0 to ensure compatibility with the KamiYomu ecosystem.
3. Add a Nuget.Config file to the directory of your solution (.sln file) with the following content to include the KamiYomu NuGet feed:
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
    <add key="KamiYomu-github" value="https://nuget.pkg.github.com/KamiYomu/index.json" />
  </packageSources>
</configuration>
```
3. Add the `KamiYomu.CrawlerAgents.Core` package from the configured feed.
4. Implement the agent
- Create a class that implements the `ICrawlerAgent` interface from the `KamiYomu.CrawlerAgents.Core` namespace. This interface defines the lifecycle and behavior required by KamiYomu.
5. Validate locally
- Use the `KamiYomu.CrawlerAgents.Validator` repository to validate your implementation. Import it into your solution and run the validation tests to ensure compliance with KamiYomu standards:
  - https://github.com/KamiYomu/KamiYomu.CrawlerAgents.Validator
6.Package (optional)
-To create a distributable NuGet package, use `dotnet pack` orenable package generation on build in your `.csproj`.
7. Publish (optional)
- Publish your package to a feed accessible by KamiYomu.Web (GitHub Packages, a private feed,or a local folder).
8. Register the feedin KamiYomu.Web
-In KamiYomu.Web: Agent Crawler → Install fromOther Sources → Setup Sources — add your feed URL so agents can be discovered and installed.

Tip — local testing
--------------------
- You do not need to publish to a public feed to test. Upload the generated `.nupkg` directly into KamiYomu.Web.
- To produce a package automatically for Debug builds, add the following to your `.csproj`:
```
	<PropertyGroup Condition="'$(Configuration)' == 'Debug'">
		<GeneratePackageOnBuild>True</GeneratePackageOnBuild>
	</PropertyGroup>
```
You can import this package directly into KamiYomu.Web for testing without publishing it to any feed, and also you can debug it if you import the `.pdb` file in the `/AppData/agents/{your.package}/lib/netstandard2.0/` folder.`

### Clone the Repository

```
git clone https://github.com/KamiYomu/KamiYomu.CrawlerAgents.Core.git 
cd KamiYomu.CrawlerAgents.Core
```

### Build the Project
```
dotnet build -c Release
```


### Generate NuGet Package (Debug mode only)
```
dotnet build -c Debug
```


> The package will be generated automatically in `bin/Debug` due to `GeneratePackageOnBuild=True`.

Dependencies
------------

| Package         | Version |
|-----------------|---------|
| HtmlAgilityPack | 1.12.4  |
| PuppeteerSharp  | 20.2.4  |

License
-------

This project is licensed under the GNU General Public License v3.0 (GPL-3.0).

© KamiYomu. Licensed under GPL-3.0.