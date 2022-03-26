using System.Text.Json.Serialization;

namespace ReleaseChecker.GitHub
{
    public class GithubReleaseFile : ReleaseFile
    {
        [JsonPropertyName("name")]
        public new string Name { get; set; } = "";

        [JsonPropertyName("browser_download_url")]
        public new string Url { get; set; } = "";

        [JsonPropertyName("size")]
        public new long Size { get; set; }

        [JsonPropertyName("download_count")]
        public new int DownloadCount { get; set; }

        public override void Download(string targetPath)
        {
            throw new NotImplementedException();
        }
    }
}