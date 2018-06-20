using System;
namespace Traveling.Models
{
    public class RoteiroItem
    {
        public int ID
        {
            get;
            set;
        }

        public int RoteiroID
        {
            get;
            set;
        }

        public string Descricao
        {
            get;
            set;
        }

        public DateTime Data
        {
            get;
            set;
        }

        public string Street
        {
            get;
            set;
        }

		public string City
		{
			get;
			set;
		}
		public string State
		{
			get;
			set;
		}
		public string ZipCode
		{
			get;
			set;
		}
		public string Country
		{
			get;
			set;
		}
		public string CountryCode
		{
			get;
			set;
		}
    }
}
