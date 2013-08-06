using CLTcpServer;
using CLTcpServer.Interfaces;
using MineClient.Rules;
using WpfClient.Model.Abstract;
using WpfClient.Model.Concrete;
using WpfClient.ViewModel;

namespace WpfClient.Services
{
    public class DiService 
    {
        public static void SetBindings()
        {
            IoC.RegisterType<IMsgParser, MsgParser>();
            IoC.RegisterType<IRemoteExchange, TcpServer>();
            IoC.RegisterSingleton<DatabaseService, DatabaseService>();
            IoC.RegisterSingleton<MainVm, MainVm>();
            IoC.RegisterSingleton<SettingsVm, SettingsVm>();
            IoC.RegisterInstance<RemoteService, RemoteService>();
            IoC.RegisterInstance<GeneralService, GeneralService>();
            IoC.RegisterType<IConfig, MineConfig>();
        }
    }
}
