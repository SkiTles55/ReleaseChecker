namespace ReleaseChecker
{
    public abstract class ReleaseFile
    {
        public abstract string Name { get; set; }
        public abstract string Url { get; set; }
        public abstract long Size { get; set; }
        public abstract int DownloadCount { get; set; }

        public abstract Task<bool> DownloadAsync(string targetPath, IProgress<float>? progress = null, CancellationToken cancellationToken = default);
    }
}