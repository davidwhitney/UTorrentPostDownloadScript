using System;

namespace UTorrentPostDownloadScript
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Main(args, new UtorrentCommandLineParameters());
        }

        public static void Main(string[] args, IParsableArguments<UtorrentCommandLineParameters> supportedParameters)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(supportedParameters.GetHelp());
                return;
            }

            var utorrentArgs = supportedParameters.Parse(args);
        }
    }
}