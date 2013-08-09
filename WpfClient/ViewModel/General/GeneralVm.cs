using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using DataRepository.DataAccess.GenericRepository;
using DataRepository.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
                signals.AddRange(_databaseService.GetAnalogSignals(1).Select(s => s.Property));
                
                Application.Current.Dispatcher.BeginInvoke(new Action(() => signals.ForEach(s => _signalNames.Add(s))));
            });
        }

        private void updateFanValues()
        {
            var parametersList = new List<List<Parameter>>();

            for (int i = 1; i <= _fans.Count; i++)
            {
                var fanObjectVm = _databaseService.GetFanObject(i);
                var parameters = new List<Parameter>();
                parameters.Add(new Parameter {
                    Value = fanObjectVm.WorkingFanNumber == 0 ? "ОСТАНОВ" : string.Format("№{0}",fanObjectVm.WorkingFanNumber), 
                                             ParameterState = Parameter.ValueState.None});

                parameters.Add(new Parameter { Value = _fanService.GetFanMode(fanObjectVm.WorkingFanNumber, fanObjectVm.Doors), ParameterState = Parameter.ValueState.None });
                parameters.AddRange(fanObjectVm.Parameters);
                
                parametersList.Add(parameters);
            }
            
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                for (int i = 0; i < parametersList.Count; i++) _fans[i].Values = parametersList[i];
            }));
        }
    }
}
