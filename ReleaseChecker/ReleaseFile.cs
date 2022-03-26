namespace ReleaseChecker
{
    public abstract class ReleaseFile
    {
        public string Name { get; set; } = "";
        public string Url { get; set; } = "";
        public long Size { get; set; }
        public int DownloadCount { get; set; }

        public abstract void Download(string targetPath);
    }
}