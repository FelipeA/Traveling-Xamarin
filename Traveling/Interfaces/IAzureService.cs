using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Traveling.Interfaces
{
    public interface IAzureService
    {
        Task<MobileServiceUser> LoginAsync();
    }
}
