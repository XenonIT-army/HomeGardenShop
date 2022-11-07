using System;
using System.Collections.Generic;
using HomeGardenShop.Models;
using HomeGardenShop.ViewModels.DialogViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace HomeGardenShop.Views.DialogViews
{	
	public partial class EditUserInfoDialogPage : PopupPage
    {
        EditUserInfoViewModel basketDialogViewModel;
        public EditUserInfoDialogPage (User user)
		{
			InitializeComponent ();

            basketDialogViewModel = new EditUserInfoViewModel(user, this);

            if (basketDialogViewModel != null)
            {
                this.BindingContext = basketDialogViewModel;
            }
        }
	}
}

