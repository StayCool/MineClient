using System;
using CLTcpServer.Interfaces;
using WpfClient.Model.Abstract;
using WpfClient.Services;

namespace WpfClient.Model.Concrete
{
    public class TcpRemoteListener : IRemoteListener
    {
        private readonly IRemoteExchange _remoteExchange;

        public TcpRemoteListener(IRemoteExchange remoteExchange, IDataInserter dataInserter) {
            _remoteExchange = remoteExchange;
            _remoteExchange.ReceiveEvent += RemoteService.onRecieve;
            SetDataInserter(dataInserter);
        }

        public void InitServer(string port)
        {
            _remoteExchange.StartServer(port);
        }

        public void SetDataInserter(IDataInserter dataInserter)
        {
            if (dataInserter == null) throw new ArgumentNullException("dataInserter");
            _remoteExchange.ReceiveEvent += dataInserter.InsertData;
        }

        public void RemoveDataInserter(IDataInserter dataInserter)
        {
            if (dataInserter == null) throw new ArgumentNullException("dataInserter");
            _remoteExchange.ReceiveEvent -= dataInserter.InsertData;
        }
    }
}
