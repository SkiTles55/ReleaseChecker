using ReleaseChecker.GitHub;
using ReleaseCheckerDemo.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReleaseCheckerDemo.ViewModels
{
    internal class GitHubViewModel : ReleaseCheckerViewModel
    {
        private readonly ReleaseCheckerModel<GitHubChecker, GitHubRelease> _gitHubModel = new();

        public override RelayCommand GetReleases => new(async obj => { await GetGitHubReleases(); });

        public override RelayCommand GetLatestRelease => new(async obj => { await GetGitHubLatestRelease(); });

        public override RelayCommand GetNextPage => new(async obj => { await GetGitHubReleasesNextPage(); });

        public override RelayCommand GetPreviousPage => new(async obj => { await GetGitHubReleasesPreviousPage(); });

        public ObservableCollection<GitHubRelease> Releases => _gitHubModel.Releases;

        public new string RepoAuthor { get; set; } = "SkiTles55";

        public new string RepoName { get; set; } = "SP-EFT-ProfileEditor";

        private async Task GetGitHubReleasesNextPage()
        {
            Page += 1;
            await GetGitHubReleases();
        }

        private async Task GetGitHubReleasesPreviousPage()
        {
            Page -= 1;
            await GetGitHubReleases();
        }

        private async Task GetGitHubLatestRelease()
        {
            PrepareGitHubChecker();
            try
            {
                await _gitHubModel.GetLatestRelease(IncludePreRelease);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            UpdatePageButtons();
        }

        private void UpdatePageButtons()
        {
            IsNextPageButtonEnabled = _gitHubModel.Checker?.HasNextPage ?? false;
            IsPreviousPageButtonEnabled = Page > 1;
        }

        private async Task GetGitHubReleases()
        {
            PrepareGitHubChecker();
            try
            {
                await _gitHubModel.GetReleases(Page, PageSize, IncludePreRelease);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            UpdatePageButtons();
        }

        private void PrepareGitHubChecker()
        {
            if (_gitHubModel.Checker == null)
            {
                SetupGitHubChecker();
            }
            else if (repoAuthor != RepoAuthor || repoName != RepoName)
            {
                _gitHubModel.Checker = null;
                SetupGitHubChecker();
            }
        }

        private void SetupGitHubChecker()
        {
            repoAuthor = RepoAuthor;
            repoName = RepoName;
            _gitHubModel.Checker = new(RepoAuthor, RepoName);
        }
    }
}