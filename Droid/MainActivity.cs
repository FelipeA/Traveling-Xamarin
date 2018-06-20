using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Gcm.Client;

namespace Traveling.Droid
{
    [Activity(Label = "Traveling", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
		// Create a new instance field for this activity.
		static MainActivity instance = null;

		// Return the current activity instance.
		public static MainActivity CurrentActivity
		{
			get
			{
				return instance;
			}
		}

        protected override void OnCreate(Bundle bundle)
        {
            instance = this;

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());

			
        }

		public void CreateAndShowDialog(string message, string title)
		{
			var builder = new AlertDialog.Builder(this);

			builder.SetMessage(message);
			builder.SetTitle(title);
			builder.Create().Show();
		}
    }
}
