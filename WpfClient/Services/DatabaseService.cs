using System;
using System.Collections.Generic;
using System.Linq;
using DataRepository.DataAccess.GenericRepository;
using WpfClient.Model.Entities;
using WpfClient.ViewModel.Plot;

namespace WpfClient.Services
{
    public class DatabaseService
    {
        public List<Parameter> GetAnalogSignals(int fanObjectNum)
        {
            var parameters = new List<Parameter>();

            using (var unit = new RepoUnit())
            {
                if (isFanLogEmpty(unit, fanObjectNum)) return null;
                var analogSignals = unit.FanLog.LastRecord(f => f.FanNumber == fanObjectNum).AnalogSignalLogs.ToList();
                parameters.AddRange(analogSignals.Select(signal => new Parameter {Name = signal.SignalType.Type, Value = signal.SignalValue}));
            }

            return parameters;
        }

        public List<string> GetAnalogSignalNames()
        {
            var names = new List<string>();

            using (var unit = new RepoUnit())
            {
                names.AddRange(unit.AnalogSignal.Load().Select(s => s.Type).ToList());
            }
            return names;
        }

        public FanObject GetFanObject(int fanOjbectNum)
        {
            var fanObjectVm = new FanObject {FanObjectId = fanOjbectNum};

            using (var unit = new RepoUnit())
            {
                if (isFanLogEmpty(unit, fanOjbectNum)) return null;

                var fanLog = unit.FanLog.LastRecord(f => f.FanNumber == fanOjbectNum);

                fanObjectVm.Parameters.AddRange(
                    fanLog.AnalogSignalLogs.Select(
                        s => new Parameter { Name = s.SignalType.Type, Value = s.SignalValue, State = SystemStateService.GetParameterState(s.SignalValue) }));

                fanObjectVm.Doors.AddRange(fanLog.DoorsLogs.Select(d => new Door {Type = d.DoorType.Type, State = d.DoorState.State, StateId = d.DoorStateId, TypeId = d.DoorTypeId}));
                fanObjectVm.WorkingFanNumber = GetWorkingFanNumber(fanLog.Fan1State_Id, fanLog.Fan2State_Id);
                fanObjectVm.Date = fanLog.Date;
            }
            return fanObjectVm;
        }

        private bool isFanLogEmpty(RepoUnit unit, int fanObjectNum)
        {
            return unit == null || !unit.FanLog.Load(f => f.FanNumber == fanObjectNum).Any();
        }

        private int GetWorkingFanNumber(int? fan1State, int? fan2State)
        {
            return fan1State == fan2State ? 0 : fan1State == 2 ? 1 : 2;
        } 


        public List<PropertyValueVm> FindParameterByIdAndDate(int fanNum, DateTime date)
        {
            var propertyList = new List<PropertyValueVm>();

            try
            {
                using (var repoUnit = new RepoUnit())
                {
                    var fansLogRepo = repoUnit.FanLog;

                    var fansLogId = fansLogRepo.Load().Where(n => n.FanNumber == fanNum && n.Date == date).Max(n => n.Id);
                    var fansLog = fansLogRepo.Find(fansLogId);

                    propertyList.Add(new PropertyValueVm { Property = "Время приема параметров", Value = fansLog.Date.ToString() });
                    propertyList.Add(new PropertyValueVm { Property = "Вентилятор 1", Value = fansLog.Fan1State.State });
                    propertyList.Add(new PropertyValueVm { Property = "Вентилятор 2", Value = fansLog.Fan2State.State });

                    propertyList.AddRange(fansLog.DoorsLogs.Select(doorLog => new PropertyValueVm { Property = doorLog.DoorType.Type, Value = doorLog.DoorState.State }));
                    propertyList.AddRange(fansLog.AnalogSignalLogs.Select(signal => new PropertyValueVm { Property = signal.SignalType.Type, Value = signal.SignalValue.ToString() }));
                }
            }
            catch (Exception)
            {
                //nothing in Db
            }
            return propertyList;
        }

        public List<List<PropertyValueVm>> HistoryFind(int fanNum, DateTime dateFrom, DateTime dateTill)
        {
            var propertyList = new List<List<PropertyValueVm>>();

            try
            {
                using (var repoUnit = new RepoUnit())
                {
                    var fansLogRepo = repoUnit.FanLog;
                    var fansLogId = fansLogRepo.Load().Where(n => n.FanNumber == fanNum && n.Date >= dateFrom && n.Date <= dateTill).Select(n => n.Id);
                    int i = 0;
                    foreach (var fanLogId in fansLogId.ToArray())
                    {
                        var fansLog = fansLogRepo.Find(fanLogId);
                        propertyList.Add(new List<PropertyValueVm>());
                        propertyList[i].Add(new PropertyValueVm { Property = "Время приема параметров", Value = fansLog.Date.ToString() });
                        propertyList[i].Add(new PropertyValueVm { Property = "Вентилятор 1", Value = fansLog.Fan1State.State });
                        propertyList[i].Add(new PropertyValueVm { Property = "Вентилятор 2", Value = fansLog.Fan2State.State });

                        propertyList[i].AddRange(fansLog.DoorsLogs.Select(doorLog => new PropertyValueVm { Property = doorLog.DoorType.Type, Value = doorLog.DoorState.State }));
                        propertyList[i].AddRange(fansLog.AnalogSignalLogs.Select(signal => new PropertyValueVm { Property = signal.SignalType.Type, Value = signal.SignalValue.ToString() }));
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                //nothing in Db
            }
            return propertyList;
        }
    }
}
