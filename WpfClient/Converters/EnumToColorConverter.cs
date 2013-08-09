using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using WpfClient.Model.Entities;
using WpfClient.ViewModel;
using WpfClient.ViewModel.General;

namespace WpfClient.Converters
{
    public class EnumToColorConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (Parameter.ValueState) value;
            switch (v)
            {
                case Parameter.ValueState.Ok:
                    return new SolidColorBrush(Colors.LightGreen);
                case Parameter.ValueState.Bad:
                    return new SolidColorBrush(Colors.Yellow);
                case Parameter.ValueState.Dangerous:
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
