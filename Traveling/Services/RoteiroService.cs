using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Traveling.Interfaces;
using Traveling.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(Traveling.Services.RoteiroService))]
namespace Traveling.Services
{
    public class RoteiroService : IRoteiroService
    {
        private List<Roteiro> Roteiros { get; set; }
        private List<RoteiroItem> RoteiroItens { get; set; }

        public RoteiroService()
        {
            Roteiros = new List<Roteiro>();
            RoteiroItens = new List<RoteiroItem>();
            
            LoadRoteiros();
            LoadRoterioItens();
        }

        public List<RoteiroItem> GetRoteiroItens(int roteiroID)
        {
            return RoteiroItens.FindAll(x => x.RoteiroID == roteiroID);
        }

        public List<Roteiro> GetRoteiros()
        {
            return Roteiros;
        }

        private void LoadRoteiros()
        {
			Roteiros.Add(new Roteiro()
			{
				ID = 3,
				Nome = "Miami",
				Data = new DateTime(2017, 10, 20)
			});

			Roteiros.Add(new Roteiro()
			{
				ID = 2,
				Nome = "California e Las Vegas",
				Data = new DateTime(2015, 10, 14)
			});

            Roteiros.Add(new Roteiro(){
                ID = 1,
                Nome = "Londres e Paris",
                Data = new DateTime(2013, 08, 18)
            });

        }

        private void LoadRoterioItens()
        {
            RoteiroItens.Add(new RoteiroItem()
            {
                ID = 1,
                RoteiroID = 1,
                Descricao = "Big Ben",
                Data = new DateTime(2013, 08, 14),
                Street = "Westminster Londres SW1A 0AA",
                City = "London",
                State = "",
                ZipCode = "",
                Country = "UK",
                CountryCode = "UK"
            });
            RoteiroItens.Add(new RoteiroItem()
            {
                ID = 2,
                RoteiroID = 1,
                Descricao = "London Eye",
                Data = new DateTime(2013, 08, 14),
                Street = "Lambeth, Londres SE1 7PB",
                City = "London",
				State = "",
				ZipCode = "",
				Country = "UK",
				CountryCode = "UK"
            });
            RoteiroItens.Add(new RoteiroItem()
            {
                ID = 3,
                RoteiroID = 1,
                Descricao = "Palácio de Westminster",
                Data = new DateTime(2013, 08, 14),
				//Endereco = ", Reino Unido"
				Street = "Westminster, London SW1A 2PW",
                City = "London",
				State = "",
				ZipCode = "",
				Country = "UK",
				CountryCode = "UK"
            });
            RoteiroItens.Add(new RoteiroItem()
            {
                ID = 4,
                RoteiroID = 1,
                Descricao = "Palácio de Buckingham",
                Data = new DateTime(2013, 08, 14),
				//Endereco = "Westminster, Londres SW1A 1AA, Reino Unido"
				Street = "Westminster, London SW1A 1AA",
                City = "London",
				State = "",
				ZipCode = "",
				Country = "UK",
				CountryCode = "UK"
            });
			
            RoteiroItens.Add(new RoteiroItem()
			{
				ID = 5,
				RoteiroID = 1,
				Descricao = "Torre Eiffel",
				Data = new DateTime(2013, 08, 15),
				//Endereco = "Westminster, Londres SW1A 1AA, Reino Unido"
				Street = "Champ de Mars, 5 Avenue Anatole France",
                City = "Paris",
				State = "",
				ZipCode = "75007",
				Country = "France",
				CountryCode = ""
			});
			RoteiroItens.Add(new RoteiroItem()
			{
				ID = 6,
				RoteiroID = 1,
				Descricao = "Champs-Élysées",
				Data = new DateTime(2013, 08, 15),
				//Endereco = "Avenue des Champs-Élysées"
				Street = "Avenue des Champs-Élysées",
				City = "Paris",
				State = "",
				ZipCode = "",
				Country = "France",
				CountryCode = ""
			});
			RoteiroItens.Add(new RoteiroItem()
			{
				ID = 7,
				RoteiroID = 1,
				Descricao = "Museu do Louvre",
				Data = new DateTime(2013, 08, 16),
				//Endereco = "Rue de Rivoli, 75001 Paris, França"
				Street = "Rue de Rivoli",
				City = "Paris",
				State = "",
				ZipCode = "75001",
				Country = "France",
				CountryCode = ""
			});

        }
    }
}
