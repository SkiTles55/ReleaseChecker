using NUnit.Framework;
using System;
using System.Linq;

namespace ReleaseChecker.Gitea.Tests
{
    internal class GiteaTests
    {
        private readonly GiteaChecker testRepo = new("https://dev.sp-tarkov.com/", "SPT-AKI", "NotFound");
        private readonly GiteaChecker testRepo2 = new("https://dev.sp-tarkov.com/", "Test", "NotFound");
        private readonly GiteaChecker sptAkiRepo = new("https://dev.sp-tarkov.com/", "SPT-AKI", "Stable-releases");
        private readonly GiteaChecker emptyRepo = new("https://dev.sp-tarkov.com/", "SPT-AKI", "Modules");
        private readonly GiteaChecker prereleaseRepo = new("https://try.gitea.io/", "skitles55", "hackintosh-ryzen1400-msi-b350m-bazooka");

        [Test]
        public void CanThrowInvalidServerUrlException()
        {
            var ex = Assert.Throws<InvalidServerUrlException>(() => { GiteaChecker testRepo = new("dev.test.com/", "SPT-AKI", "NotFound"); });
            Assert.NotNull(ex, "Exception is null");
            Assert.AreEqual("Invalid Gitea server url.", ex!.Message, "Exception is not InvalidServerUrlException");
        }

        [Test]
        public void CanThrowNotFoundException()
        {
            var ex = Assert.ThrowsAsync<NotFoundException>(() => testRepo.GetReleasesAsync());
            Assert.NotNull(ex, "Exception is null");
            Assert.AreEqual("Gitea Repo not found.", ex!.Message, "Exception is not NotFoundException");
        }

        [Test]
        public void CanThrowNotFoundException2()
        {
            var ex = Assert.ThrowsAsync<NotFoundException>(() => testRepo2.GetReleasesAsync());
            Assert.NotNull(ex, "Exception is null");
            Assert.AreEqual("Gitea Repo not found.", ex!.Message, "Exception is not NotFoundException");
        }

        [Test]
        public void CanGetReleases()
        {
            var releases = sptAkiRepo.GetReleasesAsync().Result;
            Assert.NotNull(releases, "Releases is null");
            Assert.IsNotEmpty(releases, "Releases is empty");
            Assert.IsTrue(releases.Count <= 30, "Releases count greater than default page size");
        }

        [Test]
        public void CanGetEmptyReleases()
        {
            var releases = emptyRepo.GetReleasesAsync().Result;
            Assert.NotNull(releases, "Releases is null");
            Assert.IsEmpty(releases, "Releases is not empty");
        }

        [Test]
        public void CanGetPreReleases()
        {
            var releases = prereleaseRepo.GetReleasesAsync(includePreRelease: true).Result;
            Assert.NotNull(releases, "Releases is null");
            Assert.IsNotEmpty(releases, "Releases is empty");
            Assert.IsTrue(releases.Any(x => x.PreRelease), "Releases dont has pre-releases");
        }

        [Test]
        public void DontHasNextPage()
        {
            var releases = sptAkiRepo.GetReleasesAsync().Result;
            Assert.NotNull(releases, "Releases is null");
            Assert.IsNotEmpty(releases, "Releases is empty");
            Assert.IsTrue(releases.Count <= 30, "Releases count greater than default page size");
            Assert.IsFalse(sptAkiRepo.HasNextPage, "Release checker has next page");
        }

        [Test]
        public void HasNextPage()
        {
            var releases = sptAkiRepo.GetReleasesAsync(1, 3).Result;
            Assert.NotNull(releases, "Releases is null");
            Assert.IsNotEmpty(releases, "Releases is empty");
            Assert.IsFalse(releases.Count > 3, "Releases count greater than page size");
            Assert.IsTrue(sptAkiRepo.HasNextPage, "Release checker dont has next page");
        }

        [Test]
        public void CanGetLatestRelease()
        {
            var release = sptAkiRepo.GetLatestReleaseAsync().Result;
            Assert.NotNull(release, "Release is null");
            Assert.IsNotEmpty(release!.Tag, "Tag is empty");
            Assert.IsNotEmpty(release.Title, "Title is empty");
            Assert.IsNotEmpty(release.Description, "Description is empty");
            Assert.IsTrue(release.PublishDate != DateTime.Now, "PublishDate is wrong");
            Assert.IsNotEmpty(release.Url, "Url is empty");
            Assert.NotNull(release.Files, "Files is null");
            Assert.IsNotEmpty(release.Files!, "Files is empty");
        }

        [Test]
        public void CanGetLatestPreRelease()
        {
            var release = sptAkiRepo.GetLatestReleaseAsync(includePreRelease: true).Result;
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
