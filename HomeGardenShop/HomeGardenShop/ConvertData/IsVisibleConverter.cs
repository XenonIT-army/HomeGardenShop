using System;
using System.Globalization;
using Xamarin.Forms;

namespace HomeGardenShop.ConvertData
{
    public class IsVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool res = false;
            if (value != null)
            {
                double count = System.Convert.ToDouble(value);
                if(count > 0)
                {
                    res = true;
                }
            }
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

