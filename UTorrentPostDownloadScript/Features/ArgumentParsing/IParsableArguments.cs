namespace UTorrentPostDownloadScript.Features.ArgumentParsing
{
    public interface IParsableArguments<out T> where T : class, new()
    {
        T Parse(string[] args);
        string GetHelp();
    }
}