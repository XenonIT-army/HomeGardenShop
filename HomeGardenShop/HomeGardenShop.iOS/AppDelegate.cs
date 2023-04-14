﻿using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Prism;
using Prism.Ioc;
using Sharpnado.CollectionView;
using UIKit;
using Xamarin.Forms;

namespace HomeGardenShop.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        public override UIWindow Window
        {
            get;
            set;
        }
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.SetFlags(new string[] { "RadioButton_Experimental", "IndicatorView_Experimental", "AppTheme_Experimental", "FastRenderers_Experimental" });
            global::Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            Sharpnado.Tabs.iOS.Preserver.Preserve();
            Sharpnado.Shades.iOS.iOSShadowsRenderer.Initialize();
            Initializer.Initialize(false,false);
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }
    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}

