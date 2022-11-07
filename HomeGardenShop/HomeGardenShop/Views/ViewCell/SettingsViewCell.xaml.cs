using HomeGardenShop.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace HomeGardenShop.Views.ViewCell
{	
	public partial class SettingsViewCell : ContentView
	{
		private UserDataViewModel viewModel  = null;
        public SettingsViewCell ()
		{
			InitializeComponent ();
        }

		private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
		{
            viewModel = this.BindingContext as UserDataViewModel;
            if (viewModel != null && e.Value)
			{
				viewModel.LanguageChangecommand.Execute();
            }
		}
	}
}

