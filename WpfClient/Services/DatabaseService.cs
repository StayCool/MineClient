using System;
using System.Collections.Generic;
using System.Linq;
using CLTcpServer.Interfaces;
using DataRepository.DataAccess.UnitOfWork;
using DataRepository.Models;
using WpfClient.Model.Abstract;
using WpfClient.ViewModel;

namespace WpfClient.Services
{
    public enum DoorEnum
    {
        mb100 = 1, mb101, mb105,
        mb106, mb102, mb103,
        mb104, mb114, mb115,
    }

    public enum AnalogSignalEnum
    {
        mi2 = 1, mi4, mi6
    }

    public enum FanEnum
    {
        mb11, mb12, fn
    }

    public class DatabaseService
    {
        private readonly IMsgParser _msgParser;

        public event ReceiveHandler ReceiveHandler;
        

        public DatabaseService(IMsgParser msgParser)
        {
            _msgParser = msgParser;
        }

        public void InsertRawData(string message)
        {
            var paramsTable = _msgParser.Parse(message);
            
            using (var repoUnit = RepoUnit)
            {
                repoUnit.FansLogRepo.Save(MapToFanLog(paramsTable));
                repoUnit.Commit();   
            }
        }

        public RepoUnit RepoUnit { get { return new RepoUnit();}}

        public List<DoorsLog> MapToDoorsLog(IDictionary<string, int> paramsTable)
        {
            var doorsLogs = new List<DoorsLog>();
            foreach (var doorEnum in Enum.GetValues(typeof(DoorEnum)))
            {
                doorsLogs.Add(new DoorsLog { DoorTypeId = (int)doorEnum, DoorStateId = paramsTable[doorEnum.ToString()]});
            }
            return doorsLogs;
        }
        public List<AnalogSignalLog> MapToAnalogSignalLog(IDictionary<string, int> paramsTable)
        {
            var analogSignals = new List<AnalogSignalLog>(); 
            foreach (var analogEnum in Enum.GetValues(typeof(AnalogSignalEnum))) 
            {
                analogSignals.Add(new AnalogSignalLog { SignalTypeId = (int)analogEnum, SignalValue = paramsTable[analogEnum.ToString()] });
            }
            return analogSignals;
        }
        public FanLog MapToFanLog(IDictionary<string, int> paramsTable) {

            return new FanLog {
                FanNumber = paramsTable[FanEnum.fn.ToString()],
                Fan1State_Id = paramsTable[FanEnum.mb11.ToString()],
                Fan2State_Id = paramsTable[FanEnum.mb12.ToString()],
                Date = DateTime.Now,
                AnalogSignalLogs = MapToAnalogSignalLog(paramsTable),
                DoorsLogs = MapToDoorsLog(paramsTable)
            };
        }

        public List<PropertyValueVm> GetLastParameters(int fanNum)
        {
            var propertyList = new List<PropertyValueVm>();

            try
            {
                using (var repoUnit = RepoUnit) 
                {
                    var fansLogRepo = repoUnit.FansLogRepo;

                    var fansLogId = fansLogRepo.Load().Where(n => n.FanNumber == fanNum).Max(n => n.Id);
                    var fansLog = fansLogRepo.Find(fansLogId);

                    propertyList.Add(new PropertyValueVm {Property = "Время приема параметров", Value = fansLog.Date.ToString()});
                    propertyList.Add(new PropertyValueVm { Property = "Вентилятор 1", Value = fansLog.Fan1State.State });
                    propertyList.Add(new PropertyValueVm { Property = "Вентилятор 2", Value = fansLog.Fan2State.State });

                    propertyList.AddRange(fansLog.DoorsLogs.Select(doorLog => new PropertyValueVm {Property = doorLog.DoorType.Type, Value = doorLog.DoorState.State}));
                    propertyList.AddRange(fansLog.AnalogSignalLogs.Select(signal => new PropertyValueVm {Property = signal.SignalType.Type, Value = signal.SignalValue.ToString()}));
                }
            }
            catch (Exception)
            {
                //nothing in Db
            }

            return propertyList;
        }
    }
}
