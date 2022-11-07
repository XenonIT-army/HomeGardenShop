﻿using System;
using HomeGardenShop.CustomViews;
using HomeGardenShop.iOS.CustomViews;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedImageButton), typeof(ExtendedImageButtonRenderer))]
namespace HomeGardenShop.iOS.CustomViews
{
    [Obsolete]
    public class ExtendedImageButtonRenderer : ImageButtonRenderer
    {
        public static void Init()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ImageButton> e)
        {
            base.OnElementChanged(e);

            SetTint();
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ExtendedImageButton.TintColorProperty.PropertyName || e.PropertyName == ExtendedImageButton.SourceProperty.PropertyName)
                SetTint();
        }

        void SetTint()
        {
            if (Control == null || Element == null)
                return;

            var templatedImg = Control.CurrentImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            Control.SetImage(null, UIControlState.Normal);
            if (((ExtendedImageButton)Element).TintColor != Color.Transparent)
            {
                Control.ImageView.TintColor = ((ExtendedImageButton)Element).TintColor.ToUIColor();
                Control.TintColor = ((ExtendedImageButton)Element).TintColor.ToUIColor();

            }
            Control.SetImage(templatedImg, UIControlState.Normal);
        }
    }
}

