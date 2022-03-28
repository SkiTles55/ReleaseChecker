using System.Text.Json.Serialization;

namespace ReleaseChecker.GitHub
{
    public class GithubReleaseFile : ReleaseFile
    {
        [JsonPropertyName("name")]
        public override string Name { get; set; } = "";

        [JsonPropertyName("browser_download_url")]
        public override string Url { get; set; } = "";

        [JsonPropertyName("size")]
        public override long Size { get; set; }

        [JsonPropertyName("download_count")]
        public override int DownloadCount { get; set; }

        public override async Task<bool> DownloadAsync(string targetPath, IProgress<float>? progress = null, CancellationToken cancellationToken = default)
        {
            try
            {
                using var client = new HttpClient();
                using var file = new FileStream(targetPath, FileMode.Create, FileAccess.Write, FileShare.None);
                await DownloadAsync(client, Url, file, progress, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}