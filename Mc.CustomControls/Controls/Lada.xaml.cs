using System.Windows;
using System.Windows.Controls;
using Mc.CustomControls.Model;

namespace Mc.CustomControls.Controls
{
    /// <summary>
    /// Interaction logic for Lada.xaml
    /// </summary>
    public partial class Lada : UserControl
    {
        public static readonly DependencyProperty StateProperty =
                    DependencyProperty.Register("State", typeof(DoorStateEnum), typeof(Lada),
                    new PropertyMetadata(DoorStateEnum.Close, OnStartPropertyChanged));

        private static void OnStartPropertyChanged(DependencyObject dependencyObject,
                                                   DependencyPropertyChangedEventArgs e) {
            var lada = ((Lada)dependencyObject);
            if (lada.State == DoorStateEnum.Open) {
                lada.Close.Visibility = Visibility.Hidden;
                lada.Open.Visibility = Visibility.Visible;
            } else if (lada.State == DoorStateEnum.Close) {
                lada.Open.Visibility = Visibility.Hidden;
                lada.Close.Visibility = Visibility.Visible;
            } else if (lada.State == DoorStateEnum.Undefined) {
                lada.Open.Visibility = Visibility.Visible;
                lada.Close.Visibility = Visibility.Visible;
            }
        }
        private DoorStateEnum _state;
        public DoorStateEnum State {
            get {
                return (DoorStateEnum)GetValue(StateProperty);
            }
            set {
                SetValue(StateProperty, value);
            }
        }
        public Lada() {
            InitializeComponent();
            Open.Visibility = Visibility.Hidden;
            Close.Visibility = Visibility.Visible;
        }
    }
}
