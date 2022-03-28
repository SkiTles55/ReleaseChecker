using ReleaseChecker.GitHub;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReleaseCheckerDemo.Views
{
    internal class GitHubViewModel : BindableEntity
    {
        private GitHubChecker? gitHubChecker = null;
        private string repoAuthor = "SkiTles55";
        private string repoName = "SP-EFT-ProfileEditor";
        private List<GitHubRelease>? releases;

        public string RepoAuthor { get; set; } = "SkiTles55";

        public string RepoName { get; set; } = "SP-EFT-ProfileEditor";

        public bool IncludePreRelease { get; set; } = false;

        public List<GitHubRelease>? Releases
        {
            get => releases;
            set
            {
                releases = value;
                OnPropertyChanged("Releases");
            }
        }

        public RelayCommand GetGitHubReleases => new(async obj => { await GetReleases(); });

        private async Task GetReleases()
        {
            if (gitHubChecker == null)
            {
                SetupGitHubChecker();
            }
            else if (repoAuthor != RepoAuthor && repoName != RepoName)
            {
                gitHubChecker = null;
                SetupGitHubChecker();
            }
            try
            {
                Releases = await gitHubChecker!.GetReleasesAsync(includePreRelease: IncludePreRelease);
            }
            catch (Exception ex)
            {

            }
        }

        private void SetupGitHubChecker()
        {
            repoAuthor = RepoAuthor;
            repoName = RepoName;
            gitHubChecker = new(RepoAuthor, RepoName);
        }
    }
}