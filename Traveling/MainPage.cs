using Traveling.Interfaces;
using Traveling.ViewModels;
using Xamarin.Forms;

namespace Traveling
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var roteiroService = DependencyService.Get<IRoteiroService>();
            var notificationService = DependencyService.Get<Services.NotificationService>();
            BindingContext = new MainViewModel(roteiroService, notificationService);

            ((BaseViewModel)BindingContext).Initialize(this);
        }
    }
}
