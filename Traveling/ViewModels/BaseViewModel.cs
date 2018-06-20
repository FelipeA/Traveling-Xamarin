using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Traveling.Interfaces;
using Xamarin.Forms;

namespace Traveling.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

        protected Page CurrentPage { get; private set; }

		protected virtual void onPropertyChanged([CallerMemberName]string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(storage, value))
			{
				return false;
			}

			storage = value;
			onPropertyChanged(propertyName);

			return true;
		}

		public void Initialize(Page page)
		{
			CurrentPage = page;

			CurrentPage.Appearing += CurrentPageOnAppearing;
			CurrentPage.Disappearing += CurrentPageOnDisappearing;
		}

		protected virtual void CurrentPageOnAppearing(object sender, EventArgs eventArgs) { }

		protected virtual void CurrentPageOnDisappearing(object sender, EventArgs eventArgs) { }

		public async Task PushAsync<TViewModel>(params object[] args) where TViewModel : BaseViewModel
		{
			var viewModelType = typeof(TViewModel);
			var viewModelTypeName = viewModelType.Name;
			var viewModelWordLength = "ViewModel".Length;
			var viewTypeName = $"Traveling.{viewModelTypeName.Substring(0, viewModelTypeName.Length - viewModelWordLength)}Page";
			var viewType = Type.GetType(viewTypeName);

			var page = Activator.CreateInstance(viewType) as Page;

			if (viewModelType.GetTypeInfo().DeclaredConstructors.Any(c => c.GetParameters().Any(p => p.ParameterType == typeof(IAzureService))))
			{
				var argsList = args.ToList();
				var monkeyHubApiService = DependencyService.Get<IAzureService>();
				argsList.Insert(0, monkeyHubApiService);
				args = argsList.ToArray();
			}

			var viewModel = Activator.CreateInstance(viewModelType, args);
			if (page != null)
			{
				page.BindingContext = viewModel;
			}

			await Application.Current.MainPage.Navigation.PushAsync(page);
		}

		public async Task DisplayAlert(string title, string message, string cancel)
		{
			await Application.Current.MainPage.DisplayAlert(title, message, cancel);
		}

		public async Task DisplayAlert(string title, string message, string accept, string cancel)
		{
			await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
		}
	}
}
