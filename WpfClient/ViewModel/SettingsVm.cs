using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfClient.ViewModel
{
    public class SettingsVm
    {
        public ObservableCollection<int> Lines { get; set; }

        public SettingsVm() {
            Lines = new ObservableCollection<int> { 3, 4, 5 };
        }

    }
}
