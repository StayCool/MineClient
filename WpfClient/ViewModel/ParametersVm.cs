using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using WpfClient.Services;
using WpfClient.ViewModel.General;

namespace WpfClient.ViewModel
{
    class ParametersVm : ViewModelBase
    {
        public ObservableCollection<FanVm> Fans { get; set; }
        public ObservableCollection<PropertyValueVm> PropertyValues { get; set; }

        private int _selectedIndex;
        public int SelectedIndex {
            set
            {
                _selectedIndex = value;
                new Thread(updatePropertuValues).Start();
            }
            get { return _selectedIndex; }
        }

        public ParametersVm()
        {
            Fans = new ObservableCollection<FanVm>();
            PropertyValues = new ObservableCollection<PropertyValueVm>();
            SelectedIndex = _selectedIndex;

            Fans.Add(new FanVm {FanName = "Вентилятор 1"});
            Fans.Add(new FanVm {FanName = "Вентилятор 2"});

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0,0,10);
            dispatcherTimer.Tick += (sender, e) => updatePropertuValues();
            dispatcherTimer.Start();
        }

        private void updatePropertuValues()
        {
            var databaseService = IoC.Resolve<DatabaseService>();
            var propertyList = databaseService.GetLastParameters(SelectedIndex + 1);

            Application.Current.Dispatcher.Invoke(() =>
            {
                PropertyValues.Clear();
                propertyList.ForEach(n => PropertyValues.Add(n));
            });
        }
    }
}
