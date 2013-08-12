using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using WpfClient.Model;
using WpfClient.Model.Concrete;
using WpfClient.Model.Entities;
using WpfClient.Services;

namespace WpfClient.ViewModel.General
{
    public class GeneralVm : ViewModelBase
    {
        private List<FanVm> _fans;
        private ObservableCollection<string> _signalNames;

        private readonly DatabaseService _databaseService;
        private readonly FanService _fanService;

        public GeneralVm(DatabaseService databaseService, FanService fanService)
        {
            _databaseService = databaseService;
            _fanService = fanService;

            AsyncProvider.StartTimer(2000, updateFanValues);
        }

        public ObservableCollection<string> Signals 
        {
            get
            {
                if (_signalNames == null) setParameterNames();
                return _signalNames;
            }
        }

        public List<FanVm> Fans
        {
            get
            {
                if (_fans == null)
                {
                    _fans = new List<FanVm>();
                    for (int i = 1; i <= Config.Instance.FansCount; i++)
                    {
                        _fans.Add(new FanVm(i));
                    }
                }
                return _fans;
            }
        }

        private void setParameterNames()
        {
            _signalNames = new ObservableCollection<string>();

            Task.Run(() =>
            {
                var signals = new List<string> {"Вентилятор в работе", "Состояние вентилятора"};
                signals.AddRange(_databaseService.GetAnalogSignals(1).Select(s => s.Name));
                
                Application.Current.Dispatcher.BeginInvoke(new Action(() => signals.ForEach(s => _signalNames.Add(s))));
            });
        }

        private void updateFanValues()
        {
            var parametersList = new List<List<ParameterVm>>();

            for (int i = 1; i <= _fans.Count; i++)
            {
                var fanObject = _databaseService.GetFanObject(i);
                var parameters = new List<ParameterVm>();
                
                parameters.Add(getFanNumberParameter(fanObject));
                parameters.Add(getFanStateParameter(fanObject));
                fanObject.Parameters.ForEach(p => parameters.Add(new ParameterVm(p)));
                
                parametersList.Add(parameters);
            }
            
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                for (var i = 0; i < parametersList.Count; i++) _fans[i].Values = parametersList[i];
            }));
        }

        private ParameterVm getFanStateParameter(FanObject fanObject)
        {
            return _fanService.GetFanMode(fanObject.WorkingFanNumber, fanObject.Doors);
        }

        private ParameterVm getFanNumberParameter(FanObject fanObject)
        {
            var parameter = new ParameterVm {Value = fanObject.WorkingFanNumber == 0 ? "АВАРИЯ" : string.Format("№{0}", fanObject.WorkingFanNumber)};
            parameter.State = SystemStateService.GetFanState(fanObject.WorkingFanNumber);

            return parameter;
        }
    }
}
