namespace ReleaseChecker.Gitea
{
    public class InvalidServerUrlException : Exception
    {
        public InvalidServerUrlException()
        : base("Invalid Gitea server url.") { }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException()
        : base("Gitea Repo not found.") { }
    }

    public class RateLimitExceededException : Exception
    {
        public RateLimitExceededException()
        : base("Gitea API rate limit exceeded.") { }
    }

    public class APICallFailedException : Exception
    {
        public APICallFailedException()
        : base("Gitea API call failed.") { }
    }
}