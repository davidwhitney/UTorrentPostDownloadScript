namespace UTorrentPostDownloadScript
{
    class Program
    {
        static void Main(string[] args)
        {
            var utorrentArgs = CommandLineArgsInterpreter.Parse(args);
        }
    }

    public class CommandLineArgsInterpreter
    {
        public static Arguments Parse(string[] args)
        {
            return new Arguments();
        }
    }

    public class Arguments
    {
        public string NameOfDownloadedFileForsingleFileTorrents { get; set; }
        public string DirectoryWhereFilesAreSaved { get; set; }
        public string TitleOfTorrent { get; set; }
        public StateOfTorrent PreviousStateOfTorrent { get; set; }
        public string Label { get; set; }
        public string Tracker { get; set; }
        public string StatusMessage { get; set; }
        public string HexEndocdedInfoHash { get; set; }
        public StateOfTorrent StateOfTorrent { get; set; }
        public KindOfTorrent KindOfTorrent { get; set; }
    }

    public enum KindOfTorrent
    {
        Single,
        Multi
    }

    public enum StateOfTorrent
    {
        Error = 1,
        Checked = 2,
        Paused = 3,
        SuperSeeding = 4,
        Seeding = 5,
        Downloading = 6,
        SuperSeedF = 7,
        SeedingF = 8,
        DownloadingF = 9,
        QueuedSeed = 10,
        Finished = 11,
        Queued = 12,
        Stopped = 13
    }
}
