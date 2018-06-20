using System;
using System.Collections.ObjectModel;
using Plugin.ExternalMaps;
using Traveling.Models;
using Traveling.Services;
using Xamarin.Forms;

namespace Traveling.ViewModels
{
    public class RoteiroViewModel : BaseViewModel
    {
        private RoteiroService _roteiroService;
        public Roteiro _roteiro { get; }
        public ObservableCollection<RoteiroItem> RoteiroItens { get; private set; }
        public Command<RoteiroItem> ShowRoteiroItemCommand { get; set; }
        
        public RoteiroViewModel(RoteiroService roteiroService, Roteiro roteiro)
        {
            _roteiroService = roteiroService;
            _roteiro = roteiro;

            ShowRoteiroItemCommand = new Command<RoteiroItem>(ExecuteShowRoteiroItemCommand);

            RoteiroItens = new ObservableCollection<RoteiroItem>(); 
            LoadRoteiros();
        }

		private void LoadRoteiros()
		{
			var roteiroItens = _roteiroService.GetRoteiroItens(_roteiro.ID);

			foreach (var r in roteiroItens)
			{
				RoteiroItens.Add(r);
			}

		}

        public async void ExecuteShowRoteiroItemCommand(RoteiroItem roteiroItem)
        {
            //await CrossExternalMaps.Current.NavigateTo("Space Needle", 47.6204, -122.3491);
            await CrossExternalMaps.Current.NavigateTo(roteiroItem.Descricao
                                                       , roteiroItem.Street
                                                       , roteiroItem.City
                                                       , roteiroItem.State
                                                       , roteiroItem.ZipCode
                                                       , roteiroItem.Country
                                                       , roteiroItem.CountryCode);

        }
    }
}
