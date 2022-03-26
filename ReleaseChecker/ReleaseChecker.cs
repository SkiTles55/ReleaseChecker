namespace ReleaseChecker
{
    public abstract class ReleaseChecker<T> where T : Release
    {
        public abstract Task<List<T>> GetReleasesAsync(int page = 1, int pageSize = 30, bool includePreRelease = false);

        public abstract Task<T?> GetLatestReleaseAsync(bool includePreRelease = false);
    }
}