using System;
using System.Data.Entity;
using System.Windows.Input;
using DataRepository.DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WpfClient.Model;
using WpfClient.Services;
using WpfClient.ViewModel.General;

namespace WpfClient.ViewModel
{
    public class MainVm : ViewModelBase
    {
        private object _currentView;

        
        public MainVm()
        {
            Database.SetInitializer(new MineDbInitializer());
            var remoteExchange = IoC.Resolve<RemoteService>();
            CurrentView = IoC.Resolve<GeneralVm>();
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
        public ICommand MenuClick {
            get { return _viewMenuCommand ?? (_viewMenuCommand = new RelayCommand<object>(MenuViewControl)); }
        }

        private void MenuViewControl(object t) 
        {
            var menuStr = t as string;

            if (menuStr.Equals("FanParams", StringComparison.InvariantCulture))
                CurrentView = new ParametersVm();
            if (menuStr.Equals("GeneralView", StringComparison.InvariantCulture))
                CurrentView = IoC.Resolve<GeneralVm>();
            if (menuStr.Equals("SettingsView", StringComparison.InvariantCulture))
                CurrentView = IoC.Resolve<SettingsVm>();
        }
    }
}