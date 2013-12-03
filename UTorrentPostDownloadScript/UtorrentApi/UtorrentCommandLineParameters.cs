using UTorrentPostDownloadScript.Features.ArgumentParsing;

namespace UTorrentPostDownloadScript.UtorrentApi
{
    public class UtorrentCommandLineParameters : ParsableArguments<UtorrentCommandLineParameters>
    {
        public string NameOfDownloadedFileForSingleFileTorrents { get; set; }
        public string DirectoryWhereFilesAreSaved { get; set; }
        public string TitleOfTorrent { get; set; }
        public StateOfTorrent PreviousStateOfTorrent { get; set; }
        public string Label { get; set; }
        public string Tracker { get; set; }
        public string StatusMessage { get; set; }
        public string HexEndocdedInfoHash { get; set; }
        public StateOfTorrent StateOfTorrent { get; set; }
        public KindOfTorrent KindOfTorrent { get; set; }
        
        public UtorrentCommandLineParameters()
        {
            Add("f", (arguments, value) => arguments.NameOfDownloadedFileForSingleFileTorrents = value);
            Add("d", (arguments, value) => arguments.DirectoryWhereFilesAreSaved = value);
            Add("i", (arguments, value) => arguments.HexEndocdedInfoHash = value);
            Add("l", (arguments, value) => arguments.Label = value);
            Add("m", (arguments, value) => arguments.StatusMessage = value);
            Add("n", (arguments, value) => arguments.TitleOfTorrent = value);
            Add("t", (arguments, value) => arguments.Tracker = value);
            Add("k", (arguments, value) => ToEnum<KindOfTorrent>(value, v => arguments.KindOfTorrent = v));
            Add("s", (arguments, value) => ToEnum<StateOfTorrent>(value, v => arguments.StateOfTorrent = v));
            Add("p", (arguments, value) => ToEnum<StateOfTorrent>(value, v => arguments.PreviousStateOfTorrent = v));
        }

        public TypeOfDownload KindOfDownload
        {
            get { return new TypeOfDownload(this); }
        }
    }
}