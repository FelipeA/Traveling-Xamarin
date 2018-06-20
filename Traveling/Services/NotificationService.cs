using System;
using System.Threading.Tasks;
using Traveling.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(Traveling.Services.NotificationService))]
namespace Traveling.Services
{
    public class NotificationService
    {
        public async Task Register()
        {
#if __ANDROID__
			var auth = DependencyService.Get<INotificationService>();
			await auth.RegisterNotificationAsync();
#endif
        }
    }
}
