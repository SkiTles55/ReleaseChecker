using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ReleaseChecker.Gitea
{
    public class GiteaChecker : ReleaseChecker<GiteaRelease>
    {
        private readonly string _repositoryURL;

        private readonly HttpClient httpClient;

        public GiteaChecker(string server, string author, string repository)
        {
            if (server.EndsWith("/"))
                server = server.Remove(server.Length - 1);
            //if (server.StartsWith("https://") || server.StartsWith("http://"))
            //    throw new InvalidServerUrlException();

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", repository);

            _repositoryURL = server + "/api/v1/repos/" + author + "/" + repository + "/releases";
        }

        public bool HasNextPage { get; set; } = false;

        public override async Task<List<GiteaRelease>> GetReleasesAsync(int page = 1, int pageSize = 30, bool includePreRelease = false)
        {
            var giteaReleases = await GetReleasesAsync(true, page, pageSize);
            if (giteaReleases != null && giteaReleases.Length > 0)
                return giteaReleases.Where(x => includePreRelease || x.PreRelease == false).ToList();
            return new List<GiteaRelease>();
        }

        public override async Task<GiteaRelease?> GetLatestReleaseAsync(bool includePreRelease = false)
        {
            var giteaReleases = await GetReleasesAsync();
            if (giteaReleases != null && giteaReleases.Length > 0)
                return giteaReleases.Where(x => includePreRelease || x.PreRelease == false).FirstOrDefault();
            return null;
        }

        private static void VerifyGiteaAPIResponse(HttpStatusCode statusCode, string content)
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

        private async Task<GiteaRelease[]?> GetReleasesAsync(bool checkNextPage = false, int page = 1, int pageSize = 30)
        {
            var response = await httpClient.GetAsync(new Uri(_repositoryURL + "?page=" + page + "&per_page=" + pageSize));
            HasNextPage = checkNextPage && ResponseHasNextPage(response);
            var contentJson = await response.Content.ReadAsStringAsync();
            //VerifyGiteaAPIResponse(response.StatusCode, contentJson);
            return JsonSerializer.Deserialize<GiteaRelease[]>(contentJson);
        }
    }
}