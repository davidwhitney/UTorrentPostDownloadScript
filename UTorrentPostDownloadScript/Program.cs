using System;
using System.Collections.Generic;

namespace UTorrentPostDownloadScript
{
    public class Program
    {
        static void Main(string[] args)
        {
            var utorrentArgs = CommandLineArgsInterpreter.Parse(args);
        }
    }

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
                var rawValue = cliParams.ValueOrDefault<string>("-" + commandLineKey.Key);
                if (rawValue == null) continue;
                
                commandLineKey.Value(rawValue);
            }

            return arguments;
        }

        public static void ToEnum<T>(string value, Action<T> action) where T : struct
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
    }

    public static class DictionaryExtensions
    {
        public static T ValueOrDefault<T>(this Dictionary<string, string> src, string key)
        {
            try
            {
                string value;
                return src.TryGetValue(key, out value)
                    ? (T) Convert.ChangeType(value, typeof (T))
                    : default(T);
            }
            catch
            {
                return default(T);
            }
        }
    }

    public class Arguments
    {
        /// <summary>%F</summary>
        public string NameOfDownloadedFileForSingleFileTorrents { get; set; }
        
        /// <summary>%D</summary>
        public string DirectoryWhereFilesAreSaved { get; set; }
        
        /// <summary>%N</summary>
        public string TitleOfTorrent { get; set; }
        
        /// <summary>%P</summary>
        public StateOfTorrent PreviousStateOfTorrent { get; set; }
        
        /// <summary>%L</summary>
        public string Label { get; set; }
        
        /// <summary>%T</summary>
        public string Tracker { get; set; }
        
        /// <summary>%M</summary>
        public string StatusMessage { get; set; }
        
        /// <summary>%I</summary>
        public string HexEndocdedInfoHash { get; set; }
        
        /// <summary>%S</summary>
        public StateOfTorrent StateOfTorrent { get; set; }
        
        /// <summary>%K</summary>
        public KindOfTorrent KindOfTorrent { get; set; }
    }

    public enum KindOfTorrent
    {
        Single,
        Multi
    }

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
