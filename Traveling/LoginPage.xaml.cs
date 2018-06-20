using System;
using System.Collections.Generic;
using Traveling.Interfaces;
using Traveling.ViewModels;
using Xamarin.Forms;

namespace Traveling
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            var azureService = DependencyService.Get<IAzureService>();
            BindingContext = new LoginViewModel(azureService, this);
        }
    }
}
