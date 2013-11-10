using Ninject;
using Ninject.Modules;

namespace UTorrentPostDownloadScript
{
    public class Bindings : INinjectModule
    {
        public IKernel Kernel { get; private set; }
        public string Name { get; private set; }
        
        public void OnLoad(IKernel kernel)
        {
        }

        public void OnUnload(IKernel kernel)
        {
        }

        public void OnVerifyRequiredModules()
        {
        }
    }
}