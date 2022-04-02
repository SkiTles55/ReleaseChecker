using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ReleaseChecker.GitHub
{
    public class GitHubChecker : ReleaseChecker<GitHubRelease>
    {
        private readonly string _repositoryURL;

        private readonly HttpClient httpClient;

        public GitHubChecker(string author, string repository)
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", repository);
            _repositoryURL = "https://api.github.com/repos/" + author + "/" + repository + "/releases";
        }

        public bool HasNextPage { get; set; } = false;

        public override async Task<List<GitHubRelease>> GetReleasesAsync(int page = 1, int pageSize = 30, bool includePreRelease = false)
        {
            var githubReleases = await GetReleasesAsync(true, page, pageSize);
            if (githubReleases != null && githubReleases.Length > 0)
                return githubReleases.Where(x => includePreRelease || x.PreRelease == false).ToList();
            return new List<GitHubRelease>();
        }

        public override async Task<GitHubRelease?> GetLatestReleaseAsync(bool includePreRelease = false)
        {
            var githubReleases = await GetReleasesAsync();
            if (githubReleases != null && githubReleases.Length > 0)
                return githubReleases.Where(x => includePreRelease || x.PreRelease == false).FirstOrDefault();
            return null;
        }

        private static void VerifyGitHubAPIResponse(HttpStatusCode statusCode, string content)
        {
            switch (statusCode)
            {
                case HttpStatusCode.Forbidden when content.Contains("API rate limit exceeded"):
                    throw new RateLimitExceededException();
                case HttpStatusCode.NotFound when content.Contains("Not Found"):
                    throw new NotFoundException();
                default:
                    {
                        if (statusCode != HttpStatusCode.OK)
                            throw new APICallFailedException();
                        break;
                    }
            }
        }

        private static bool ResponseHasNextPage(HttpResponseMessage? headers)
        {
            if (headers == null)
                return false;
            try
            {
                string linkHeader = headers.Headers.GetValues("Link").First();
                if (string.IsNullOrWhiteSpace(linkHeader))
                    return false;
                var links = linkHeader.Split(',');
                return links.Any() && links.Where(x => x.Contains(@"rel=""next""")).Select(x => Regex.Match(x, "(?<=page=)(.*)(?=>;)").Value).FirstOrDefault() != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<GitHubRelease[]?> GetReleasesAsync(bool checkNextPage = false, int page = 1, int pageSize = 30)
        {
            var response = await httpClient.GetAsync(new Uri(_repositoryURL + "?page=" + page + "&per_page=" + pageSize));
            HasNextPage = checkNextPage && ResponseHasNextPage(response);
            var contentJson = await response.Content.ReadAsStringAsync();
            VerifyGitHubAPIResponse(response.StatusCode, contentJson);
            return JsonSerializer.Deserialize<GitHubRelease[]>(contentJson);
        }
    }
}