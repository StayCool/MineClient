using CLTcpServer;
using CLTcpServer.Interfaces;
using Ninject;
using WpfClient.Model;
using WpfClient.Model.Abstract;
using WpfClient.Model.Concrete;

namespace WpfClient.Services
{
    public class DiService 
    {
        private static IKernel _kernel;

        static DiService()
        {
            _kernel = new StandardKernel();
            SetBindings();
        }

        private static void SetBindings()
        {
            _kernel.Bind<IMsgParser>().To<MsgParser>();
            _kernel.Bind<IRemoteExchange>().To<TcpServer>();
            _kernel.Bind<DatabaseService>().ToSelf().InSingletonScope();
            _kernel.Bind<RemoteService>().ToSelf();
        }

        public static TInterf Get<TInterf>()
        {
            return _kernel.Get<TInterf>();
        }
    }
}
