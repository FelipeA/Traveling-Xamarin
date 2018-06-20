using System;
using System.Collections.Generic;
using Traveling.ViewModels;
using Xamarin.Forms;

namespace Traveling
{
    public partial class RoteiroPage : ContentPage
    {

        public string TitlePage{
            get{
                return this.Title;
            }
            set {
                this.Title = value;
            }
        }

        public RoteiroPage()
        {
            InitializeComponent();
        }
    }
}
