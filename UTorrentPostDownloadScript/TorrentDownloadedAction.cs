using System;
using System.Collections.Generic;
using log4net;
using UTorrentPostDownloadScript.Features.ArgumentParsing;
using UTorrentPostDownloadScript.UtorrentApi;

namespace UTorrentPostDownloadScript
{
    public class TorrentDownloadedAction
    {
        private readonly IParsableArguments<UtorrentCommandLineParameters> _supportedParameters;
        private readonly IEnumerable<IActOnCompletedTorrents> _allActions;
        private readonly ILog _logger;

        public TorrentDownloadedAction(IParsableArguments<UtorrentCommandLineParameters> supportedParameters, IEnumerable<IActOnCompletedTorrents> allActions, ILog logger)
        {
            _supportedParameters = supportedParameters;
            _allActions = allActions;
            _logger = logger;
        }

        public void Execute(string[] args)
        {
            _logger.Info("UTorrentPostDownloadScript");
            _logger.Info("Called with " + string.Join(" ", args));

            if (args.Length == 0)
            {
                Console.WriteLine(_supportedParameters.GetHelp());
                return;
            }

            var utorrentArgs = _supportedParameters.Parse(args);

            foreach (var action in _allActions)
            {
                action.Handle(utorrentArgs);
            }
        }
    }
}