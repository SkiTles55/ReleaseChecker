﻿using NUnit.Framework;
using System;
using System.Linq;

namespace ReleaseChecker.Gitea.Tests
{
    internal class GiteaTests
    {
        private readonly GiteaChecker dotNetCoreRepo = new("https://dev.sp-tarkov.com/", "SPT-AKI", "Stable-releases");
        private readonly GiteaChecker testRepo = new("https://dev.sp-tarkov.com/", "Test", "NotFound");
        private readonly GiteaChecker sptAkiRepo = new("https://dev.sp-tarkov.com/", "SPT-AKI", "Stable-releases");
        private readonly GiteaChecker emptyRepo = new("https://dev.sp-tarkov.com/", "SPT-AKI", "Modules");
        private readonly GiteaChecker prereleaseRepo = new("test", "SkiTles55", "NetSwitcher");

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
        public void CanGetEmptyReleases()
        {
            var releases = emptyRepo.GetReleasesAsync().Result;
            Assert.NotNull(releases, "Releases is null");
            Assert.IsEmpty(releases, "Releases is not empty");
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
            var releases = sptAkiRepo.GetReleasesAsync().Result;
            Assert.NotNull(releases, "Releases is null");
            Assert.IsNotEmpty(releases, "Releases is empty");
            Assert.IsTrue(releases.Count <= 30, "Releases count greater than default page size");
            Assert.IsFalse(sptAkiRepo.HasNextPage, "Release checker has next page");
        }

        [Test]
        public void HasNextPage()
        {
            var releases = dotNetCoreRepo.GetReleasesAsync(1, 3).Result;
            Assert.NotNull(releases, "Releases is null");
            Assert.IsNotEmpty(releases, "Releases is empty");
            Assert.IsFalse(releases.Count > 3, "Releases count greater than page size");
            Assert.IsTrue(dotNetCoreRepo.HasNextPage, "Release checker dont has next page");
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

        [Test]
        public void CanGetEmptyLatestRelease()
        {
            var release = prereleaseRepo.GetLatestReleaseAsync().Result;
            Assert.IsNull(release, "Release is not null");
        }
    }
}
