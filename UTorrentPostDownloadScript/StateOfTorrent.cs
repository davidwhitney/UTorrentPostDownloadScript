namespace UTorrentPostDownloadScript
{
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