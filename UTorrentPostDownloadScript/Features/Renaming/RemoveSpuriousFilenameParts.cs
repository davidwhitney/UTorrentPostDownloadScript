using System.Configuration.Abstractions;
using System.IO.Abstractions;
using System.Linq;
using UTorrentPostDownloadScript.UtorrentApi;

namespace UTorrentPostDownloadScript.Features.Renaming
{
    public class RemoveSpuriousFilenameParts : IActOnCompletedTorrents
    {
        private readonly IAppSettings _appSettings;
        private readonly IFileSystem _fileSystem;

        public RemoveSpuriousFilenameParts(IAppSettings appSettings, IFileSystem fileSystem)
        {
            _appSettings = appSettings;
            _fileSystem = fileSystem;
        }

        public void Handle(UtorrentCommandLineParameters parameters)
        {
            var badParts =
                (_appSettings.AllKeys.Where(key => key.StartsWith("RemoveSpuriousFilenameParts::"))
                    .Select(key => _appSettings[key])).ToList();

            if (badParts.Count == 0)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(parameters.DirectoryWhereFilesAreSaved))
            {
                var dest = badParts.Aggregate(parameters.DirectoryWhereFilesAreSaved,
                    (current, badPart) => current.Replace(badPart, string.Empty));

                _fileSystem.Directory.Move(parameters.DirectoryWhereFilesAreSaved, dest);
                
                parameters.DirectoryWhereFilesAreSaved = dest;
            }

            if (!string.IsNullOrWhiteSpace(parameters.NameOfDownloadedFileForSingleFileTorrents))
            {
                var dest = badParts.Aggregate(parameters.NameOfDownloadedFileForSingleFileTorrents,
                    (current, badPart) => current.Replace(badPart, string.Empty));

                _fileSystem.File.Move(parameters.NameOfDownloadedFileForSingleFileTorrents, dest);

                parameters.NameOfDownloadedFileForSingleFileTorrents = dest;
            }
        }
    }
}
