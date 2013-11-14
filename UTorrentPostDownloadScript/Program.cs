using System.Linq;
using log4net.Config;
using Ninject;

namespace UTorrentPostDownloadScript
{
    public class Program
    {
        public static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            var kernel = new StandardKernel(new Bindings());
            kernel.Get<TorrentDownloadedAction>().Execute(args);
        }
    }
}