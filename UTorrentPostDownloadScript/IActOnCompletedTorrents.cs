using UTorrentPostDownloadScript.UtorrentApi;

namespace UTorrentPostDownloadScript
{
    public interface IActOnCompletedTorrents
    {
        void Handle(UtorrentCommandLineParameters parameters);
    }
}