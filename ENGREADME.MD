## ReleaseChecker [RU README version](README.md)
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/SkiTles55/ReleaseChecker/publish-ReleaseChecker)
![Nuget](https://img.shields.io/nuget/dt/ReleaseChecker)
![GitHub](https://img.shields.io/github/license/SkiTles55/ReleaseChecker)
![Nuget](https://img.shields.io/nuget/v/ReleaseChecker)

## ReleaseChecker.GitHub
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/SkiTles55/ReleaseChecker/publish-ReleaseChecker-GitHub)
![Nuget](https://img.shields.io/nuget/dt/ReleaseChecker.GitHub)
![GitHub](https://img.shields.io/github/license/SkiTles55/ReleaseChecker)
![Nuget](https://img.shields.io/nuget/v/ReleaseChecker.GitHub)

A simple library that can be used for basic update checking by requesting a list of releases from GitHub repositories.

Usage example:

```csharp
// Getting the latest release and comparing the version with the current version of the application
GitHubChecker gitHubChecker = new GitHubChecker("RepoAuthor", "RepoName");
var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
var latestRelease = await gitHubChecker.GetLatestReleaseAsync();
if (latestRelease != null && new Version(latestRelease.Tag) > currentVersion)
  ShowUpdateAvailableDialog();

// Getting a list of releases
GitHubChecker gitHubChecker = new GitHubChecker("RepoAuthor", "RepoName");
var release = await gitHubChecker.GetReleasesAsync(pageNumber, pageSize, includePreReleases);
```

## ReleaseChecker.Demo
Demo project showing all the features of the libraries
![ReleaseChecker.Demo](/Screenshots/demo.github.png?raw=true)
