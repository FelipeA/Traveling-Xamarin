using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Traveling.Interfaces;
using Traveling.Models;
using Xamarin.Forms;
using Android.Webkit;

[assembly: Dependency(typeof(Traveling.Droid.Services.AuthenticateDroid))]
namespace Traveling.Droid.Services
{
    public class AuthenticateDroid : IAuthenticate
    {
        public async Task<MobileServiceUser> Authenticate(MobileServiceClient client, MobileServiceAuthenticationProvider provider)
        {
            try{
                return await client.LoginAsync(Xamarin.Forms.Forms.Context, provider);
            }
            catch{
                return null;
            }
        }

		public async Task LogoutAsync(MobileServiceClient client, IDictionary<string, string> parameters = null)
		{
			try
			{
				CookieManager.Instance.RemoveAllCookie();
				await client.LogoutAsync();

			}
			catch (Exception)
			{
				// TODO: Log error
				throw;
			}
		}
    }
}
