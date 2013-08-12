using System;
using System.Data.Entity;
using System.Windows.Input;
using DataRepository.DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WpfClient.Model;
using WpfClient.Model.Abstract;
using WpfClient.ViewModel.FanObjectSystem;
using WpfClient.ViewModel.General;

namespace WpfClient.ViewModel
{
    public class MainVm : ViewModelBase
    {
        private object _currentView;

        
        public MainVm()
        {
            Database.SetInitializer(new MineDbInitializer());
            
            IoC.Resolve<IRemoteListener>().InitServer("15000");

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

            if (menuStr.Equals("GeneralView", StringComparison.InvariantCulture))
                CurrentView = IoC.Resolve<GeneralVm>();
            if (menuStr.Equals("FanObject", StringComparison.InvariantCulture))
                CurrentView = IoC.Resolve<FanObjectVm>();
        }
    }
}