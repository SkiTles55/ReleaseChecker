namespace ReleaseCheckerDemo.ViewModels
{
    internal class ReleaseCheckerViewModel : BindableEntity
    {
        protected string repoAuthor = string.Empty;
        protected string repoName = string.Empty;
        private int page = 1;
        private bool isNextPageButtonEnabled;
        private bool isPreviousPageButtonEnabled;
        private int pageSize = 30;

        public virtual RelayCommand? GetReleases { get; }
        public virtual RelayCommand? GetLatestRelease { get; }
        public virtual RelayCommand? GetNextPage { get; }
        public virtual RelayCommand? GetPreviousPage { get; }
        public virtual bool IsServerFieldEnabled { get; }
        public string ServerUrl { get; set; } = string.Empty;
        public string RepoAuthor { get; set; } = string.Empty;

        public string RepoName { get; set; } = string.Empty;

        public bool IncludePreRelease { get; set; } = false;

        public int Page
        {
            get => page;
            set
            {
                page = value;
                OnPropertyChanged("Page");
            }
        }

        public int PageSize
        {
            get => pageSize;
            set
            {
                pageSize = value;
                OnPropertyChanged("PageSize");
            }
        }

        public bool IsNextPageButtonEnabled
        {
            get => isNextPageButtonEnabled;
            set
            {
                isNextPageButtonEnabled = value;
                OnPropertyChanged("IsNextPageButtonEnabled");
            }
        }

        public bool IsPreviousPageButtonEnabled
        {
            get => isPreviousPageButtonEnabled;
            set
            {
                isPreviousPageButtonEnabled = value;
                OnPropertyChanged("IsPreviousPageButtonEnabled");
            }
        }
    }
}