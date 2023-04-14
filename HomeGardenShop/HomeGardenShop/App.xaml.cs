using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Grpc.Net.Client;
using HomeGardenShop.AppManagers;
using HomeGardenShop.Helps.DataContainer;
using HomeGardenShop.Services;
using HomeGardenShop.ViewModels;
using HomeGardenShop.ViewModels.DialogViewModels;
using HomeGardenShop.Views;
using HomeGardenShop.Views.DialogViews;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeGardenShop
{
    public partial class App : PrismApplication
    {
        public static AppModel AppModel { get; private set; }
        public static AppInfo Info { get; private set; }
        public static GardenShopService GreeterService;
        public static bool IsLoadingData { get; set; }
        private DataContainer dataContainer;
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Sharpnado.CollectionView.Initializer.Initialize(true, false);
            Sharpnado.Shades.Initializer.Initialize(false);
            Sharpnado.Tabs.Initializer.Initialize(true, false);
            Info = new AppInfo();
            AppModel = new AppModel();
            dataContainer = new DataContainer();
            GetTheme();
            GetLocalization();
            
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }


        protected override void OnStart()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                UpdateData();
                IsLoadingData = true;
            }
            else
            {
                IsLoadingData = false;
            }
        }

        protected override void OnSleep()
        {
            this.PopupPluginOnSleep();
        }

        protected override void OnResume()
        {
            this.PopupPluginOnResume();
            GreeterService = new GardenShopService();
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupDialogService();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
            containerRegistry.RegisterForNavigation<UserDataPage, UserDataViewModel>();
            containerRegistry.RegisterForNavigation<StorePage, StoreViewModel>();
            containerRegistry.RegisterForNavigation<BasketPage, BasketViewModel>();
            containerRegistry.RegisterForNavigation<OrderHistoryPage, OrderHistoryViewModel>();
            containerRegistry.RegisterDialog<BasketDialogPage, BasketDialogViewModel>();
            containerRegistry.RegisterDialog<OrderDetailDialogPage, OrderDetailDialogViewModel>();
            containerRegistry.RegisterDialog<EditUserInfoDialogPage, EditUserInfoViewModel>();
        }

        public static async Task UpdateData()
        {
            GreeterService = new GardenShopService();
            AppModel.Products = await GreeterService.GetProductsAsync(CultureInfo.CurrentUICulture.Name);
            AppModel.Categorys = await GreeterService.GetCategorysAsync(CultureInfo.CurrentUICulture.Name);
            AppModel.Orders = await GreeterService.GetListOrdersAsync("1");
            AppModel.News = await GreeterService.GetListNews(CultureInfo.CurrentUICulture.Name);
            AppModel.AboutUs = await GreeterService.GetAboutUsText(CultureInfo.CurrentUICulture.Name);
        }
        public static async void UpdateOrders()
        {
            AppModel.Orders = await GreeterService.GetListOrdersAsync("1");
        }
        private void GetTheme()
        {
            try
            {
                var theme = dataContainer.GetValue(App.Info.ThemeKey).ToString();
                if (theme != null)
                {
                    if (theme == "Dark")
                        Application.Current.UserAppTheme = OSAppTheme.Dark;
                    else
                        Application.Current.UserAppTheme = OSAppTheme.Light;
                }
            }
            catch { }
        }
        private void GetLocalization()
        {
            try
            {
                var language = dataContainer.GetValue(App.Info.LanguageKey);
                if (language != null)
                {
                    CultureInfo selectedCulture = new CultureInfo(language.ToString());
                    CultureInfo.DefaultThreadCurrentCulture = selectedCulture;
                    CultureInfo.DefaultThreadCurrentUICulture = selectedCulture;
                    CultureInfo.CurrentCulture = selectedCulture;
                    CultureInfo.CurrentUICulture = selectedCulture;
                }
            }
            catch { }

        }
    }
}

