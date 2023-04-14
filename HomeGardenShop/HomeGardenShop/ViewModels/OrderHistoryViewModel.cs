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
	public class OrderHistoryViewModel : ViewModelToolbarBase
    {
        private DelegateCommand<Order> _detailCommand;
        private DelegateCommand<Order> _cancelStatusCommand;
        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get
            {

                return _orders;
            }
            set
            {
                if (_orders != value)
                {
                    SetProperty(ref _orders, value);
                }
            }
        }
        public OrderHistoryViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService) :
            base(navigationService, eventAggregator, pageDialogService)
        {
            base.SelectedIndex = 3;
            this.ToolbarItems = new List<ViewItem>
            {
                new ViewItem {Title="StorePage", ImageSource="outline_tree.png" },
                  new ViewItem { Title = "BasketPage", ImageSource = "outline_cargo.png" },
                  new ViewItem {Title="MainPage", ImageSource="outline_book.png" },
                  new ViewItem { Title = "OrderHistoryPage", ImageSource = "store_history.png" },
                    new ViewItem {Title="UserDataPage", ImageSource="outline_settings.png" }
            };
            Orders = new ObservableCollection<Order>(App.AppModel.Orders.OrderByDescending(x => x.Id));

        }

        public DelegateCommand<Order> DetailCommand =>
           _detailCommand ?? (_detailCommand = new DelegateCommand<Order>(async (order) =>
           {

               await PopupNavigation.Instance.PushAsync(new OrderDetailDialogPage(order));
           }));

        public DelegateCommand<Order> CancelStatusCommand =>
          _cancelStatusCommand ?? (_cancelStatusCommand = new DelegateCommand<Order>(async (order) =>
          {
            var res =  await PageDialogService.DisplayAlertAsync("Сообщение",
                     "Вы уверены что хотите отменить заказ?", "Ok","Отмена");

              if(res)
              {
                  bool cancel = await App.GreeterService.CancelOrderAsync(order);
                  if (cancel)
                  {
                      order.StatusId = (int)OrderStatus.Canceled;
                      await PageDialogService.DisplayAlertAsync("Сообщение",
                              "Заказ отменен. :(", "Ok");

                  }
                  else
                  {
                      await PageDialogService.DisplayAlertAsync("Ошибка",
                              "Возникли проблемы с отменой заказа, попробуйте пожалуста позже", "Ok");

                  }
              }
             
              //await PopupNavigation.Instance.PushAsync(new OrderDetailDialogPage(order));
          }));


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

