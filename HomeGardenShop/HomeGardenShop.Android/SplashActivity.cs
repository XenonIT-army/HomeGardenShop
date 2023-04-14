using Android.App;
using Android.Content.PM;
using static Android.Content.Res.Resources;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidX.AppCompat.App;

namespace HomeGardenShop.Droid
{
    [Activity(MainLauncher = true,
 ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden,
 Icon = "@mipmap/icon", Theme = "@style/splashscreen", NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    StartActivity(typeof(MainActivity));
        //}
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.splash_layout);
            System.Threading.Tasks.Task splashscreen = new System.Threading.Tasks.Task(() => {
                SplashScreen();
            });
            splashscreen.Start();
        }

        public async void SplashScreen()
        {
            await System.Threading.Tasks.Task.Delay(2000);
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }
    }
}