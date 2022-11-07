using System;
using UIKit;

namespace HomeGardenShop.iOS.Helpers
{
    public static class AlignmentHelper
    {
        public static UITextAlignment ToHorizontalTextAlignment(this Xamarin.Forms.TextAlignment alignment)
        {
            if (alignment == Xamarin.Forms.TextAlignment.Center)
                return UITextAlignment.Center;
            return alignment == Xamarin.Forms.TextAlignment.End ? UITextAlignment.Right : UITextAlignment.Left;
        }
    }
}

