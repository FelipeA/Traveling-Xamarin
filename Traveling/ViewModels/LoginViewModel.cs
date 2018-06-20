using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Traveling.Interfaces;
using Traveling.Services;
using Xamarin.Forms;

namespace Traveling.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly AzureService _azureService;
        private readonly Page _page;

        public Command LoginCommand { get; private set; }

		private MobileServiceUser _user;
		public MobileServiceUser user
		{
			get
			{
				return _user;
			}
			set
			{
				SetProperty(ref _user, value);
			}
		}

        public LoginViewModel(IAzureService azureService, Page page)
        {
            _azureService = azureService as AzureService;
            _page = page;
            
            LoginCommand = new Command(ExecuteLoginCommand);
        }

		private async void ExecuteLoginCommand()
		{
			await ExecuteLogin();
		}

		private async Task ExecuteLogin()
		{
			if (user == null)
			{
				user = await _azureService.LoginAsync();
				if (user != null)
				{
                    //retornoLogin = $"Bem vindo: {user.UserId}";
                    //btnLoginText = "Logout";

                    //_page.Navigation.InsertPageBefore(new MainPage(), _page);
                    //await _page.Navigation.PopAsync();
                    //Application.Current.MainPage = new NavigationPage(new MainPage());

				}
				else
				{
                    //retornoLogin = "Falha no login, tente novamente!";
                    await DisplayAlert("Ops...", "Falha ao tentar logar, tente novamente!", "OK");
				}
			}
			else
			{
				await _azureService.LogoutAsync();
				user = null;

				await DisplayAlert("Até logo...", "Espero não demorar para te ver novamente por aqui!", "OK");
			}
		}
    }
}
