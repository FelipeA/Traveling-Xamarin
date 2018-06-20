using System;
using System.Collections.ObjectModel;
using Traveling.Interfaces;
using Traveling.Services;
using Xamarin.Forms;
using Traveling.Models;

namespace Traveling.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        
        private readonly RoteiroService _roteiroService;
        private readonly NotificationService _notificationService;

        public Command AboutCommand { get; private set; }
        public Command<Roteiro> ShowRoteiroCommand { get; private set; }

        public ObservableCollection<Models.Roteiro> Roteiros { get; }

        public MainViewModel(IRoteiroService roteiroService
                             , NotificationService notificationService)
        {
            _roteiroService = roteiroService as RoteiroService;
            _notificationService = notificationService;

            Roteiros = new ObservableCollection<Models.Roteiro>();

            AboutCommand = new Command(ExecuteAboutCommand);

            ShowRoteiroCommand = new Command<Roteiro>(ExecuteShowRoteiroCommand);

            RegisterNotification();

            LoadRoteiros();

        }

        protected override async void CurrentPageOnAppearing(object sender, EventArgs eventArgs)
        {
            //if (user == null)
            //{
            //    await DisplayAlert("Bem vindo", "Efetue o login para visualizar seus roteiros e boa viagem!", "OK");
            //}
            //if (!tryFirstAuth)
            //{
            //    tryFirstAuth = true;
            //
            //    await ExecuteLogin();
            //}
        }

        private async void RegisterNotification()
        {
            await _notificationService.Register();
        }

		private async void ExecuteAboutCommand()
		{
            await PushAsync<AboutViewModel>();
		}

        private async void ExecuteShowRoteiroCommand(Roteiro roteiro)
        {
            await PushAsync<RoteiroViewModel>(_roteiroService, roteiro);
        }

        private void LoadRoteiros()
        {
            var roteiros = _roteiroService.GetRoteiros();

            foreach (var r in roteiros)
            {
                Roteiros.Add(r);
            }

        }
    }
}
