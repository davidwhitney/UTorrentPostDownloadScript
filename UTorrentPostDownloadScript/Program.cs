using System;

namespace UTorrentPostDownloadScript
{
    public class Program
    {
        static void Main(string[] args)
        {
            var cliInterpreter = new CommandLineArgsInterpreter();

            if (args.Length == 0)
            {
                Console.WriteLine(cliInterpreter.GetHelp());
                return;
            }

            var utorrentArgs = cliInterpreter.Parse(args);
        }
    }
}