using System;
using System.Collections.Generic;

namespace UTorrentPostDownloadScript
{
    public class CommandLineArgsInterpreter
    {
        public static Arguments Parse(string[] args)
        {
            var cliParams = BuildDictionaryOfInputParams(args);

            var arguments = new Arguments();
            var dictionary = new Dictionary<string, Action<string>>
            {
                {"f", value => arguments.NameOfDownloadedFileForSingleFileTorrents = value},
                {"d", value => arguments.DirectoryWhereFilesAreSaved = value},
                {"i", value => arguments.HexEndocdedInfoHash = value},
                {"l", value => arguments.Label = value},
                {"m", value => arguments.StatusMessage = value},
                {"n", value => arguments.TitleOfTorrent = value},
                {"t", value => arguments.Tracker = value},
                {"k", value => ToEnum<KindOfTorrent>(value, v => arguments.KindOfTorrent = v)},
                {"s", value => ToEnum<StateOfTorrent>(value, v => arguments.StateOfTorrent = v)},
                {"p", value => ToEnum<StateOfTorrent>(value, v => arguments.PreviousStateOfTorrent = v)},
            };

            foreach (var commandLineKey in dictionary)
            {
                var rawValue = ValueOrDefault<string>(cliParams, "-" + commandLineKey.Key);
                if (rawValue == null) continue;
                
                commandLineKey.Value(rawValue);
            }

            return arguments;
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