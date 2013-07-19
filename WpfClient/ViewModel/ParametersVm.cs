using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using GalaSoft.MvvmLight;

namespace WpfClient.ViewModel
{
    class ParametersVm : ViewModelBase
    {
        public ObservableCollection<FanViewModel> Fans { get; set; }

        private ListView _listView;

        private IList<FanProperty> FanPropertys { get; set; } 

        public ParametersVm()
        {
            FanPropertys = new List<FanProperty>();

            ListViewInit();
            Fans = new ObservableCollection<FanViewModel>();
            Fans.Add(new FanViewModel
            {
                FanName = "Fan1",
                PropertyValues = new List<PropertyValue>() { new PropertyValue { Property = "Temperature", Value = 60 }, new PropertyValue { Property = "Scan", Value = 60 } }
            });
            Fans.Add(new FanViewModel {
                FanName = "Fan1",
                PropertyValues = new List<PropertyValue>() { new PropertyValue { Property = "Temperature", Value = 60 }, new PropertyValue { Property = "Scan", Value = 70 } }
            });

        }

        private GridView GridViewInit()
        {
            var gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn() { Header = "Name", DisplayMemberBinding = new Binding("Name") });
            gridView.Columns.Add(new GridViewColumn() { Header = "Value", DisplayMemberBinding = new Binding("Value") });
            return gridView;
        }
 
        private void ListViewInit()
        {
            _listView = new ListView();
            _listView.View = GridViewInit();
            _listView.ItemsSource = FanPropertys;
        }
    }




    class FanProperty : ViewModelBase
    {
        public string MyName { get; set; }
        public string Value { get; set; }
    }
}
