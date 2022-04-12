using System.Text.Json.Serialization;

namespace ReleaseChecker.Gitea
{
    public class GiteaReleaseFile : ReleaseFile
    {
        [JsonPropertyName("name")]
        public override string Name { get; set; } = "";

        [JsonPropertyName("browser_download_url")]
        public override string Url { get; set; } = "";

        [JsonPropertyName("size")]
        public override long Size { get; set; }

        [JsonPropertyName("download_count")]
        public override int DownloadCount { get; set; }
    }
}