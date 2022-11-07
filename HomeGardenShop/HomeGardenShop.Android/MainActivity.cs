using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Prism;
using Prism.Ioc;
using Sharpnado.CollectionView;
using Prism.Plugin.Popups;
using Xamarin.Forms;
using AndroidX.Core.OS;
using Sharpnado.HorizontalListView.Droid;
using AndroidX.AppCompat.Widget;

namespace HomeGardenShop.Droid
{
    [Activity(Label = "HomeGardenShop", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Forms.SetFlags("IndicatorView_Experimental", "RadioButton_Experimental", "AppTheme_Experimental");
           // CrossCurrentActivity.Current.Init(this, bundle);
            SharpnadoInitializer.Initialize(enableInternalLogger: true, enableInternalDebugLogger: true);
           

            Initializer.Initialize(false,false);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Rg.Plugins.Popup.Popup.Init(this);
            LoadApplication(new App(new AndroidInitializer()));
            Toolbar toolbar
             = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            PopupPlugin.OnBackPressed();
        }

        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
            }
        }
    }
}
