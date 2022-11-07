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
		public StorePage ()
		{
			InitializeComponent ();

		}
        protected override void OnAppearing()
        {

            base.OnAppearing();
            int count = GetEnumerableCount(TabHost.ItemsSource);
            if (count >0)
            {
                for (int i = 1; i < count; i++)
                {
                    ProductsLoaderView loaderView = new ProductsLoaderView();
                    Switcher.Children.Add(loaderView);
                }
            }

        }

        public int GetEnumerableCount(IEnumerable Enumerable)
		{
			return (from object Item in Enumerable
					select Item).Count();
		}
	}
}

