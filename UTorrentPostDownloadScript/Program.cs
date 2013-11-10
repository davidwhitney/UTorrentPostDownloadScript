using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using UTorrentPostDownloadScript.Features.ArgumentParsing;
using UTorrentPostDownloadScript.UtorrentApi;

namespace UTorrentPostDownloadScript
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var kernel = new StandardKernel(new Bindings());
            var allActions = kernel.GetAll<IActOnCompletedTorrents>();
            Main(args, new UtorrentCommandLineParameters(), allActions.ToList());
        }

        public static void Main(string[] args, IParsableArguments<UtorrentCommandLineParameters> supportedParameters, IEnumerable<IActOnCompletedTorrents> allActions)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(supportedParameters.GetHelp());
                return;
            }

            var utorrentArgs = supportedParameters.Parse(args);

            foreach (var action in allActions)
            {
                action.Handle(utorrentArgs);
            }
        }
    }
}