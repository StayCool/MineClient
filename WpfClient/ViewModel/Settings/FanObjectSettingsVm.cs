using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WpfClient.Model.Concrete;

namespace WpfClient.ViewModel.Settings
{
    class FanObjectSettingsVm : ViewModelBase 
    {
        private int _fanObjectCount;

        public FanObjectSettingsVm()
        {
            initialize();
        }

         ~FanObjectSettingsVm()
        {
            Config.Instance.Save();
        }

        private void initialize()
        {
            _fanObjectCount = Config.Instance.FanObjectConfig.FanObjectCount;
            FanObjectCounts = new int[] { 1, 2, 3, 4, 5, 6 };

            PillowTemperature = new RangeValueElementVm(Config.Instance.FanObjectConfig.PillowTemperature);
            PillowVibration = new RangeValueElementVm(Config.Instance.FanObjectConfig.PillowVibration);
            Pressure = new RangeValueElementVm(Config.Instance.FanObjectConfig.Pressure);
            AirConsumption = new RangeValueElementVm(Config.Instance.FanObjectConfig.AirConsumption);
            GprsQuality = new RangeValueElementVm(Config.Instance.FanObjectConfig.GprsQuality);
        }

        

        public int FanObjectCount
        {
            get { return _fanObjectCount; }
            set
            {
                _fanObjectCount = value;
                Config.Instance.FanObjectConfig.FanObjectCount = value;
                RaisePropertyChanged("FanObjectCount");
            }
        }

        public int[] FanObjectCounts { get; set; }

        public RangeValueElementVm PillowTemperature { get; set; }

        public RangeValueElementVm PillowVibration { get; set; }

        public RangeValueElementVm Pressure { get; set; }

        public RangeValueElementVm AirConsumption { get; set; }

        public RangeValueElementVm GprsQuality { get; set; }
    }
}
