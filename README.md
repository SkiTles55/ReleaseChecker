## ReleaseChecker
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

## ReleaseChecker.Demo
Демо проект показывающий все возможности библиотек
