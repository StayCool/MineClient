using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WpfClient.View
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public ObservableCollection<int> Lines { get; set; }
        private readonly int _lineCount = 9;
        public SettingsView()
        {
            Lines = new ObservableCollection<int>();
            for (int i = _lineCount-1; i >= 0; i-- )
                Lines.Add(i*15);

                InitializeComponent();
        }
    }
}
