using System;
using System.Collections.Generic;

namespace UTorrentPostDownloadScript
{
    public class CommandLineArgsInterpreter
    {
        private readonly Dictionary<string, Action<Arguments, string>> _argumentMap;

        public CommandLineArgsInterpreter()
        {
            _argumentMap = new Dictionary<string, Action<Arguments, string>>
            {
                {"f", (arguments, value) => arguments.NameOfDownloadedFileForSingleFileTorrents = value},
                {"d", (arguments, value) => arguments.DirectoryWhereFilesAreSaved = value},
                {"i", (arguments, value) => arguments.HexEndocdedInfoHash = value},
                {"l", (arguments, value) => arguments.Label = value},
                {"m", (arguments, value) => arguments.StatusMessage = value},
                {"n", (arguments, value) => arguments.TitleOfTorrent = value},
                {"t", (arguments, value) => arguments.Tracker = value},
                {"k", (arguments, value) => ToEnum<KindOfTorrent>(value, v => arguments.KindOfTorrent = v)},
                {"s", (arguments, value) => ToEnum<StateOfTorrent>(value, v => arguments.StateOfTorrent = v)},
                {"p", (arguments, value) => ToEnum<StateOfTorrent>(value, v => arguments.PreviousStateOfTorrent = v)},
            };
        }

        public Arguments Parse(string[] args)
        {
            var cliParams = BuildDictionaryOfInputParams(args);

            var argumentss = new Arguments();
            foreach (var commandLineKey in _argumentMap)
            {
                var rawValue = ValueOrDefault<string>(cliParams, "-" + commandLineKey.Key);
                if (rawValue == null) continue;

                commandLineKey.Value(argumentss, rawValue);
            }

            return argumentss;
        }

        private static void ToEnum<T>(string value, Action<T> action) where T : struct
        {
            T k;
            if (Enum.TryParse(value, true, out k))
            {
                action(k);
            }
        }

        private static Dictionary<string, string> BuildDictionaryOfInputParams(string[] args)
        {
            var parameters = new Dictionary<string, string>();
            var lastKey = string.Empty;
            for (var i = 0; i < args.Length; i++)
            {
                var item = args[i];
                if ((i%2 == 0))
                {
                    lastKey = item;
                }
                else
                {
                    parameters.Add(lastKey, item);
                }
            }
            return parameters;
        }

        private static T ValueOrDefault<T>(IReadOnlyDictionary<string, string> src, string key)
        {
            try
            {
                string value;
                return src.TryGetValue(key, out value)
                    ? (T)Convert.ChangeType(value, typeof(T))
                    : default(T);
            }
            catch
            {
                return default(T);
            }
        }
    }
}