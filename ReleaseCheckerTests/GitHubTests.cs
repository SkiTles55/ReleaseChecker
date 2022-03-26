using NUnit.Framework;
using ReleaseChecker.GitHub;
using System;
using System.Linq;

namespace ReleaseCheckerTests
{
    internal class ReleaseCheckerTests
    {
        private readonly GitHubChecker dotNetCoreRepo = new("dotnet", "core");
        private readonly GitHubChecker testRepo = new("Test", "NotFound");
        private readonly GitHubChecker profileEditorRepo = new("SkiTles55", "SPT-AKI-Profile-Editor");

        [Test]
        public void CanThrowNotFoundException()
        {
            var ex = Assert.ThrowsAsync<NotFoundException>(() => testRepo.GetReleasesAsync());
            Assert.NotNull(ex, "Exception is null");
            Assert.AreEqual("GitHub Repo not found.", ex!.Message, "Exception is not NotFoundException");
        }

        [Test]
        public void CanGetReleases()
        {
            var releases = dotNetCoreRepo.GetReleasesAsync().Result;
            Assert.NotNull(releases, "Releases is null");
            Assert.IsNotEmpty(releases, "Releases is empty");
            Assert.IsTrue(releases.Count <= 30, "Releases count greater than default page size");
            Assert.IsTrue(dotNetCoreRepo.HasNextPage, "Release checker dont has next page");
        }

        [Test]
        public void CanGetPreReleases()
        {
            var releases = dotNetCoreRepo.GetReleasesAsync(includePreRelease: true).Result;
            Assert.NotNull(releases, "Releases is null");
            Assert.IsNotEmpty(releases, "Releases is empty");
            Assert.IsTrue(releases.Any(x => x.PreRelease), "Releases dont has pre-releases");
        }

        [Test]
        public void DontHasNextPage()
        {
            var releases = profileEditorRepo.GetReleasesAsync().Result;
            Assert.NotNull(releases, "Releases is null");
            Assert.IsNotEmpty(releases, "Releases is empty");
            Assert.IsTrue(releases.Count <= 30, "Releases count greater than default page size");
            Assert.IsFalse(profileEditorRepo.HasNextPage, "Release checker has next page");
        }

        [Test]
        public void CanGetLatestRelease()
        {
            var release = profileEditorRepo.GetLatestReleaseAsync().Result;
            Assert.NotNull(release, "Release is null");
            Assert.IsNotEmpty(release!.Tag, "Tag is empty");
            Assert.IsNotEmpty(release.Title, "Title is empty");
            Assert.IsNotEmpty(release.Description, "Description is empty");
            Assert.IsTrue(release.PublishDate != DateTime.Now, "PublishDate is wrong");
            Assert.IsNotEmpty(release.Url, "Url is empty");
            Assert.NotNull(release.Files, "Files is null");
            Assert.IsNotEmpty(release.Files!, "Files is empty");
        }
    }
}