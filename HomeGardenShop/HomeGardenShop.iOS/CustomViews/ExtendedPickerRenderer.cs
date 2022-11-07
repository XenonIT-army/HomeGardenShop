using System;
using HomeGardenShop.CustomViews;
using HomeGardenShop.iOS.CustomViews;
using HomeGardenShop.iOS.Helpers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedPicker), typeof(ExtendedPickerRenderer))]
namespace HomeGardenShop.iOS.CustomViews
{
    public class ExtendedPickerRenderer : PickerRenderer
    {
        PickerBorderHelper<ExtendedPicker> _helper;

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                _helper = new PickerBorderHelper<ExtendedPicker>(Control, (Element as ExtendedPicker));
                var customPicker = e.NewElement as ExtendedPicker;
                SetUIButton(customPicker.DoneButtonText);
            }


        }
        public void SetUIButton(string doneButtonText)
        {
            UIToolbar toolbar = new UIToolbar();
            toolbar.BarStyle = UIBarStyle.Default;
            toolbar.Translucent = true;
            toolbar.SizeToFit();
            UIBarButtonItem doneButton = new UIBarButtonItem(String.IsNullOrEmpty(doneButtonText) ? "OK" : doneButtonText, UIBarButtonItemStyle.Done, (s, ev) =>
            {
                Control.ResignFirstResponder();

            });
            UIBarButtonItem flexible = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
            toolbar.SetItems(new UIBarButtonItem[] { flexible, doneButton }, true);
            Control.InputAccessoryView = toolbar;
        }
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            _helper.UpdateBorderByPropertyName(e.PropertyName);
        }
    }
}

