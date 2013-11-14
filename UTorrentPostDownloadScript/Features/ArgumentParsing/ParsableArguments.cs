using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace UTorrentPostDownloadScript.Features.ArgumentParsing
{
    public class ParsableArguments<T> : Dictionary<string, Action<T, string>>, IParsableArguments<T> where T : class, new()
    {
        public T Parse(string[] args) 
        {
            var cliParams = BuildDictionaryOfInputParams(args);

            var argumentss = new T();
            foreach (var commandLineKey in this)
            {
                var rawValue = ValueOrDefault<string>(cliParams, "-" + commandLineKey.Key);

                if (rawValue == null)
                {
                    continue;
                }

                if (rawValue.StartsWith("\"") || rawValue.StartsWith("'"))
                {
                    rawValue = rawValue.Substring(1, rawValue.Length - 1);
                }

                if (rawValue.EndsWith("\"") || rawValue.EndsWith("'"))
                {
                    rawValue = rawValue.Substring(0, rawValue.Length - 1);
                }

                commandLineKey.Value(argumentss, rawValue);
            }

            return argumentss;
        }

        public virtual string GetHelp()
        {
            var sb = new StringBuilder();

            sb.AppendLine("You need to configure UTorrentPostDownload script - add the following as the post torrent hook: " + Environment.NewLine);
            sb.Append(Assembly.GetCallingAssembly().Location);
            foreach (var item in this)
            {
                sb.Append(" -" + item.Key + " '%" + item.Key.ToUpper() + "'");
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

        private static Dictionary<string, string> BuildDictionaryOfInputParams(string[] originalArgs)
        {
            var args = FoldQuotedParamsTogether(originalArgs).ToArray();

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
                    if (parameters.ContainsKey(lastKey))
                    {
                        throw new InvalidOperationException("Cannot add key '" + lastKey + "' with value '" + item + "' - it already exists in the parameter map.");
                    }

                    parameters.Add(lastKey, item);
                }
            }
            return parameters;
        }

        private static List<string> FoldQuotedParamsTogether(string[] args)
        {
            var compressedArgs = new List<string>();
            var capturing = false;
            var capturedValue = string.Empty;
            foreach (var item in args)
            {
                if (item.StartsWith("\"") || item.StartsWith("'"))
                {
                    capturing = true;
                }

                if (!capturing)
                {
                    compressedArgs.Add(item);
                    continue;
                }

                capturedValue = capturedValue + " " + item;


                if (item.EndsWith("\"") || item.EndsWith("'"))
                {
                    capturing = false;
                }

                if (!capturing)
                {
                    compressedArgs.Add(capturedValue.Trim());
                    capturedValue = string.Empty;
                }
            }

            return compressedArgs;
        }

        private static T2 ValueOrDefault<T2>(IReadOnlyDictionary<string, string> src, string key)
        {
            string value;
            return src.TryGetValue(key, out value)
                ? (T2) Convert.ChangeType(value, typeof (T2))
                : default(T2);
        }
    }
}