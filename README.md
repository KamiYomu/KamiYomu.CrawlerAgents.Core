# KamiYomu Crawler Agent Core — Create Crawler Agents for KamiYomu

A small foundational library and set of conventions for building, extending, and integrating custom crawler agents within the KamiYomu ecosystem. It provides lifecycle abstractions, utilities, and optional integrations for scraping HTML and controlling headless browsers.

## Key Features

- 🔄 Crawler Agent lifecycle hooks for crawling and metadata extraction
- 🌐 **Smart HTTP Client** with automatic fallback chain (HTTP → FlareSolverr → PuppeteerSharp)
- 📡 **Enriched Headers & Auto-Retry** - Built-in headers to prevent failures and configurable timeouts
- 🔌 Integration-ready with the KamiYomu runtime
- 🛠️ Built-in helpers for `HtmlAgilityPack` and `PuppeteerSharp`
- 🎯 Compatible with `.NET 8.0` for maximum host compatibility

## Getting Started

Choose one of the two approaches below:

### Option 1: Use the Template (Recommended) ⭐

The fastest way to get started is to use the official template repository, which includes:
- Pre-configured project structure
- Sample implementations
- Built-in validator tool (`KamiYomu.CrawlerAgents.ConsoleApp`)
- Best practices already applied

**Steps:**

1. Clone the template repository:

	```bash
	git clone https://github.com/KamiYomu/KamiYomu.CrawlerAgents.Sample.git
	cd KamiYomu.CrawlerAgents.Sample
	```

2. Follow the instructions in the template's `README.md` to customize it for your source

3. Use the included validator project to ensure compliance:

	```bash
	cd src/KamiYomu.CrawlerAgents.ConsoleApp
	dotnet run
	```

4. Renaming the project and namespaces to match your source is recommended for clarity. Using the template name as `[DeveloperName].CrawlerAgents.[SourceName]`

