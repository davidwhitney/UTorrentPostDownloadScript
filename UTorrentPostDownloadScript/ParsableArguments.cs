﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace UTorrentPostDownloadScript
{
    public abstract class ParsableArguments<T> : Dictionary<string, Action<T, string>> where T : class, new()
    {
        public T Parse(string[] args) 
        {
            var cliParams = BuildDictionaryOfInputParams(args);

            var argumentss = new T();
            foreach (var commandLineKey in this)
            {
                var rawValue = ValueOrDefault<string>(cliParams, "-" + commandLineKey.Key);
                if (rawValue == null) continue;

                commandLineKey.Value(argumentss, rawValue);
            }

            return argumentss;
        }

        public string GetHelp()
        {
            var sb = new StringBuilder();

            sb.AppendLine("You need to configure UTorrentPostDownload script - add the following as the post torrent hook: " + Environment.NewLine);
            sb.Append(Assembly.GetCallingAssembly().Location);
            foreach (var item in this)
            {
                sb.Append(" -" + item.Key + " %" + item.Key.ToUpper() + " ");
            }

            return sb.ToString().Trim();
        }

        protected static void ToEnum<T>(string value, Action<T> action) where T : struct
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

        private static T2 ValueOrDefault<T2>(IReadOnlyDictionary<string, string> src, string key)
        {
            try
            {
                string value;
                return src.TryGetValue(key, out value)
                    ? (T2)Convert.ChangeType(value, typeof(T2))
                    : default(T2);
            }
            catch
            {
                return default(T2);
            }
        }
    }
}