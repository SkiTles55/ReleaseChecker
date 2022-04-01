namespace ReleaseCheckerDemo.ViewModels
{
    internal class ReleaseCheckerViewModel : BindableEntity
    {
        protected string repoAuthor = "SkiTles55";
        protected string repoName = "SP-EFT-ProfileEditor";

        public virtual RelayCommand? GetReleases { get; }
        public virtual RelayCommand? GetLatestRelease { get; }

        public string RepoAuthor { get; set; } = "SkiTles55";

        public string RepoName { get; set; } = "SP-EFT-ProfileEditor";

        public bool IncludePreRelease { get; set; } = false;

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 30;
    }
}