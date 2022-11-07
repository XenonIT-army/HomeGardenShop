using System;
using System.Globalization;
using System.IO;
using Google.Protobuf;
using Xamarin.Forms;

namespace HomeGardenShop.ConvertData
{
	public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource res = null;
            try
            {
                byte[] bytes;
                if (value as ByteString != null && !(value as ByteString).IsEmpty)
                {
                    bytes = (value as ByteString).ToByteArray();
                    res = ImageSource.FromStream(() => new MemoryStream(bytes));
                }
                else
                {
                    return "Default.png";
                }
            }
            catch(Exception ex)
            {

            }
           
            //if (value != null)
            //{
            //    sum = prod.Count * prod.Price;
            //}
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class ImageVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value as ByteString != null && !(value as ByteString).IsEmpty)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

