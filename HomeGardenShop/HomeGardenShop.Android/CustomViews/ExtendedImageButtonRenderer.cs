using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using HomeGardenShop.CustomViews;
using HomeGardenShop.Droid.CustomViews;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedImageButton), typeof(ExtendedImageButtonRenderer))]
namespace HomeGardenShop.Droid.CustomViews
{
    [Obsolete]
    public class ExtendedImageButtonRenderer : ImageButtonRenderer
    {
        public ExtendedImageButtonRenderer(Context context)
            : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ImageButton> e)
        {
            base.OnElementChanged(e);

            SetTint();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ExtendedImageButton.TintColorProperty.PropertyName || e.PropertyName == ExtendedImageButton.SourceProperty.PropertyName)
                SetTint();
        }

        void SetTint()
        {
            try
            {
                if (this == null || Element == null)
                    return;

                if (((ExtendedImageButton)Element).TintColor != Xamarin.Forms.Color.Transparent)
                {
                    if (this.ColorFilter != null)
                        this.ClearColorFilter();
                }

                //Apply tint color
                var colorFilter = new PorterDuffColorFilter(((ExtendedImageButton)Element).TintColor.ToAndroid(), PorterDuff.Mode.SrcIn);
                this.SetColorFilter(colorFilter);
            }
            catch { }

        }
    }
}

