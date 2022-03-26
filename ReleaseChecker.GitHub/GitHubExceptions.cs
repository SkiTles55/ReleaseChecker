namespace ReleaseChecker.GitHub
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        : base("GitHub Repo not found.") { }
    }

    public class RateLimitExceededException : Exception
    {
        public RateLimitExceededException()
        : base("GitHub API rate limit exceeded.") { }
    }

    public class APICallFailedException : Exception
    {
        public APICallFailedException()
        : base("GitHub API call failed.") { }
    }
}