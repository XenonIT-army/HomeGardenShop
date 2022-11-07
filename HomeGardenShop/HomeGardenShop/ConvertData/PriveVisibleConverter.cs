using System;
using System.Globalization;
using Xamarin.Forms;

namespace HomeGardenShop.ConvertData
{
	public class PriveVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool mainprice = false;
            if (parameter!= null)
            {
                mainprice = bool.Parse(parameter.ToString());
            }
            double price = System.Convert.ToDouble(value);
            bool res;
            if (price != 0)
                res = true;
            else
                res = false;

            if(mainprice)
            {
                res = !res;
            }
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

