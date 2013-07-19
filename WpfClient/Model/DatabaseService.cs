using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataRepository.DataAccess.UnitOfWork;
using DataRepository.Models;
using WpfClient.Model.Abstract;

namespace WpfClient.Model
{
    class DatabaseService : IDataService
    {
        private readonly IMsgParser _msgParser;
        
        public DatabaseService(IMsgParser msgParser)
        {
            _msgParser = msgParser;
        }

        public void InsertRawData(string message)
        {
            var paramsTable = _msgParser.Parse(message);
            var repoUnit = new RepoUnit();

            repoUnit.FansLogRepo.Save(MapToFanLog(paramsTable));
            repoUnit.Commit();
        }


        public DoorsLog MapToDoorsLog(IDictionary<string, int> paramsTable)
        {
            return new DoorsLog
            {
                DoorId1 = paramsTable["mb100"] != paramsTable["mb107"] ? paramsTable["mb100"] : (int?) null,
                DoorId2 = paramsTable["mb101"] != paramsTable["mb108"] ? paramsTable["mb101"] : (int?)null,
                DoorId3 = paramsTable["mb105"] != paramsTable["mb112"] ? paramsTable["mb105"] : (int?)null,
                DoorId4 = paramsTable["mb106"] != paramsTable["mb113"] ? paramsTable["mb106"] : (int?)null,
                DoorId5 = paramsTable["mb102"] != paramsTable["mb109"] ? paramsTable["mb102"] : (int?)null,
                DoorId6 = paramsTable["mb103"] != paramsTable["mb110"] ? paramsTable["mb103"] : (int?)null,
                DoorId7 = paramsTable["mb104"] != paramsTable["mb111"] ? paramsTable["mb104"] : (int?)null,
                DoorId8 = paramsTable["mb114"],
                DoorId9 = paramsTable["mb115"]
            };
        }

        public AnalogSignal MapToAnalogSignal(IDictionary<string, int> paramsTable)
        {
            return new AnalogSignal
            {
                Signal1 = paramsTable["mi2"],
                Signal2 = paramsTable["mi4"],
                Signal3 = paramsTable["mi6"]
            };
        }

        public FanLog MapToFanLog(IDictionary<string, int> paramsTable)
        {
            return new FanLog
            {
                FanNumber = paramsTable["fn"],
                Fan1StateId = paramsTable["mb11"],
                Fan2StateId = paramsTable["mb12"],
                AnalogSignals = MapToAnalogSignal(paramsTable),
                DoorsLog = MapToDoorsLog(paramsTable),
                Date = DateTime.Now
            };
        }
    }
}
