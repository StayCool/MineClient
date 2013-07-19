using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLTcpServer;
using CLTcpServer.Interfaces;
using Ninject;
using WpfClient.Model.Abstract;
using WpfClient.Model.Concrete;

namespace WpfClient.Model
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
            _kernel.Bind<IDataService>().To<DatabaseService>().InSingletonScope();
            _kernel.Bind<RemoteExchange>().ToSelf();
        }

        public static TInterf Get<TInterf>()
        {
            return _kernel.Get<TInterf>();
        }
    }
}
