using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLTcpServer.Interfaces;
using DataRepository.DataAccess;
using DataRepository.DataAccess.UnitOfWork;
using DataRepository.Models;
using WpfClient.Model.Abstract;

namespace WpfClient.Model
{
    public class RemoteExchange
    {
        private readonly IRemoteExchange _remoteExchange;
        private readonly IDataService _dataService;

        public RemoteExchange(IRemoteExchange remoteExchange, IDataService dataService) {
            _remoteExchange = remoteExchange;
            initServer();
            _dataService = dataService;
        }

        private void initServer()
        {
            _remoteExchange.StartServer("15000");
            _remoteExchange.ReceiveEvent += onRecieve;
        }


        private void onRecieve(string msg, int clientNum)
        {
            _dataService.InsertRawData(msg);

            _remoteExchange.StopServer();
        }
    }
}
