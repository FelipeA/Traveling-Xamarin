using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Traveling.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(Traveling.Services.AzureService))]
namespace Traveling.Services
{
    public class AzureService : IAzureService
    {
        public MobileServiceClient client { get; private set; }

        public AzureService()
        {
            client = new MobileServiceClient(Helpers.Constants.AzureUrl);
        }

        public async Task<MobileServiceUser> LoginAsync()
        {
			var auth = DependencyService.Get<IAuthenticate>();
			var user = await auth.Authenticate(client, MobileServiceAuthenticationProvider.Facebook);

			if (user == null)
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
					await App.Current.MainPage.DisplayAlert("Ops!", "Não conseguimos efetuar o seu login. Tente mais tarde.", "OK");
				});

				return null;
			}

			return user;
        }

        public async Task LogoutAsync()
        {
            var auth = DependencyService.Get<IAuthenticate>();
            await auth.LogoutAsync(client);
        }
    }
}
