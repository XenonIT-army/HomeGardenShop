using System;
using HomeGardenShop.CustomViews;
using HomeGardenShop.Droid.CustomViews;
using HomeGardenShop.Droid.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(ExtendedPicker), typeof(ExtendedPickerRender))]
namespace HomeGardenShop.Droid.CustomViews
{
    [Obsolete]
    public class ExtendedPickerRender : PickerRenderer
    {
        BorderHelper<ExtendedPicker> _helper;

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                _helper = new BorderHelper<ExtendedPicker>(Control, (Element as ExtendedPicker));
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                base.OnElementPropertyChanged(sender, e);
                _helper.UpdateBorderByPropertyName(e.PropertyName);
            }
            catch (Exception ex)
            {

            }

        }

    }
}