5. Once validated, proceed to [Packaging and Publishing](#packaging-and-publishing)

### Option 2: Create from Scratch

If you prefer to build your crawler agent from scratch, follow this step-by-step tutorial:

#### Step 1: Create the Project

Create a new class library project targeting `.NET 8.0`:

```bash
dotnet new classlib -n [YourName].CrawlerAgents.[SourceName] -f net8.0
cd [YourName].CrawlerAgents.[SourceName]
```

Replace:
- `[YourName]` with your name or organization (e.g., `MyCompany`)
- `[SourceName]` with the manga source name (e.g., `MangaHub`)

#### Step 2: Configure NuGet Package Source

Create a `NuGet.Config` file in your solution root (next to the `.sln` file):

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
	<clear />
	<add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
  </packageSources>
</configuration>
```

#### Step 3: Install the Core Package

Add the KamiYomu Crawler Agents Core package:

```bash
dotnet add package KamiYomu.CrawlerAgents.Core
```

#### Step 4: Create Your Crawler Agent Class

Create a new class file (e.g., `MyMangaCrawlerAgent.cs`) that implements the required interfaces:

```csharp
using KamiYomu.CrawlerAgents.Core;
using KamiYomu.CrawlerAgents.Core.Catalog;
using KamiYomu.CrawlerAgents.Core.Inputs;

namespace YourName.CrawlerAgents.SourceName
{
	public class MyMangaCrawlerAgent : AbstractCrawlerAgent, IDefaultHeadersCrawlerAgent
	{
		public string AgentName => "Your Manga Source";
		public string SourceUrl => "https://www.yourmangasource.com";

		public IEnumerable<KeyValuePair<string, string>> GetDefaultHeaders()
		{
			return new Dictionary<string, string>
			{
				{ "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36" }
			};
		}

		public override Task<Uri> GetFaviconAsync(CancellationToken cancellationToken)
		{
			// Return the favicon URL of your manga source
			return Task.FromResult(new Uri("https://www.yourmangasource.com/favicon.ico"));
		}

		public override async Task<PagedResult<Manga>> SearchAsync(
			string titleName, 
			PaginationOptions paginationOptions, 
			CancellationToken cancellationToken)
		{
			// Use DefaultHttpClient for HTTP requests
			var url = $"https://www.yourmangasource.com/search?q={Uri.EscapeDataString(titleName)}";
			var response = await DefaultHttpClient.GetAsync(url, cancellationToken);
			// Parse response and return results
			throw new NotImplementedException();
		}

		public override async Task<Manga> GetByIdAsync(string id, CancellationToken cancellationToken)
		{
			// Use DefaultHttpClient to retrieve manga by ID
			var url = $"https://www.yourmangasource.com/manga/{id}";
			var response = await DefaultHttpClient.GetAsync(url, cancellationToken);
			// Parse response and return manga
			throw new NotImplementedException();
		}

		public override async Task<PagedResult<Chapter>> GetChaptersAsync(
			Manga manga, 
			PaginationOptions paginationOptions, 
			CancellationToken cancellationToken)
		{
			// Use DefaultHttpClient to fetch chapters
			var url = $"https://www.yourmangasource.com/manga/{manga.Id}/chapters";
			var response = await DefaultHttpClient.GetAsync(url, cancellationToken);
			// Parse response and return chapters
			throw new NotImplementedException();
		}

		public override async Task<IEnumerable<Page>> GetChapterPagesAsync(
			Chapter chapter, 
			CancellationToken cancellationToken)
		{
			// Use DefaultHttpClient to fetch pages
			var url = $"https://www.yourmangasource.com/chapter/{chapter.Id}";
			var response = await DefaultHttpClient.GetAsync(url, cancellationToken);
			// Parse response and return pages
			throw new NotImplementedException();
		}
	}
}
```

#### Step 5: Configure Your Project

Update your `.csproj` file to include package metadata:

```xml
<PropertyGroup>
  <TargetFramework>net8.0</TargetFramework>
  <PackageTags>kamiyomu;kamiyomu-crawler-agents;manga-download;[SourceName]</PackageTags>
</PropertyGroup>
```

**For NSFW content sources**, add the `nsfw` tag:

```xml
<PropertyGroup>
  <TargetFramework>net8.0</TargetFramework>
  <PackageTags>kamiyomu;kamiyomu-crawler-agents;manga-download;[SourceName];nsfw</PackageTags>
</PropertyGroup>
```

#### Step 6: Implement the Methods

Now implement each method based on your manga source's API or website structure:

**Available Helper Classes:**
- `PageBuilder` - Build page objects
- `ChapterBuilder` - Build chapter objects
- `MangaBuilder` - Build manga objects
- `PagedResultBuilder` - Build paginated results
- `HttpClientExtensions` - HTTP client utilities

#### Step 7: Test Locally

Build and test your agent:

```bash
dotnet build
dotnet run
```

## Interface Reference

### HTTP Client & Request Handling

The KamiYomu Crawler Agents Core provides a **robust default HTTP client** that handles complex scraping scenarios automatically. You don't need to worry about implementing custom HTTP logic—the framework takes care of it.

#### Smart Fallback Chain

The HTTP client uses an intelligent fallback mechanism to ensure requests succeed:

1. **Standard HTTP Client** (First attempt)
   - Fast, lightweight requests for most websites
   - Includes enriched headers (User-Agent, Accept, etc.)
   - Respects configured timeout and retry policies

2. **FlareSolverr Fallback** (If standard HTTP fails)
   - Automatically handles Cloudflare protection and similar challenges
   - Transparently processes JavaScript-protected content
   - No configuration needed—it just works

3. **PuppeteerSharp Browser Fallback** (If both above fail)
   - Full headless browser support for complex dynamic content
   - Handles JavaScript rendering, cookies, and sessions
   - Used as a last resort for maximum compatibility

#### Built-in Features

- **Enriched Headers**: Automatically includes realistic browser headers to avoid common blocking patterns
- **Timeout Configuration**: Configurable request timeouts with sensible defaults
- **Automatic Retries**: Retry logic built-in to handle transient failures
- **Default Headers Integration**: Your `IDefaultHeadersCrawlerAgent.GetDefaultHeaders()` are automatically merged with framework headers

#### Usage Example

Simply use the `DefaultHttpClient` property provided by the framework—no special configuration needed:

```csharp
public override async Task<PagedResult<Manga>> SearchAsync(
	string titleName, 
	PaginationOptions paginationOptions, 
	CancellationToken cancellationToken)
{
	var url = $"https://yourmangasource.com/search?q={Uri.EscapeDataString(titleName)}";

	// Use DefaultHttpClient which automatically handles:
	// - Your custom headers (from GetDefaultHeaders())
	// - Enriched browser-like headers
	// - Fallback to FlareSolverr if needed
	// - Fallback to PuppeteerSharp if FlareSolverr fails
	// - Timeout management and automatic retries
	var response = await DefaultHttpClient.GetAsync(url, cancellationToken);
	var html = await response.Content.ReadAsStringAsync(cancellationToken);

	// Parse and return results
	return ParseResults(html);
}
```

### ICrawlerAgent

The core interface that all crawler agents must implement. Required methods:

- **`GetFaviconAsync(CancellationToken)`** → `Task<Uri>`
  - Retrieve the favicon URI for your manga source

- **`SearchAsync(titleName, paginationOptions, CancellationToken)`** → `Task<PagedResult<Manga>>`
  - Search for manga by title, supporting pagination or continuation tokens

- **`GetByIdAsync(id, CancellationToken)`** → `Task<Manga>`
  - Retrieve detailed manga information by unique identifier

- **`GetChaptersAsync(manga, paginationOptions, CancellationToken)`** → `Task<PagedResult<Chapter>>`
  - Retrieve paginated chapters for a specific manga

- **`GetChapterPagesAsync(chapter, CancellationToken)`** → `Task<IEnumerable<Page>>`
  - Retrieve all pages in a chapter

### IDefaultHeadersCrawlerAgent

Provides HTTP headers for requests to your manga source:

- **`GetDefaultHeaders()`** → `IEnumerable<KeyValuePair<string, string>>`
  - Return default HTTP headers (User-Agent, Authorization, etc.)
  - These headers are used for all requests and file downloads

## Validation and Testing

After implementing your crawler agent, validate it with the test project:

1. Clone the validator from the template repository (if you created from scratch):

	```bash
	git clone https://github.com/KamiYomu/KamiYomu.CrawlerAgents.Sample.git
	cd KamiYomu.CrawlerAgents.Sample/src/KamiYomu.CrawlerAgents.ConsoleApp
	```

2. Configure it to reference your agent package

3. Run the validation:

	```bash
	dotnet run
	```

The validator will confirm that your agent meets KamiYomu's requirements before publishing.

## Packaging and Publishing

### Build a Distributable Package

Create a release build:

```bash
dotnet pack -c Release
```

This generates a `.nupkg` file in your `bin/Release` folder.

### Enable Automatic Package Generation for Debug Builds

Optionally, add this to your `.csproj` to auto-generate packages during debug builds:

```xml
<PropertyGroup Condition="'$(Configuration)' == 'Debug'">
  <GeneratePackageOnBuild>True</GeneratePackageOnBuild>
</PropertyGroup>
```

### Publish Your Package

Choose one of the following options:

1. **NuGet.org** (Public)
   - Register at https://www.nuget.org
   - Push your package: `dotnet nuget push [DeveloperName].CrawlerAgents.[SourceName].nupkg --api-key [YOUR_API_KEY] --source https://api.nuget.org/v3/index.json`

2. **GitHub Packages**
   - Configure GitHub Actions to auto-publish
   - Set up authentication in your workflow

3. **Azure Artifacts**
   - Create a private feed
   - Configure your NuGet.Config for authentication

4. **Local Testing** (Without Publishing)
   - Place the `.nupkg` directly in KamiYomu.Web's agent folder
   - Or upload via the KamiYomu.Web UI

### Packaging Checklist

- ✅ Ensure your package includes all necessary runtime assets
- ✅ Include all dependencies in the package
- ✅ Keep the public API minimal and well-documented
- ✅ Document any required configuration or permissions
- ✅ Version your package following semantic versioning

## Debugging

### Debug an Installed Agent

To debug your agent while running in KamiYomu.Web:

1. Build your project in Debug mode
2. Locate the generated `.pdb` file
3. Place it alongside the agent DLL in the agent folder:
   ```
   /AppData/agents/{[DeveloperName].CrawlerAgents.[SourceName]}/lib/net8.0/
   ```
4. KamiYomu.Web will now use source-level debugging

## Configuration Options

### Fetch Data via DefaultHttpClient
The `DefaultHttpClient` is provided by the core library and can be used for making HTTP requests with default headers and proper configuration.
KamiYomu will provide a pre-configured `HttpClient` instance to your agent, which you can use for all requests.
This DefaultHttpClient will be pre configured to use the cloudflare bypass and other necessary headers for scraping.
and Also using a request by browser using the puppeteer sharp library for scraping the data from the website.


### Using HTML Scraping (HtmlAgilityPack)

The library includes helpers for `HtmlAgilityPack`:

```csharp
using HtmlAgilityPack;

public override async Task<PagedResult<Manga>> SearchAsync(
	string titleName, 
	PaginationOptions paginationOptions, 
	CancellationToken cancellationToken)
{
	var doc = new HtmlDocument();
	// Load and parse HTML...

	// Build results with MangaBuilder
	var manga = new MangaBuilder()
		.WithId("123")
		.WithTitle("Example Manga")
		.Build();

	return new PagedResultBuilder<Manga>()
		.WithItems(new[] { manga })
		.Build();
}
```

### Using Headless Browser (PuppeteerSharp)

For JavaScript-heavy websites:

```csharp
using PuppeteerSharp;

public override async Task<IEnumerable<Page>> GetChapterPagesAsync(
	Chapter chapter, 
	CancellationToken cancellationToken)
{
	await new BrowserFetcher().DownloadAsync();
	var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
	// Navigate and extract pages...

	return pages;
}
```

## API Reference

### Data Models

- **`Manga`** - Represents a manga title
- **`Chapter`** - Represents a chapter of a manga
- **`Page`** - Represents a page/image in a chapter
- **`PagedResult<T>`** - Pagination wrapper for search and listing results

### Builder Classes

Use builders to simplify object creation:

- **`MangaBuilder`** - Create manga objects fluently
- **`ChapterBuilder`** - Create chapter objects fluently
- **`PageBuilder`** - Create page objects fluently
- **`PagedResultBuilder<T>`** - Create paginated results fluently

### Utilities

- **`HttpClientExtensions`** - Helper methods for HTTP requests with proper headers

## Quick Reference Commands

```bash
# Create new project
dotnet new classlib -n [DeveloperName].CrawlerAgents.[SourceName] -f net8.0

# Add core package
dotnet add package KamiYomu.CrawlerAgents.Core

# Build project
dotnet build

# Build release package
dotnet pack -c Release

# Publish to NuGet (after registering)
dotnet nuget push bin/Release/[DeveloperName].CrawlerAgents.[SourceName].nupkg --api-key [KEY] --source https://api.nuget.org/v3/index.json
```

## Dependencies

| Package         | Version | Purpose                              |
|-----------------|---------|--------------------------------------|
| HtmlAgilityPack | 1.12.4  | HTML parsing and scraping            |
| PuppeteerSharp  | 20.2.4  | Headless browser automation          |

These are optional - only add them if your agent needs their functionality.

## Troubleshooting

### My agent isn't being discovered by KamiYomu.Web

- ✅ Verify the package name follows: `*.CrawlerAgents.*`
- ✅ Confirm `PackageTags` include `kamiyomu;kamiyomu-crawler-agents;`
- ✅ Check that your class implements both `ICrawlerAgent` and `IDefaultHeadersCrawlerAgent`
- ✅ Ensure the package is installed in KamiYomu.Web's agent folder

### HTTP requests are being blocked

- ✅ Check `GetDefaultHeaders()` returns proper User-Agent
- ✅ Add authorization headers if the source requires authentication
- ✅ Respect rate limiting and delays between requests

### Validation tests are failing

- ✅ Run the validator project from the template repository
- ✅ Fix any compliance issues reported
- ✅ Ensure all interface methods are properly implemented

## Contributing

Contributions are welcome! Please:

1. Follow the existing code style and conventions
2. Include unit tests for new functionality
3. Use the validator project to confirm compliance before submitting
4. Submit clear pull requests with reproducible examples
5. Update documentation if adding new features

## Resources

- **Template Repository**: https://github.com/KamiYomu/KamiYomu.CrawlerAgents.Sample
- **Core Library**: https://github.com/KamiYomu/KamiYomu.CrawlerAgents.Core
- **Issues & Support**: Open an issue on the repository for questions or bugs
- **KamiYomu Main Project**: https://github.com/KamiYomu/KamiYomu

## Community

Join the conversation and be part of the KamiYomu community:

| Action | Link |
| :--- | :--- |
| **Following** | [![GitHub followers](https://img.shields.io/github/followers/kamiyomu)](https://github.com/orgs/KamiYomu/followers) |
| **Discord** | [![Join the discord](https://img.shields.io/discord/1468597233032101942)](https://discord.gg/b9zwEEejsJ) |
| **Sponsor** | [![GitHub Sponsors](https://img.shields.io/github/sponsors/kamiyomu?logo=github&label=Sponsor)](https://github.com/sponsors/kamiyomu) |
| **Report** | [![GitHub issues](https://img.shields.io/github/issues/kamiyomu/KamiYomu.CrawlerAgents.Core?logo=github&label=Issues)](https://github.com/kamiyomu/KamiYomu.CrawlerAgents.Core/issues) |
| **Contribute** | [![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?logo=github)](https://github.com/KamiYomu/KamiYomu.CrawlerAgents.Core/pulls) |


## License

This project is licensed under the **MIT License**. 

See the `LICENSE` file for full terms.

## Copyright

© KamiYomu. Licensed under AGPL-3.0.

---

**Ready to build?** Start with [Option 1 (Template)](#option-1-use-the-template-recommended-) for the quickest path, or [Option 2 (From Scratch)](#option-2-create-from-scratch) if you prefer learning the full structure!