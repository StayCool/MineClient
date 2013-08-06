using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WpfClient.Services;

namespace WpfClient.ViewModel.General
{
    public class GeneralVm : ViewModelBase
    {
        private readonly GeneralService _generalService;

        public GeneralVm(GeneralService generalService)
        {
            _generalService = generalService;

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
            dispatcherTimer.Tick += (sender, e) => _generalService.UpdateFanValues();
            dispatcherTimer.Start();
        }

        public ObservableCollection<string> Signals 
        {
            get { return _generalService.SignalsToTrack; }
        }

        public ObservableCollection<FanVm> Fans
        {
            get { return _generalService.Fans; }
        }
    }
}
