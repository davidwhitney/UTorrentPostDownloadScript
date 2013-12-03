namespace UTorrentPostDownloadScript.UtorrentApi
{
    public class TypeOfDownload
    {
        public bool IsDirectory { get; private set; }
        public string Location { get; private set; }

        public TypeOfDownload(UtorrentCommandLineParameters cliParams)
        {
            if (!string.IsNullOrWhiteSpace(cliParams.DirectoryWhereFilesAreSaved))
            {
                Location = cliParams.DirectoryWhereFilesAreSaved;
                IsDirectory = true;
            }
            else
            {
                Location = cliParams.NameOfDownloadedFileForSingleFileTorrents ?? string.Empty;
            }
        }
    }
}