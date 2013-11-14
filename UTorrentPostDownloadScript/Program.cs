using System;
using log4net;
using log4net.Config;
using Ninject;

namespace UTorrentPostDownloadScript
{
    public class Program
    {
        public static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            try
            {
                var kernel = new StandardKernel(new Bindings());
                kernel.Get<TorrentDownloadedAction>().Execute(args);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("Log").Error(ex);
            }
        }
    }
}