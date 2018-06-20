using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using Traveling.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(Traveling.iOS.Services.AuthenticateIOS))]
namespace Traveling.iOS.Services
{
    public class AuthenticateIOS : IAuthenticate
    {
        public async Task<MobileServiceUser> Authenticate(MobileServiceClient client, MobileServiceAuthenticationProvider provider)
        {
            try{
                return await client.LoginAsync(UIKit.UIApplication.SharedApplication.KeyWindow.RootViewController, provider); 
            }
            catch
            {
                return null;
            }
        }

		public async Task LogoutAsync(MobileServiceClient client, IDictionary<string, string> parameters = null)
		{
			try
			{
				ClearCookies();
				await client.LogoutAsync();
			}
			catch (Exception)
			{
				// TODO: Log error
				throw;
			}
		}

        private void ClearCookies()
        {
			foreach (var c in NSHttpCookieStorage.SharedStorage.Cookies)
				NSHttpCookieStorage.SharedStorage.DeleteCookie(c);
        }
    }
}
