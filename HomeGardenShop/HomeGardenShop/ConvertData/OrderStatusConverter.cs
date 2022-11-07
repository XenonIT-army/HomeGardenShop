using System;
using System.Globalization;
using HomeGardenShop.Models;
using Xamarin.Forms;

namespace HomeGardenShop.ConvertData
{
	public class OrderStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int status = System.Convert.ToInt32(value);
            if (status == (int)OrderStatus.Error)
            {
                return "Ошибка";
            }
            if (status == (int)OrderStatus.Make)
            {
                return "В обработка";
            }
            if (status == (int)OrderStatus.InProcess)
            {
                return "Принят";
            }
            if (status == (int)OrderStatus.Formed)
            {
                return "Сформирован";
            }
            if (status == (int)OrderStatus.Complete)
            {
                return "Выполнен";
            }
            if (status == (int)OrderStatus.Canceled)
            {
                return "Отменен";
            }

            return "Не определен";
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
		
	}
    public class OrderStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int status = System.Convert.ToInt32(value);
           
            if (status == (int)OrderStatus.Error)
            {
                return Xamarin.Forms.Color.Red;
            }
            if (status == (int)OrderStatus.Make)
            {
                return Xamarin.Forms.Color.Gold;
            }
            if (status == (int)OrderStatus.InProcess)
            {
                return Xamarin.Forms.Color.Gold;
            }
            if (status == (int)OrderStatus.Formed)
            {
                return Xamarin.Forms.Color.Blue;
            }
            if (status == (int)OrderStatus.Complete)
            {
                return Xamarin.Forms.Color.Green;
            }
            if (status == (int)OrderStatus.Canceled)
            {
                return Xamarin.Forms.Color.Violet;
            }
            return Xamarin.Forms.Color.Gold;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

    }
    public class CancelStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int status = System.Convert.ToInt32(value);
            
            if (status == (int)OrderStatus.Error)
            {
                return false;
            }
            if (status == (int)OrderStatus.Make)
            {
                return true;
            }
            if (status == (int)OrderStatus.InProcess)
            {
                return true;
            }
            if (status == (int)OrderStatus.Formed)
            {
                return false;
            }
            if (status == (int)OrderStatus.Complete)
            {
                return false;
            }
            if (status == (int)OrderStatus.Canceled)
            {
                return false;
            }
            return false;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

    }
}

