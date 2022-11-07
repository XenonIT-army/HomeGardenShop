using System;
using System.Collections.Generic;
using HomeGardenShop.Models;
using HomeGardenShop.ViewModels.DialogViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace HomeGardenShop.Views.DialogViews
{	
	public partial class BasketDialogPage : PopupPage
	{
		BasketDialogViewModel basketDialogViewModel;
        public BasketDialogPage(Product product)
		{
			InitializeComponent ();
			basketDialogViewModel = new BasketDialogViewModel(product,this);

			if(basketDialogViewModel != null)
            {
                this.BindingContext = basketDialogViewModel;
            }
		}

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.RemovePageAsync(this);
        }
    }
}

