using System;
using System.Collections.Generic;
using HomeGardenShop.ViewModels;
using HomeGardenShop.Views.ViewCell;
using System.Linq;
using Xamarin.Forms;
using HomeGardenShop.Models;
using System.Collections;

namespace HomeGardenShop.Views
{	
	public partial class StorePage : ContentPage
	{	
      bool isAppearing = false;

        public StorePage ()
		{
            isAppearing = true;
            InitializeComponent ();

		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            AddView();
            isAppearing = false;
        }

        public int GetEnumerableCount(IEnumerable Enumerable)
		{
			return (from object Item in Enumerable
					select Item).Count();
		}

        private void Switcher_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            if(e.PropertyName ==  nameof(TabHost.SelectedIndex) && !isAppearing)
            {
                int count = GetEnumerableCount(TabHost.ItemsSource);
                if (Switcher.Children.Count < count)
                {
                    for (int i = Switcher.Children.Count; i < count; i++)
                    {
                        ProductsLoaderView loaderView = new ProductsLoaderView();
                        Switcher.Children.Add(loaderView);
                    }
                }
            }
        }
        private void AddView()
        {
            int count = GetEnumerableCount(TabHost.ItemsSource);
            if (count > 0)
            {
                for (int i = 1; i < count; i++)
                {
                    ProductsLoaderView loaderView = new ProductsLoaderView();
                    Switcher.Children.Add(loaderView);
                }
            }
        }
    }
}

