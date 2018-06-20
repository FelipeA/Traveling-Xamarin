using System;
namespace Traveling.ViewModels
{
    public class ContentWebViewModel : BaseViewModel
    {
        public string linkedInURLProfile { get; }

		public ContentWebViewModel()
		{
            linkedInURLProfile = "https://www.linkedin.com/in/felipe-augusto-89123a6";
		}
    }
}
