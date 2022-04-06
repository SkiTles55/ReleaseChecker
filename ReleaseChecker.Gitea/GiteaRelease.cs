using System.Text.Json.Serialization;

namespace ReleaseChecker.Gitea
{
    public class GiteaRelease : Release
    {
        [JsonPropertyName("tag_name")]
        public new string Tag { get; set; } = "";

        [JsonPropertyName("name")]
        public new string Title { get; set; } = "";

        [JsonPropertyName("body")]
        public new string Description { get; set; } = "";

        [JsonPropertyName("published_at")]
        public new DateTime PublishDate { get; set; }

        [JsonPropertyName("prerelease")]
        public new bool PreRelease { get; set; }

        [JsonPropertyName("html_url")]
        public new string Url { get; set; } = "";

        [JsonPropertyName("assets")]
        public new GiteaReleaseFile[]? Files { get; set; }
    }
}