using System;
using System.Collections.Generic;
using HomeGardenShop.Models;
using HomeGardenShop.ViewModels.DialogViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace HomeGardenShop.Views.DialogViews
{	
	public partial class OrderDetailDialogPage : PopupPage
	{
        OrderDetailDialogViewModel orderDetailDialogViewModel;
        public OrderDetailDialogPage(Order order)
		{
			InitializeComponent ();
            orderDetailDialogViewModel = new OrderDetailDialogViewModel(order,this);

            if (orderDetailDialogViewModel != null)
            {
                this.BindingContext = orderDetailDialogViewModel;
            }
        }
	}
}

