using System;
using Android.Views;
using HomeGardenShop.CustomViews;
using HomeGardenShop.Droid.CustomViews;
using HomeGardenShop.Droid.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedDatePicker), typeof(ExtendedDatePickerRender))]
namespace HomeGardenShop.Droid.CustomViews
{
    [Obsolete]
    public class ExtendedDatePickerRender : DatePickerRenderer
    {
        BorderHelper<DatePicker> _helper;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                _helper = new BorderHelper<DatePicker>(Control, (Element as ExtendedDatePicker));
                SetTextAlignment();
            }

        }
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            _helper.UpdateBorderByPropertyName(e.PropertyName);

            if (e.PropertyName == ExtendedDatePicker.HorizontalTextAlignmentProperty.PropertyName)
            {
                SetTextAlignment();
            }
        }
        public void SetTextAlignment()
        {
            Control.Gravity = (Element as ExtendedDatePicker).HorizontalTextAlignment.ToHorizontalGravityFlags();
            Control.Gravity = GravityFlags.CenterVertical;
        }
    }
}

