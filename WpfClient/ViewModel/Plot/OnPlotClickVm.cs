using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WpfClient.Model;
using WpfClient.Services;

namespace WpfClient.ViewModel.Plot
{
    class OnPlotClickVm:ViewModelBase
    {
        public ObservableCollection<PropertyValueVm> ListCollection { get; set; }

        private AnalogParametersVm _prevView;

        private RelayCommand _backArrowClickCommand;
        public ICommand BackArrowClick
        {
            get { return _backArrowClickCommand ?? (_backArrowClickCommand = new RelayCommand(BackArrowClickHandler)); }
        }

        private void BackArrowClickHandler()
        {
            IoC.Resolve<MainVm>().CurrentView = _prevView;
        }

        public OnPlotClickVm(DateTime date, int fanObjectId, AnalogParametersVm prevView)
        {
            _prevView = prevView;
            ListCollection = new ObservableCollection<PropertyValueVm>();
            UpdatePropertyValue(date, fanObjectId);
        }

        private void UpdatePropertyValue(DateTime date, int fanObjectId)
        {
            var databaseService = IoC.Resolve<DatabaseService>();
            var propertyList = databaseService.FindParameterByIdAndDate(fanObjectId, date);

            if (propertyList.Count > 0)
            {

                            ListCollection.Clear();
                            propertyList.ForEach(n => ListCollection.Add(n));
            }
        }
    }

}
