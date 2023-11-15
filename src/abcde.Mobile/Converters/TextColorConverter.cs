using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abcde.Mobile.Converters
{
    public class TextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return (Color)Application.Current.Resources["Primary"];

            var SelectedValue = value.ToString();
            var Value = parameter.ToString();

            return SelectedValue == Value ?  Colors.White: (Color)Application.Current.Resources["Primary"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
