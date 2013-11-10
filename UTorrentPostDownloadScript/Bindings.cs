using Ninject;
using Ninject.Modules;
using Ninject.Extensions.Conventions;

namespace UTorrentPostDownloadScript
{
    public class Bindings : INinjectModule
    {
        public IKernel Kernel { get; private set; }
        public string Name { get { return "Default"; } }
        
        public void OnLoad(IKernel kernel)
        {
            kernel.Bind(x => x.FromThisAssembly().SelectAllClasses().BindDefaultInterfaces());
            kernel.Bind(x => x.FromAssemblyContaining<System.Configuration.Abstractions.IAppSettings>().SelectAllClasses().BindDefaultInterfaces());
            kernel.Bind(x => x.FromAssemblyContaining<System.IO.Abstractions.FileSystem>().SelectAllClasses().BindDefaultInterfaces());
        }

        public void OnUnload(IKernel kernel)
        {
        }

        public void OnVerifyRequiredModules()
        {
        }
    }
}