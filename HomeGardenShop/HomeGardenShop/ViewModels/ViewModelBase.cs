using System;
using System.Collections;
using System.Threading.Tasks;
using HomeGardenShop.Views.DialogViews;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HomeGardenShop.ViewModels
{
	public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
	{
        public INavigationService NavigationService { get; private set; }
        protected IPageDialogService PageDialogService { get; private set; }
        protected IEventAggregator EventAggregator { get; private set; }
        private bool _isNavigating = true;
        private InternetConnectionDialogPage page;
        protected bool _isConnected;
        public bool isStartNavigate;
        protected bool IsNavigating
        {
            get => _isNavigating;
            set => SetProperty(ref _isNavigating, value);
        }
        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }
        public ViewModelBase()
		{
            IsConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            if (!IsConnected)
            {
                GetInternetConncetionView();
            }
        }

        protected ViewModelBase(INavigationService navigationService) : this()
        {
            NavigationService = navigationService;
        }

        protected ViewModelBase(INavigationService navigationService, IEventAggregator eventAggregator) : this(navigationService)
        {
            EventAggregator = eventAggregator;
        }

        protected ViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService) : this(navigationService)
        {
            PageDialogService = pageDialogService;
        }

        protected ViewModelBase(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService) : this(navigationService, eventAggregator)
        {
            PageDialogService = pageDialogService;
        }

        public void Destroy()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            Device.StartTimer(TimeSpan.FromSeconds(0.1), (Func<bool>)(() =>
            {
                try
                {
                    isStartNavigate = true;
                    return false;
                }
                catch
                {
                    return true;
                }
            }));
            IsNavigating = false;
        }

        protected Task NavigateAsync(string name, NavigationParameters parameters = null, bool? useModalNavigation = null, bool animated = false)
        {
            //if (_isNavigating)
            //    return Task.CompletedTask;
            isStartNavigate = false;
            IsNavigating = true;
            try { NavigationService.NavigateAsync(name, parameters, useModalNavigation, animated); IsNavigating = false; return Task.CompletedTask; }
            catch { return Task.CompletedTask; }
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var isConnected = e.NetworkAccess;

            if (isConnected == NetworkAccess.Internet)
            {
                if(!App.IsLoadingData)
                {
                    App.UpdateData();
                }
                IsConnected = true;
                RemoveInternetConnectionView();
            }
            else
            {
                GetInternetConncetionView();
                IsConnected = false;
            }
            
        }
        private async void GetInternetConncetionView()
        {
           page = new InternetConnectionDialogPage();
            await PopupNavigation.Instance.PushAsync(page);
        }
        private async void RemoveInternetConnectionView()
        {
            if(page != null)
            await PopupNavigation.Instance.RemovePageAsync(page);
        }
    }

    public abstract class ViewModelToolbarBase : ViewModelBase
    {
        private DelegateCommand<object> _itemSelectionChangedCommand;
        private IList _toolbarItems;
        private int _selectedIndex;

        public IList ToolbarItems
        {
            get => _toolbarItems;
            set => SetProperty(ref _toolbarItems, value);
        }
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
        }
        //protected IDialogService DialogService { get; private set; }

        public DelegateCommand<object> ItemSelectionChangedCommand =>
            _itemSelectionChangedCommand ?? (_itemSelectionChangedCommand = new DelegateCommand<object>((parameter) => ToolbarItemClicked(parameter)));

        protected ViewModelToolbarBase(INavigationService navigationService, IPageDialogService pageDialogService) :
            base(navigationService, pageDialogService)
        {
        }

        //protected ViewModelToolbarBase(INavigationService navigationService, IPageDialogService pageDialogService, IDialogService dialogService) :
        //    base(navigationService, pageDialogService)
        //{
        //    DialogService = dialogService;
        //}

        protected ViewModelToolbarBase(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService) :
            base(navigationService, eventAggregator, pageDialogService)
        {
        }

        protected abstract void ToolbarItemClicked(object parameter);
    }
}

