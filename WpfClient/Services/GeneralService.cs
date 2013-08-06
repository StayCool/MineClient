using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataRepository.DataAccess.GenericRepository;
using DataRepository.Models;
using MineClient.Rules;
using WpfClient.ViewModel.General;

namespace WpfClient.Services
{
    public class GeneralService
    {
        private ObservableCollection<string> _signalsToTrack;
        private ObservableCollection<FanVm> _fans;

        public ObservableCollection<string> SignalsToTrack
        {
            get
            {
                if (_signalsToTrack == null)
                {
                    initSignalsType();
                }

                return _signalsToTrack;
            }
        }

        private void initSignalsType()
        {
            _signalsToTrack = new ObservableCollection<string>();

            Task.Run(() =>
            {
                using (var repoUnit = new RepoUnit())
                {
                    var signals = new List<string> {"Вентилятор в работе", "Состояние вентилятора"};
                    signals.AddRange(repoUnit.AnalogSignal.Load().Select(a => a.Type).ToList());

                    Application.Current.Dispatcher.BeginInvoke(
                        new Action(() => signals.ForEach(s => _signalsToTrack.Add(s))));
                }
            });
        }

        public ObservableCollection<FanVm> Fans
        {
            get { return _fans ?? (_fans = new ObservableCollection<FanVm>());
        }
        }

        public void UpdateFanValues()
        {
            var fanObjectCount = Config.Instance.FansCount;

            if (_fans.Count != fanObjectCount)
            {
                _fans.Clear();
                while (_fans.Count != fanObjectCount) _fans.Add(new FanVm());
            }

            using (var repoUnit = new RepoUnit())
            {        
                for (int i = 1; i <= Config.Instance.FansCount; i++) {
                    var fanLogId = repoUnit.FanLog.Load().Where(n => n.FanNumber == i).Max(n => n.Id);
                    var fansLog = repoUnit.FanLog.Find(fanLogId);

                    var fanVm = new FanVm
                    {
                        FanName = "ВЕНТИЛЯТОР №" + i.ToString(),
                        Values = new List<SignalVm>
                            {
                                new SignalVm {Value = fansLog.Fan1State_Id == 2 ? "№1" : "№2"},
                                new SignalVm {Value = getFanMode(i)}
                            }
                    };

                    foreach (var analog in fansLog.AnalogSignalLogs) {
                        fanVm.Values.Add(new SignalVm { Value = analog.SignalValue.ToString() });
                    }

                    int i1 = i;
                    Application.Current.Dispatcher.BeginInvoke(new Action(() => _fans[i1 - 1] = fanVm));
                }
            }
        }

        private string getFanMode(int fanObjectNum)
        {
            string mode;
            
            using (var repoUnit = new RepoUnit())
            {
                var fanLogId = repoUnit.FanLog.Load().Where(n => n.FanNumber == fanObjectNum).Max(n => n.Id);
                var fansLog = repoUnit.FanLog.Find(fanLogId);
                var fanNum = fansLog.Fan1State_Id == fansLog.Fan2State_Id ? 0 : fansLog.Fan1State_Id == 2 ? 1 : 2;

                mode = fanModeAnalizer(fanNum, fansLog.DoorsLogs.ToList());
            }
            return mode;
        }

        private string fanModeAnalizer(int fanNum, List<DoorsLog> doorsLogs )
        {
            var pattern = new StringBuilder();
            doorsLogs.ForEach(d => pattern.Append(d.DoorStateId));
  
            if (fanNum == 1)
            {
                return pattern.ToString() == "332233332" ? "Норма" :
                       pattern.ToString() == "332222232"? "Реверс" : "Авария";
            }
            if (fanNum == 2)
            {
                return pattern.ToString() == "223333323" ? "Норма" :
                       pattern.ToString() == "223322223" ? "Реверс" : "Авария";
            }

            throw new ArgumentException("FanNum is out of range");
        }
    }
}
