namespace ReleaseChecker
{
    public class Release
    {
        public string Tag { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime PublishDate { get; set; }
        public bool PreRelease { get; set; }
        public string Url { get; set; } = "";
        public ReleaseFile[]? Files { get; set; }
    }
}