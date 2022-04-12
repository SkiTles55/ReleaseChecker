## ReleaseChecker [ENG README version](ENGREADME.MD)
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/SkiTles55/ReleaseChecker/publish-ReleaseChecker)
![Nuget](https://img.shields.io/nuget/dt/ReleaseChecker)
![GitHub](https://img.shields.io/github/license/SkiTles55/ReleaseChecker)
![Nuget](https://img.shields.io/nuget/v/ReleaseChecker)

## ReleaseChecker.GitHub
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/SkiTles55/ReleaseChecker/publish-ReleaseChecker-GitHub)
![Nuget](https://img.shields.io/nuget/dt/ReleaseChecker.GitHub)
![GitHub](https://img.shields.io/github/license/SkiTles55/ReleaseChecker)
![Nuget](https://img.shields.io/nuget/v/ReleaseChecker.GitHub)

Простая библиотека, которую можно использовать для базовой проверки обновлений, запрашивая список релизов из репозиториев GitHub.

Пример использования:

```csharp
// Получение последнего релиза и сравнение версии с текущей версией приложения
GitHubChecker gitHubChecker = new GitHubChecker("RepoAuthor", "RepoName");
var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
var latestRelease = await gitHubChecker.GetLatestReleaseAsync();
if (latestRelease != null && new Version(latestRelease.Tag) > currentVersion)
  ShowUpdateAvailableDialog();

// Получение списка релизов
GitHubChecker gitHubChecker = new GitHubChecker("RepoAuthor", "RepoName");
var release = await gitHubChecker.GetReleasesAsync(pageNumber, pageSize, includePreReleases);
```

## ReleaseChecker.Gitea
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/SkiTles55/ReleaseChecker/publish-ReleaseChecker-Gitea)
![Nuget](https://img.shields.io/nuget/dt/ReleaseChecker.Gitea)
![GitHub](https://img.shields.io/github/license/SkiTles55/ReleaseChecker)
![Nuget](https://img.shields.io/nuget/v/ReleaseChecker.Gitea)

Простая библиотека, которую можно использовать для базовой проверки обновлений, запрашивая список релизов из репозиториев Gitea.

Пример использования:

```csharp
// Получение последнего релиза и сравнение версии с текущей версией приложения
GiteaChecker giteaChecker = new GiteaChecker("ServerUrl", "RepoAuthor", "RepoName");
var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
var latestRelease = await giteaChecker.GetLatestReleaseAsync();
if (latestRelease != null && new Version(latestRelease.Tag) > currentVersion)
  ShowUpdateAvailableDialog();

// Получение списка релизов
GiteaChecker giteaChecker = new GiteaChecker("ServerUrl", "RepoAuthor", "RepoName");
var release = await giteaChecker.GetReleasesAsync(pageNumber, pageSize, includePreReleases);
```

## ReleaseChecker.Demo
Демо проект показывающий все возможности библиотек
![ReleaseChecker.Demo](/Screenshots/demo.github.png?raw=true)
![ReleaseChecker.Demo](/Screenshots/demo.gitea.png?raw=true)
