using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Traveling.Interfaces
{
    public interface IAuthenticate
    {
        Task<MobileServiceUser> Authenticate(MobileServiceClient client, MobileServiceAuthenticationProvider provider);

        Task LogoutAsync(MobileServiceClient client, IDictionary<string, string> parameters = null);
    }
}
