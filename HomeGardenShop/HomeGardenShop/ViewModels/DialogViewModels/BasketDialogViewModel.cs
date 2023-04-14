using System;
using System.Linq;
using HomeGardenShop.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace HomeGardenShop.ViewModels.DialogViewModels
{
	public class BasketDialogViewModel: ViewModelBase
	{
        private DelegateCommand _addProductToOrderCommand;
        private PopupPage _page;
        private Product _product;
        public Product Product
        {
            get
            {

                return _product;
            }
            set
            {
                if (_product != value)
                {
                    SetProperty(ref _product, value);
                }
            }
        }
        private bool _isError;
        public bool IsError
        {
            get
            {

                return _isError;
            }
            set
            {
                if (_isError != value)
                {
                    SetProperty(ref _isError, value);
                }
            }
        }
        private string _error;
        public string Error
        {
            get
            {

                return _error;
            }
            set
            {
                if (_error != value)
                {
                    SetProperty(ref _error, value);
                }
            }
        }
        public BasketDialogViewModel(Product product, PopupPage page)
        {
            _product = new Product(product);
            _page = page;
        }

        public DelegateCommand AddProductToOrderCommand =>
            _addProductToOrderCommand ?? (_addProductToOrderCommand = new DelegateCommand(async() =>
            {
                if(Product.Count != 0 && Product.Count <= Product.AllCount)
                {
                    IsError = false;

                    var item = App.AppModel.Order.Products.Where(x=> x.Id == Product.Id).FirstOrDefault();
                    if(item != null)
                    {
                        item.Count = Product.Count;
                    }
                    else
                    {
                        App.AppModel.Order.Products.Add(Product);
                    }
                    await PopupNavigation.Instance.RemovePageAsync(_page);
                }
                else if(Product.Count > Product.AllCount)
                {
                    Error = "Count can't be more than all count";
                     IsError = true;
                }
                else
                {
                    Error = "Count can't be null";
                    IsError = true;
                }
            }));

    }
}

