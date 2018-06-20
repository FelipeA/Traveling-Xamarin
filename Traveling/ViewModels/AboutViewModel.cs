using System;
using Version.Plugin;
using Xamarin.Forms;

namespace Traveling.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string Versao => "1.0.0";

        public Command SobreDevCommand { get; private set; }

        public AboutViewModel()
        {
            SobreDevCommand = new Command(ExecuteSobreDevCommand);
        }

        public async void ExecuteSobreDevCommand()
        {
            await PushAsync<ContentWebViewModel>();
        }
    }
}
