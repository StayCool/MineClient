using System;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WpfClient.Model;
using WpfClient.View;

namespace WpfClient.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private object _currentView;

        
        public MainViewModel()
        {
            //var remoteExchange = DiService.Get<RemoteExchange>();
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
            string menuStr = t as string;

            if (menuStr.Equals("FanParams", StringComparison.InvariantCulture))
                CurrentView = new ParametersVm();
        }


    }
}