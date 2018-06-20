using System;
using Android.App;
using Android.Support.V7.App;

namespace Traveling.Droid
{
	[Activity(Label = "Traveling", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true)]
	public class SplashActivity : AppCompatActivity
	{
		protected override void OnResume()
		{
			base.OnResume();
			StartActivity(typeof(MainActivity));
		}
	}
}
