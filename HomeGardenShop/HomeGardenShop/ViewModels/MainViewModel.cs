using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using HomeGardenShop.Controls;
using HomeGardenShop.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;

namespace HomeGardenShop.ViewModels
{
    public class MainViewModel : ViewModelToolbarBase
    {
        private DelegateCommand _mainCommand;
        private ObservableCollection<News> _news = new ObservableCollection<News>();
        private DelegateCommand _refreshCommand;
        private bool _isRefreshing;
        public ObservableCollection<News> News
        {
            get
            {

                return _news;
            }
            set
            {
                if (_news != value)
                {
                    SetProperty(ref _news, value);
                }
            }
        }
        public bool IsRefreshing
        {
            get
            {

                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    SetProperty(ref _isRefreshing, value);
                }
            }
        }

        public MainViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService) :
           base(navigationService, eventAggregator, pageDialogService)
        {
            base.SelectedIndex = 2;
            this.ToolbarItems = new List<ViewItem>
            {
                new ViewItem {Title="StorePage", ImageSource="outline_settings.png" },
                  new ViewItem { Title = "BasketPage", ImageSource = "outline_settings.png" },
                  new ViewItem {Title="MainPage", ImageSource="outline_settings.png" },
                  new ViewItem { Title = "OrderHistoryPage", ImageSource = "outline_settings.png" },
                    new ViewItem {Title="UserDataPage", ImageSource="outline_settings.png" }
            };
            News = new ObservableCollection<News>();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            GetNews();
        }

        //private async void GetInternetError()
        //{
        //    while (!IsConnected)
        //    {
        //        await PageDialogService.DisplayAlertAsync("Сообщение",
        //               "Нет интернет соединения. Для стабильной работы приложения необходимо установить интернет связь!", "Ok");
        //    }
        //    App.UpdateData();
        //}

        private Task GetNews()
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    if (App.AppModel != null && App.AppModel.News != null && App.AppModel.News.Count > 0)
                    {
                        News = new ObservableCollection<News>(App.AppModel.News);
                        break;
                    }
                }
            });
        }
        public DelegateCommand RefreshCommand =>
         _refreshCommand ?? (_refreshCommand = new DelegateCommand(async () =>
         {
             IsRefreshing = true;
             App.AppModel.News =  await App.GreeterService.GetListNews(CultureInfo.CurrentUICulture.Name);
             News = new ObservableCollection<News>(App.AppModel.News);
             IsRefreshing = false;
         }));
        public DelegateCommand MainCommand =>
            _mainCommand ?? (_mainCommand = new DelegateCommand(() =>
            {
                NavigateAsync("StorePage");

            }));

        protected override void ToolbarItemClicked(object parameter)
        {
            var index = ToolbarItems.IndexOf(parameter as ViewItem);
            if(index != SelectedIndex)
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

