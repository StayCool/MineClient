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
        private readonly double _maxPillowValue;

        public ObservableCollection<double> Levels { get; set; }
        public ObservableCollection<double> Values { get; set; }

        public IndicatorVm()
        {
            _maxIndicatorValue = Config.Instance.MaxIndicatorValue;
            _maxPillowValue = Config.Instance.MaxPillowValue;

            initialize();
        }

        public void Update(List<Parameter> indicatorValues)
        {
            for (var i = 0; i < indicatorValues.Count && i < _indicatorCount; i++)
            {
                Values[i] = indicatorValues[i].Value;
            }
            //Levels[0] = indicatorValues[0].Value / _maxIndicatorValue;
            Levels[0] = indicatorValues[0].Value / _maxPillowValue;
        }

        private void initialize()
        {
            Levels = new ObservableCollection<double>();
            Values = new ObservableCollection<double>();
            
            for (var i = 0; i < _indicatorCount; i++)
            {
                Levels.Add(0);
                Values.Add(0);
            }
        }
    }
}
