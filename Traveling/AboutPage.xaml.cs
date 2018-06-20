using System;
using System.Collections.Generic;
using Traveling.ViewModels;
using Xamarin.Forms;

namespace Traveling
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

		protected void onItemTapped(object sender, EventArgs e)
		{
            ((AboutViewModel)this.BindingContext).ExecuteSobreDevCommand();
		}
    }
}
