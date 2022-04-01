using System.Diagnostics;

namespace ReleaseCheckerDemo
{
    internal static class ExtMethods
    {
        public static void OpenUrl(string url)
        {
            ProcessStartInfo link = new(url);
            link.UseShellExecute = true;
            Process.Start(link);
        }
    }
}