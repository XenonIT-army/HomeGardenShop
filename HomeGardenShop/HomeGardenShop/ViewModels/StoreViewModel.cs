using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HomeGardenShop.AppManagers;
using HomeGardenShop.Controls;
using HomeGardenShop.Models;
using HomeGardenShop.Views.DialogViews;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace HomeGardenShop.ViewModels
{
    public class StoreViewModel : ViewModelToolbarBase
    {
        private DelegateCommand<Product> _basketCommand;
        private DelegateCommand _refreshCommand;
        private string _name;
        private bool _isRefreshing;
        private bool isBusy;
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        private ObservableCollection<Product> _loadProducts = new ObservableCollection<Product>();
        private List<Product> _allProducts = new List<Product>();
        private ObservableCollection<Category> _categories = new ObservableCollection<Category>();
        private int _selectedProductsIndex = 0;
        //public TaskLoaderNotifier<ObservableCollection<Product>> Loader { get; }
        bool isStart;
        public int SelectedProductsIndex
        {
            get => _selectedProductsIndex;
            set
            {
                if (!IsRefreshing)
                {
                    SetProperty(ref _selectedProductsIndex, value);
                    ReloadAsync();
                }
            }
        }
        public string Name
        {
            get
            {

                return _name;
            }
            set
            {
                if (_name != value)
                {
                    SetProperty(ref _name, value);
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
        public ObservableCollection<Category> Categories
        {
            get
            {

                return _categories;
            }
            set
            {
                if (_categories != value)
                {
                    SetProperty(ref _categories, value);
                }
            }
        }
        public ObservableCollection<Product> LoadProducts
        {
            get
            {

                return _loadProducts;
            }
            set
            {
                if (_loadProducts != value)
                {
                    SetProperty(ref _loadProducts, value);
                }
            }
        }

        public StoreViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService) :
            base(navigationService, eventAggregator, pageDialogService)
        {
            base.SelectedIndex = 0;
            Name = "UserData";
          
            //Categories.Add(new Category { Name = "Огурцы", Id = 1 });
            //Categories.Add(new Category { Name = "Помидоры", Id = 0 });
            _allProducts = App.AppModel.Products;

            this.ToolbarItems = new List<ViewItem>
            {
                new ViewItem {Title="StorePage", ImageSource="outline_tree.png" },
                  new ViewItem { Title = "BasketPage", ImageSource = "outline_cargo.png" },
                  new ViewItem {Title="MainPage", ImageSource="outline_book.png" },
                  new ViewItem { Title = "OrderHistoryPage", ImageSource = "store_history.png" },
                    new ViewItem {Title="UserDataPage", ImageSource="outline_settings.png" }
            };

            GetCategoryCountProduct();
            //Loader = new TaskLoaderNotifier<ObservableCollection<Product>>();

        }


        public DelegateCommand<Product> BasketCommand =>
            _basketCommand ?? (_basketCommand = new DelegateCommand<Product>(async(product) =>
            {
                //await NavigationService.NavigateAsync("BasketDialogPage");
                await PopupNavigation.Instance.PushAsync(new BasketDialogPage(product));
            }));
        public DelegateCommand RefreshCommand =>
          _refreshCommand ?? (_refreshCommand = new DelegateCommand(async() =>
          {
              IsRefreshing = true;
              App.AppModel.Categorys = await App.GreeterService.GetCategorysAsync(CultureInfo.CurrentUICulture.Name);
              App.AppModel.Products = await App.GreeterService.GetProductsAsync(CultureInfo.CurrentUICulture.Name);
              _allProducts = App.AppModel.Products;
              GetCategoryCountProduct();
              IsRefreshing = false;
              ReloadAsync();
          }));



        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            //isStart = false;
            //OnLoad(SelectedProductsIndex);
            //Loader.Load(_ => InitializeAsync());
            //isStart = true;
        }

        private Task<ObservableCollection<Product>> GetProductsList(int index)
        {
            var res = new ObservableCollection<Product>(_allProducts.Where(x => x.CategoryId == Categories[index].Id).OrderByDescending(x=> x.Id));
            return  Task.FromResult(res);
        }
        //private async Task<ObservableCollection<Product>> InitializeAsync()
        //{
        //    this.IsBusy = isStart;
        //    if (isStart)
        //    {
        //        await Task.Delay(3000);

        //        _allProducts = await App.GreeterService.GetProductsAsync("test");
        //    }
        //    this.Products = await GetProductsList(SelectedProductsIndex);
        //    this.IsBusy = false;
        //    return this.Products;
        //}
        private async void ReloadAsync()
        {
            this.Products.Clear();
            var list = await GetProductsList(SelectedProductsIndex);
            foreach (var product in list)
            {
                this.Products.Add(product);
            }
        }

        //bool isBusy = false;

        //public bool IsBusy
        //{
        //    get { return isBusy; }
        //    set { SetProperty(ref isBusy, value); }
        //}
        

        private void GetCategoryCountProduct()
        {
            var categories = new ObservableCollection<Category>(App.AppModel.Categorys);
            foreach (var item in categories)
            {
                item.Count = this._allProducts.Where(x => x.CategoryId == item.Id).Count();
            }
            if (IsRefreshing)
            {
                foreach (var item in categories)
                {
                    var res = this.Categories.Where(x => x.Id == item.Id).FirstOrDefault();
                    if (res == null && item.Count > 0)
                    {
                        this.Categories.Add(item);
                    }
                    else if(res != null)
                    {
                        res.Count = item.Count;
                    }
                  
                }
            }
            else
            {
                //foreach (var item in categories)
                //{
                //    item.Count = this._allProducts.Where(x => x.CategoryId == item.Id).Count();
                //}
                this.Categories = new ObservableCollection<Category>(categories.Where(x => x.Count > 0));
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

