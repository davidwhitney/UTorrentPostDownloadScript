using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using log4net.Config;
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

            XmlConfigurator.Configure();
            var logger = kernel.Get<ILog>();
            var allActions = kernel.GetAll<IActOnCompletedTorrents>().ToList();
            Main(args, new UtorrentCommandLineParameters(), allActions.ToList(), logger);
        }

        public static void Main(string[] args, IParsableArguments<UtorrentCommandLineParameters> supportedParameters, IEnumerable<IActOnCompletedTorrents> allActions, ILog logger)
        {
            logger.Info("UTorrentPostDownloadScript");
            logger.Info("Called with " + string.Join(" ", args));

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