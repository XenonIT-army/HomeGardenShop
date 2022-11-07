using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HomeGardenShop.Controls;
using HomeGardenShop.Models;
using HomeGardenShop.Views.DialogViews;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Rg.Plugins.Popup.Services;

namespace HomeGardenShop.ViewModels
{
	public class BasketViewModel : ViewModelToolbarBase
    {
        public DelegateCommand<Product> _removeProductCommand;
        public DelegateCommand<Product> _editProductCommand;
        public DelegateCommand _makeAnOrderCommand;
        public ObservableCollection<Product> _products;
        private bool _isVisible;
        public bool IsVisible
        {
            get
            {

                return _isVisible;
            }
            set
            {
                if (_isVisible != value)
                {
                    SetProperty(ref _isVisible, value);
                }
            }
        }

        private Order _lastOrder;
        public Order LastOrder
        {
            get
            {

                return _lastOrder;
            }
            set
            {
                if (_lastOrder != value)
                {
                    SetProperty(ref _lastOrder, value);
                }
            }
        }

        public ObservableCollection<Product> Products
        {
            get
            {

                return _products;
            }
            set
            {
                if (_products != value)
                {
                    SetProperty(ref _products, value);
                }
            }
        }

        public DelegateCommand<Product> RemoveProductCommand =>
           _removeProductCommand ?? (_removeProductCommand = new DelegateCommand<Product>((product) =>
           {
               LastOrder.Products.Remove(product);
               Products.Remove(product);
           }));
        public DelegateCommand<Product> EditProductCommand =>
          _editProductCommand ?? (_editProductCommand = new DelegateCommand<Product>(async(product) =>
          {
              await PopupNavigation.Instance.PushAsync(new BasketDialogPage(product));
          }));

        public DelegateCommand MakeAnOrderCommand =>
         _makeAnOrderCommand ?? (_makeAnOrderCommand = new DelegateCommand(async() =>
         {
             GetSumOrder();
             var res = await App.GreeterService.MakeAnOrderAsync(LastOrder);

             if(res)
             {
                 //App.AppModel.Orders.Add(new Order(LastOrder));
                 App.UpdateOrders();
                 LastOrder = new Order();
                 App.AppModel.Order = new Order();
                 Products.Clear();
                 IsVisible = false;
                 await PageDialogService.DisplayAlertAsync("Сообщение",
                         "Заказ успешно сформирован. Мы свяжимся с вами для подтверждения в ближайшее время", "Ok");

             }
             else
             {
                 await PageDialogService.DisplayAlertAsync("Ошибка",
                         "Возникли проблемы с формированием заказ, попробуйте пожалуста позже", "Ok");

             }
             //LastOrder
         }));


        public BasketViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService) :
            base(navigationService, eventAggregator, pageDialogService)
        {
            base.SelectedIndex = 1;
            this.ToolbarItems = new List<ViewItem>
            {
                new ViewItem {Title="StorePage", ImageSource="outline_settings.png" },
                  new ViewItem { Title = "BasketPage", ImageSource = "outline_settings.png" },
                  new ViewItem {Title="MainPage", ImageSource="outline_settings.png" },
                  new ViewItem { Title = "OrderHistoryPage", ImageSource = "outline_settings.png" },
                    new ViewItem {Title="UserDataPage", ImageSource="outline_settings.png" }
            };
            LastOrder = App.AppModel.Order;
            LastOrder.StatusId = 1;
            LastOrder.UserId = "1";
            Products =new ObservableCollection<Product>(LastOrder.Products);
            if(Products.Count > 0)
            {
                IsVisible = true;
            }
            else
            {
                IsVisible = false;
            }

        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

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

        private void GetSumOrder()
        {
            double sum = 0.0;
            foreach(var item in LastOrder.Products)
            {
                sum += item.CountPrice;
            }
            LastOrder.Sum = sum;
        }
    }
}

