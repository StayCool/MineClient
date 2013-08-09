using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DataRepository.DataAccess.GenericRepository;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ninject.Parameters;
using WpfClient.Model;
using WpfClient.Model.Concrete;
using WpfClient.Services;
using WpfClient.ViewModel.FanObject;
using WpfClient.ViewModel.Plot;
using Parameter = WpfClient.Model.Entities.Parameter;

namespace WpfClient.ViewModel.General
{
    public class FanVm : ViewModelBase
    {
        private List<Parameter> _parameters;
        private RelayCommand<object> _paramClickCommand;
        private RelayCommand<object> _fanClickCommand;

        public FanVm(int fanObjectId)
        {
            FanObjectId = fanObjectId;
        }

        public int FanObjectId { get; private set; }

        public string FanName
        {
            get { return string.Format("¬≈Õ“»Àﬂ“Œ– π{0}", FanObjectId); }
        }

        public List<Parameter> Values
        {
            get { return _parameters ?? (_parameters = new List<Parameter>()); }
            set
            {
                if (value != null) _parameters = value;
                RaisePropertyChanged("Values");
            }
        }

        public ICommand ParameterClick 
        {
            get { return _paramClickCommand ?? (_paramClickCommand = new RelayCommand<object>(OnParamClick)); }
        }

        public ICommand FanObjectClick 
        {
            get { return _fanClickCommand ?? (_fanClickCommand = new RelayCommand<object>(OnFanObjetClick)); }
        }

        public void OnFanObjetClick(object t)
        {
            IoC.Resolve<MainVm>().CurrentView = IoC.Resolve<FanObjectVm>(new ConstructorArgument("fanObjectId", FanObjectId));
        }

        private void OnParamClick(object t)
        {
            var analogParametersVm = new AnalogParametersVm();
            IoC.Resolve<MainVm>().CurrentView = analogParametersVm;
            analogParametersVm.ShowSignal(FanObjectId, (int)t);
        }
    }
}
