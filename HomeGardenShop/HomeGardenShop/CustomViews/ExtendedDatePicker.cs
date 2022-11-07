using System;
using Xamarin.Forms;

namespace HomeGardenShop.CustomViews
{
    public class ExtendedDatePicker : DatePicker
    {
        public ExtendedDatePicker()
        {
        }

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(
                                                                          "BorderWidth",
                                                                          typeof(double),
                                                                          typeof(ExtendedDatePicker),
                                                                          2.0);
        public Action DoneButtonClicked;


        public double BorderWidth
        {
            get
            {
                return (double)GetValue(BorderWidthProperty);
            }
            set
            {
                SetValue(BorderWidthProperty, value);
            }
        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
                                                                          "BorderColor",
                                                                          typeof(Color),
                                                                          typeof(ExtendedDatePicker),
                                                                          Color.FromHex("#807A79"));

        public Color BorderColor
        {
            get
            {
                return (Color)GetValue(BorderColorProperty);
            }
            set
            {
                SetValue(BorderColorProperty, value);
            }
        }

        public static readonly BindableProperty BorderRadiusProperty = BindableProperty.Create(
                                                                          "BorderRadius",
                                                                          typeof(double),
                                                                          typeof(ExtendedDatePicker),
                                                                          20.0);

        public static readonly BindableProperty DoneButtonProperty =
         BindableProperty.Create("DoneButtonText", typeof(string), typeof(ExtendedDatePicker),"Done" /*Translator.TranslatorInstance["New_Route_Done"]*/);


        public double BorderRadius
        {
            get
            {
                return (double)GetValue(BorderRadiusProperty);
            }
            set
            {
                SetValue(BorderRadiusProperty, value);
            }
        }

        public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(
            "PlaceHolder",
            typeof(string),
            typeof(ExtendedDatePicker),
            "");

        public string PlaceHolder
        {
            get
            {
                return (string)GetValue(PlaceHolderProperty);
            }
            set
            {
                SetValue(PlaceHolderProperty, value);
            }
        }
        public string DoneButtonText
        {
            get => (string)GetValue(DoneButtonProperty);
            set => SetValue(DoneButtonProperty, value);
        }

        public static BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create("HorizontalTextAlignment", typeof(Xamarin.Forms.TextAlignment),
                                                                                typeof(ExtendedDatePicker),
                                                                                Xamarin.Forms.TextAlignment.Center);
        public Xamarin.Forms.TextAlignment HorizontalTextAlignment
        {
            get
            {
                return (Xamarin.Forms.TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            }
            set
            {
                SetValue(HorizontalTextAlignmentProperty, value);
            }
        }

    }
}

