using CLTcpServer.Interfaces;
using WpfClient.Model;

namespace WpfClient.Services
{
    public class RemoteService
    {
        private readonly IRemoteExchange _remoteExchange;
        private readonly DatabaseService _dataService;

        public RemoteService(IRemoteExchange remoteExchange, DatabaseService dataService) {
            _remoteExchange = remoteExchange;
            initServer();
            _dataService = dataService;
        }

        private void initServer()
        {
            _remoteExchange.StartServer("15000");
            _remoteExchange.ReceiveEvent += onRecieve;
        }


        private void onRecieve(string msg)
        {
            _dataService.InsertRawData(msg);
        }
    }
}
