namespace UTorrentPostDownloadScript
{
    public class Program
    {
        static void Main(string[] args)
        {
            var cliInterpreter = new CommandLineArgsInterpreter();
            var utorrentArgs = cliInterpreter.Parse(args);
        }
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
