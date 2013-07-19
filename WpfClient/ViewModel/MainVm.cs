using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WpfClient.Model;
using WpfClient.Services;

namespace WpfClient.ViewModel
{
    public class MainVm : ViewModelBase
    {
        private object _currentView;

        
        public MainVm()
        {
            var remoteExchange = DiService.Get<RemoteService>();
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                RaisePropertyChanged("CurrentView");
            }
        }

        public string Str { get; set; }

        private RelayCommand<object> _viewMenuCommand;
        public ICommand MenuClick
        {
            get { return _viewMenuCommand ?? (_viewMenuCommand = new RelayCommand<object>(MenuViewControl)); }
        }

        private void MenuViewControl(object t)
        {
            var menuStr = t as string;

            if (menuStr.Equals("FanParams", StringComparison.InvariantCulture))
                CurrentView = new ParametersVm();
        }


    }
}