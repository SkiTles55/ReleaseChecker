namespace ReleaseChecker
{
    public abstract class ReleaseFile
    {
        public abstract string Name { get; set; }
        public abstract string Url { get; set; }
        public abstract long Size { get; set; }
        public abstract int DownloadCount { get; set; }
    }
}