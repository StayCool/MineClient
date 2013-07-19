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
        mb100 = 1, mb107, mb100_107,
        mb101, mb108, mb101_108,
        mb105, mb112, mb105_112,
        mb106, mb113, mb106_113,
        mb102, mb109, mb102_109,
        mb103, mb110, mb103_110,
        mb104, mb111, mb104_111,
        mb114, _mb114,
        mb115, _mb115,
        
        mb11, mb12, mi2, mi4, mi6, fn
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
                var ff = MapToFanLog(paramsTable);
                repoUnit.FansLogRepo.Save(MapToFanLog(paramsTable));
                repoUnit.Commit();   
            }
        }

        public RepoUnit RepoUnit { get { return new RepoUnit();}}

        public DoorsLog MapToDoorsLog(IDictionary<DoorEnum, int> paramsTable) {
            return new DoorsLog {
                Door0_Id = (int)(paramsTable[DoorEnum.mb100] == paramsTable[DoorEnum.mb107] ? DoorEnum.mb100_107 : 
                            paramsTable[DoorEnum.mb100] == 1 ? DoorEnum.mb100 : DoorEnum.mb107),

                Door1_Id = (int)(paramsTable[DoorEnum.mb101] == paramsTable[DoorEnum.mb108] ? DoorEnum.mb101_108 :
                            paramsTable[DoorEnum.mb101] == 1 ? DoorEnum.mb101 : DoorEnum.mb108),

                Door2_Id = (int)(paramsTable[DoorEnum.mb105] == paramsTable[DoorEnum.mb112] ? DoorEnum.mb105_112 :
                            paramsTable[DoorEnum.mb105] == 1 ? DoorEnum.mb105 : DoorEnum.mb112),

                Door3_Id = (int)(paramsTable[DoorEnum.mb106] == paramsTable[DoorEnum.mb113] ? DoorEnum.mb106_113 :
                            paramsTable[DoorEnum.mb106] == 1 ? DoorEnum.mb106 : DoorEnum.mb113),
                        
                Door4_Id = (int)(paramsTable[DoorEnum.mb102] == paramsTable[DoorEnum.mb109] ? DoorEnum.mb102_109 :
                            paramsTable[DoorEnum.mb102] == 1 ? DoorEnum.mb102 : DoorEnum.mb109),

                Door5_Id = (int)(paramsTable[DoorEnum.mb103] == paramsTable[DoorEnum.mb110] ? DoorEnum.mb103_110 :
                            paramsTable[DoorEnum.mb103] == 1 ? DoorEnum.mb103 : DoorEnum.mb110),

                Door6_Id = (int)(paramsTable[DoorEnum.mb104] == paramsTable[DoorEnum.mb111] ? DoorEnum.mb104_111 :
                            paramsTable[DoorEnum.mb104] == 1 ? DoorEnum.mb104 : DoorEnum.mb111),

                Door7_Id = (int)(paramsTable[DoorEnum.mb114] == 1 ? DoorEnum.mb114 : DoorEnum._mb114),

                Door8_Id = (int)(paramsTable[DoorEnum.mb115] == 1 ? DoorEnum.mb115 : DoorEnum._mb115),
            };
        }
        public AnalogSignal MapToAnalogSignal(IDictionary<DoorEnum, int> paramsTable) {
            return new AnalogSignal {
                Signal1 = paramsTable[DoorEnum.mi2],
                Signal2 = paramsTable[DoorEnum.mi4],
                Signal3 = paramsTable[DoorEnum.mi6]
            };
        }
        public FanLog MapToFanLog(IDictionary<DoorEnum, int> paramsTable) {
            return new FanLog {
                FanNumber = paramsTable[DoorEnum.fn],
                Fan1State = paramsTable[DoorEnum.mb11],
                Fan2State = paramsTable[DoorEnum.mb12],
                AnalogSignals = MapToAnalogSignal(paramsTable),
                DoorsLog = MapToDoorsLog(paramsTable),
                Date = DateTime.Now
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
                    var doorsLog = fansLog.DoorsLog;

                    propertyList.Add(new PropertyValueVm { Property = "Время приема параметров", Value = fansLog.Date.ToString() });
                    propertyList.Add(new PropertyValueVm {Property = doorsLog.Door0.DoorType, Value = doorsLog.Door0.DoorState});
                    propertyList.Add(new PropertyValueVm {Property = doorsLog.Door1.DoorType, Value = doorsLog.Door1.DoorState});
                    propertyList.Add(new PropertyValueVm {Property = doorsLog.Door2.DoorType, Value = doorsLog.Door2.DoorState});
                    propertyList.Add(new PropertyValueVm {Property = doorsLog.Door3.DoorType, Value = doorsLog.Door3.DoorState});
                    propertyList.Add(new PropertyValueVm {Property = doorsLog.Door4.DoorType, Value = doorsLog.Door4.DoorState});
                    propertyList.Add(new PropertyValueVm {Property = doorsLog.Door5.DoorType, Value = doorsLog.Door5.DoorState});
                    propertyList.Add(new PropertyValueVm {Property = doorsLog.Door6.DoorType, Value = doorsLog.Door6.DoorState});
                    propertyList.Add(new PropertyValueVm {Property = doorsLog.Door7.DoorType, Value = doorsLog.Door7.DoorState});
                    propertyList.Add(new PropertyValueVm {Property = doorsLog.Door8.DoorType, Value = doorsLog.Door8.DoorState});

                    propertyList.Add(new PropertyValueVm { Property = "Вентилятор 1", Value = fansLog.Fan1State == 1 ? "включен" : "выключен"});
                    propertyList.Add(new PropertyValueVm { Property = "Вентилятор 2", Value = fansLog.Fan2State == 1 ? "включен" : "выключен" });
                    propertyList.Add(new PropertyValueVm { Property = "Аналоговый сигнал 1", Value = fansLog.AnalogSignals.Signal1.ToString() });
                    propertyList.Add(new PropertyValueVm { Property = "Аналоговый сигнал 2", Value = fansLog.AnalogSignals.Signal2.ToString() });
                    propertyList.Add(new PropertyValueVm { Property = "Аналоговый сигнал 3", Value = fansLog.AnalogSignals.Signal3.ToString() });
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
