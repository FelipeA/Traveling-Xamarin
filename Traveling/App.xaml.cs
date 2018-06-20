using Xamarin.Forms;

namespace Traveling
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            VerifyUserLoggedToRedirect();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            VerifyUserLoggedToRedirect();
        }

        private void VerifyUserLoggedToRedirect()
        {
            if (Application.Current.Properties.ContainsKey("isLogged"))
            {
                var isLogged = false;
                if (Current.Properties["isLogged"] != null)
                    isLogged = ((bool)Current.Properties["isLogged"]);

                if (isLogged) // isLogged
                {
                    //MainPage = new NavigationPage(new MainPage());
                    var MDPage = new MasterDetailPage();
                    MDPage.Detail = new NavigationPage(new MainPage());
                }
                else
                    MainPage = new LoginPage();
            }
		    else
			    MainPage = new LoginPage();
        }
    }
}
