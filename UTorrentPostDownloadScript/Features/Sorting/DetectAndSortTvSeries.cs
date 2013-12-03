using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using UTorrentPostDownloadScript.UtorrentApi;

namespace UTorrentPostDownloadScript.Features.Sorting
{
    public class DetectAndSortTvSeries : IActOnCompletedTorrents
    {
        public void Handle(UtorrentCommandLineParameters parameters)
        {
            var tvShowRegex = new Regex("(.*)S([0-9]{1,2})E([0-9]{1,2}).*");

            var kindOfDownload = parameters.KindOfDownload;

            if (!tvShowRegex.IsMatch(kindOfDownload.Location))
            {
                return;
            }

            Debug.WriteLine("Is TV show");
            Debug.WriteLine("IsDirectory: " + kindOfDownload.IsDirectory);

            var captures = tvShowRegex.Matches(kindOfDownload.Location);

            var show = captures[0].Groups[1].Captures[0].Value;
            var season = captures[0].Groups[2].Captures[0].Value;
            var episode = captures[0].Groups[3].Captures[0].Value;

            var path = Path.GetDirectoryName(kindOfDownload.Location);
            var justTheEndBit = kindOfDownload.Location.Replace(path, string.Empty);


        }
    }
}
