using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DataRepository.DataAccess.GenericRepository;
using GalaSoft.MvvmLight.Command;
using WpfClient.Services;
using WpfClient.ViewModel.Plot;

namespace WpfClient.ViewModel.General
{
    public class FanVm
    {
        private RelayCommand<object> _paramClickCommand;

        public string FanName { get; set; }

        public List<SignalVm> Values { get; set; }

        public ICommand MenuClick {
            get { return _paramClickCommand ?? (_paramClickCommand = new RelayCommand<object>(onParamClick)); }
        }


        private void onParamClick(object t)
        {
            var analogParametersVm = new AnalogParametersVm();
            IoC.Resolve<MainVm>().CurrentView = analogParametersVm;
            analogParametersVm.ShowSignal(int.Parse(FanName.Split('¹')[1]), (int) t);

        }
    }
}
