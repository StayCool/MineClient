using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using WpfClient.ViewModel;
using WpfClient.ViewModel.General;

namespace WpfClient.Converters
{
    public class EnumToColorConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (SignalVm.ValueState) value;
            switch (v)
            {
                case SignalVm.ValueState.Ok:
                    return new SolidColorBrush(Colors.LightGreen);
                case SignalVm.ValueState.Bad:
                    return new SolidColorBrush(Colors.Yellow);
                case SignalVm.ValueState.Dangerous:
                    return new SolidColorBrush(Colors.OrangeRed);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
