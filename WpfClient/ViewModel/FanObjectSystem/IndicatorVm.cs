using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfClient.Model.Concrete;
using WpfClient.Model.Entities;

namespace WpfClient.ViewModel.FanObjectSystem
{
    class IndicatorVm
    {
        private readonly int _indicatorCount = 6;
        private readonly double _maxIndicatorValue;

        public ObservableCollection<double> Levels { get; set; }
        public ObservableCollection<double> Values { get; set; }

        public IndicatorVm()
        {
            _maxIndicatorValue = Config.Instance.MaxIndicatorValue;
            initialize();
        }

        public void Update(List<Parameter> indicatorValues)
        {
            for (var i = 0; i < _indicatorCount; i++)
            {
                Levels[i] = _maxIndicatorValue / indicatorValues[i].Value;
            }
        }

        private void initialize()
        {
            Levels = new ObservableCollection<double>();
            Values = new ObservableCollection<double>();
            
            for (var i = 0; i < _indicatorCount; i++)
            {
                Levels.Add(0);
                Values.Add(150);
            }
        }
    }
}
