using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using HomeGardenShop.AppManagers;
using HomeGardenShop.Controls;
using HomeGardenShop.Helps.AppLocalizer;
using HomeGardenShop.Helps.DataContainer;
using HomeGardenShop.Helps.DependencyServices;
using HomeGardenShop.Models;
using HomeGardenShop.Views.DialogViews;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace HomeGardenShop.ViewModels
{
    public class UserDataViewModel : ViewModelToolbarBase
    {
        private DelegateCommand _editCommand;
        private DelegateCommand _registrCommand;
        private DelegateCommand _signInCommand;
        private DelegateCommand _signOutCommand;
        private DelegateCommand _themeChangecommand;
        private DelegateCommand _languageChangecommand;
        private DataContainer dataContainer;
        private ObservableCollection<Language> _languages;
        private int _selectedViewIndex;
        private bool _isStart;
        private bool _isSign;
        private bool _isChange;
        public ObservableCollection<Language> Languages
        {
            get
            {
                return _languages;
            }
            set
            {
                if (_languages != value)
                {
                    SetProperty(ref _languages, value);
                }
            }
        }
        public bool IsSign
        {
            get
            {

                return _isSign;
            }
            set
            {
                if (_isSign != value)
                {
                    SetProperty(ref _isSign, value);
                }
            }
        }
        private bool _isDarkMode;
        public bool IsDarkMode
        {
            get
            {

                return _isDarkMode;
            }
            set
            {
                if (_isDarkMode != value)
                {
                    ChangeTheme(_isStart);
                    SetProperty(ref _isDarkMode, value);
                }
            }
        }

        private User _user;
        public User User
        {
            get
            {

                return _user;
            }
            set
            {
                if (_user != value)
                {
                    SetProperty(ref _user, value);
                }
            }
        }
        public int SelectedViewIndex
        {
            get => _selectedViewIndex;
            set
            {
                SetProperty(ref _selectedViewIndex, value);
            }
        }
        private string _aboutUsText;
        public string AboutUsText
        {
            get
            {

                return _aboutUsText;
            }
            set
            {
                if (_aboutUsText != value)
                {
                    SetProperty(ref _aboutUsText, value);
                }
            }
        }


        public UserDataViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService) :
           base(navigationService, eventAggregator, pageDialogService)
        {
            base.SelectedIndex = 4;
            SelectedViewIndex = 1;
            this.ToolbarItems = new List<ViewItem>
            {
                new ViewItem {Title="StorePage", ImageSource="outline_tree.png" },
                  new ViewItem { Title = "BasketPage", ImageSource = "outline_cargo.png" },
                  new ViewItem {Title="MainPage", ImageSource="outline_book.png" },
                  new ViewItem { Title = "OrderHistoryPage", ImageSource = "store_history.png" },
                    new ViewItem {Title="UserDataPage", ImageSource="outline_settings.png" }
            };

            User = App.AppModel.User;
            AboutUsText = App.AppModel.AboutUs;
            dataContainer = new DataContainer();
            GetTheme();
            GetLanguage();
            _isStart = true;
            if (User.Id != null && User.Id != "")
            {
                IsSign = true;
            }
        }

        public DelegateCommand ThemeChangeCommand =>
           _themeChangecommand ?? (_themeChangecommand = new DelegateCommand(() =>
           {
               if (Application.Current.UserAppTheme == OSAppTheme.Dark)
                   Application.Current.UserAppTheme = OSAppTheme.Light;
               else
                   Application.Current.UserAppTheme = OSAppTheme.Dark;
               SaveTheme();
           }));
        public DelegateCommand LanguageChangecommand =>
         _languageChangecommand ?? (_languageChangecommand = new DelegateCommand(() =>
         {
             if (!_isChange)
             {
                 RefreshLocalization();
                 UpdateData();
             }
             else
                 _isChange = false;
         }));
        public DelegateCommand EditCommand =>
            _editCommand ?? (_editCommand = new DelegateCommand(async () =>
            {
                await PopupNavigation.Instance.PushAsync(new EditUserInfoDialogPage(User));
            }));

        public DelegateCommand SignOutCommand =>
           _signOutCommand ?? (_signOutCommand = new DelegateCommand(() =>
           {
               User.SignOut();
               IsSign = false;
           }));

        public DelegateCommand SignInCommand =>
           _signInCommand ?? (_signInCommand = new DelegateCommand(async () =>
           {
               User.Id = "2";
               bool isRegistr = await App.GreeterService.IsRegistrUser(User);
               if (isRegistr)
               {
                   IsSign = true;
                   App.AppModel.User = await App.GreeterService.GetUser(App.AppModel.User);
               }
               else
               {
                   bool res = await App.GreeterService.RegistrUser(User);
                   if (res)
                   {
                       IsSign = true;
                   }
               }

           }));


        public DelegateCommand RegistrCommand =>
           _registrCommand ?? (_registrCommand = new DelegateCommand(async () =>
           {
               bool res = await App.GreeterService.RegistrUser(User);
               if (res)
               {
                   await PageDialogService.DisplayAlertAsync("Сообщение",
                           "Пользователь успешно зарегистрирован.", "Ok");

               }
               else
               {
                   await PageDialogService.DisplayAlertAsync("Ошибка",
                           "Возникли проблемы с регистрацией, попробуйте пожалуста позже", "Ok");

               }
           }));


        private void SaveTheme()
        {
            dataContainer.RemoveValue(App.Info.ThemeKey);
            dataContainer.AddValue(App.Info.ThemeKey, Application.Current.UserAppTheme.ToString());
        }

        private void GetTheme()
        {
            try
            {
                var theme = dataContainer.GetValue(App.Info.ThemeKey).ToString();
                if (theme != null)
                {
                    if (theme == "Dark")
                        IsDarkMode = true;
                    else
                        IsDarkMode = false;
                }
            }
            catch { }
        }
        private void ChangeTheme(bool isStart)
        {
            if (isStart)
            {
                var e = DependencyService.Get<IStatusBarStyleManager>();
                if (Application.Current.UserAppTheme == OSAppTheme.Dark)
                {
                    Application.Current.UserAppTheme = OSAppTheme.Light;
                    e?.SetColoredStatusBar("#ffffff", true);
                }
                else
                {
                    Application.Current.UserAppTheme = OSAppTheme.Dark;
                    e?.SetColoredStatusBar("#2c2c2e", false);
                }


                SaveTheme();
            }
        }
        private  void RefreshLocalization()
        {
            
                string language = Languages.Where(x => x.IsChange == true).Select(x => x.Reduction).FirstOrDefault();
                if (language != null)
                {
                    CultureInfo newCulture = new CultureInfo(language);
                    CultureInfo.CurrentUICulture = newCulture;
                    dataContainer.RemoveValue(App.Info.LanguageKey);
                    dataContainer.AddValue(App.Info.LanguageKey, language);
                    Translator.TranslatorInstance.Invalidate();
                    AboutUsText = App.AppModel.AboutUs;
                }
           
        }

        private async void UpdateData()
        {
            await App.UpdateData();
        }
        private void GetLanguage()
        {
            _isChange = true;
            _languages = new ObservableCollection<Language>
            {
                 new Language { Name = LanguageEnum.Українська, Reduction = "uk-UA", IsChange = false},
                new Language { Name = LanguageEnum.Русский, Reduction = "ru-RU", IsChange = false},
                new Language { Name = LanguageEnum.English, Reduction = "en-US", IsChange = false}
            };
            foreach (var item in _languages)
            {
                if (item.Reduction == CultureInfo.CurrentUICulture.Name)
                {
                    item.IsChange = true;
                }
            }
        }
        protected override void ToolbarItemClicked(object parameter)
        {
            var index = ToolbarItems.IndexOf(parameter as ViewItem);
            if (index != SelectedIndex)
                switch (index)
            {
                case 0: NavigateAsync("StorePage"); break;
                case 1: NavigateAsync("BasketPage"); break;
                case 2: NavigateAsync("MainPage"); break;
                case 3: NavigateAsync("OrderHistoryPage"); break;
                case 4: NavigateAsync("UserDataPage"); break;
                default: break;
            }
        }

    }
}

