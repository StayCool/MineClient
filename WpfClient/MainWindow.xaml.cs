using System.Windows;
using WpfClient.Services;
using WpfClient.ViewModel;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() {
            InitializeComponent();

            DiService.SetBindings();
            DataContext = IoC.Resolve<MainVm>();
        }
    }
}
