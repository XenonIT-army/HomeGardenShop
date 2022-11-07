using System;
using HomeGardenShop.CustomViews;
using HomeGardenShop.iOS.CustomViews;
using HomeGardenShop.iOS.Helpers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedDatePicker), typeof(ExtendedDatePickerRenderer))]
namespace HomeGardenShop.iOS.CustomViews
{
    public class ExtendedDatePickerRenderer : DatePickerRenderer
    {
        PickerBorderHelper<DatePicker> _helper;
        ExtendedDatePicker view;
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                _helper = new PickerBorderHelper<DatePicker>(Control, (Element as ExtendedDatePicker));
                SetTextAlignment();
                var customPicker = e.NewElement as ExtendedDatePicker;
                SetUIButton(customPicker.DoneButtonText);
            }
            //if (e.OldElement != null)
            //{
            //    var toolbar = (UIToolbar)Control.InputAccessoryView;
            //    var doneBtn = toolbar.Items[1];

            //}

            //if (e.NewElement != null)
            //{
            //    view = e.NewElement as ExtendedDatePicker;
            //    var toolbar = (UIToolbar)Control.InputAccessoryView;
            //    var doneBtn = toolbar.Items[1];

            //}
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
            if (e.PropertyName == ExtendedDatePicker.HorizontalTextAlignmentProperty.PropertyName)
            {
                SetTextAlignment();
            }
        }
        public void SetTextAlignment()
        {
            Control.TextAlignment = (Element as ExtendedDatePicker).HorizontalTextAlignment.ToHorizontalTextAlignment();
        }


    }
}

