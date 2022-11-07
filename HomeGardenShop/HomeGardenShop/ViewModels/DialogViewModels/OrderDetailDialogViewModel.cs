using System;
using System.Collections.ObjectModel;
using System.Linq;
using HomeGardenShop.Models;
using Prism.Commands;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace HomeGardenShop.ViewModels.DialogViewModels
{
	public class OrderDetailDialogViewModel : ViewModelBase
    {
        private DelegateCommand _closeCommand;
        private ObservableCollection<Product> _products;
        private PopupPage _page;
        private Order _order;
        public Order Order
        {
            get
            {

                return _order;
            }
            set
            {
                if (_order != value)
                {
                    SetProperty(ref _order, value);
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
        public OrderDetailDialogViewModel(Order order, PopupPage page)
		{
            Order = order;
            _page = page;
            GetProducts();
           // await PopupNavigation.Instance.RemovePageAsync(_page);
        }

        void GetProducts()
        {
            Products = new ObservableCollection<Product>(Order.Products);
            foreach (var item in Products)
            {
                var product = App.AppModel.Products.Where(x => x.Id == item.Id).FirstOrDefault();
                if(product != null)
                {
                    item.CategoryId = product.CategoryId;
                    item.Description = product.Description;
                    item.Name = product.Name;
                    item.Image = product.Image;
                    var res = item.Price * item.Count;
                    item.CountPrice =  Math.Round(res, 2);
                }
            }
        }
        public DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(async () =>
            {
                await PopupNavigation.Instance.RemovePageAsync(_page);
            }));
    }
}

