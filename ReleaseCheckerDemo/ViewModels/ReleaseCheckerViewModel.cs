namespace ReleaseCheckerDemo.ViewModels
{
    internal class ReleaseCheckerViewModel : BindableEntity
    {
        protected string repoAuthor = string.Empty;
        protected string repoName = string.Empty;

        public virtual RelayCommand? GetReleases { get; }
        public virtual RelayCommand? GetLatestRelease { get; }

        public string RepoAuthor { get; set; } = string.Empty;

        public string RepoName { get; set; } = string.Empty;

        public bool IncludePreRelease { get; set; } = false;

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 30;
    }
}