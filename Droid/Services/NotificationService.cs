using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Util;
using Gcm.Client;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Android.Support.V4.App;
using Android.Media;
using System.Text;
using Xamarin.Forms;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]

[assembly: Dependency(typeof(Traveling.Droid.NotificationService))]
namespace Traveling.Droid
{
	[BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "@PACKAGE_NAME@" })]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "@PACKAGE_NAME@" })]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "@PACKAGE_NAME@" })]
	public class PushHandlerBroadcastReceiver : GcmBroadcastReceiverBase<NotificationService>
	{
		public static string[] SENDER_IDS = new string[] { Traveling.Helpers.Constants.Sender_Id };
	}

    [Service]
    public class NotificationService : GcmServiceBase, Traveling.Interfaces.INotificationService
    {
        MobileServiceClient client = new MobileServiceClient(Traveling.Helpers.Constants.ApplicationURL);
        
		public static string RegistrationID { get; private set; }

		public NotificationService()
			: base(PushHandlerBroadcastReceiver.SENDER_IDS) { }

		public static async Task<bool> RegisterService(Context context)
		{
			try
			{
				// Check to ensure everything's set up right
				GcmClient.CheckDevice(context);
				GcmClient.CheckManifest(context);

				// Register for push notifications
				System.Diagnostics.Debug.WriteLine("Registering...");
				GcmClient.Register(context, PushHandlerBroadcastReceiver.SENDER_IDS);
				return await Task.Run(() => true);
			}
			catch (Java.Net.MalformedURLException) 
			{
                if (context is MainActivity)
				    (context as MainActivity).CreateAndShowDialog("There was an error creating the client. Verify the URL.", "Error");

                return await Task.Run(() => false);
			}
			catch (Exception e)
			{
				if (context is MainActivity)
					(context as MainActivity).CreateAndShowDialog(e.Message, "Error");

                return await Task.Run(() => false);
			}
		}

		public async Task RegisterNotificationAsync()
		{
			MainActivity.CurrentActivity.RunOnUiThread
			   (
				   async () => await RegisterService(MainActivity.CurrentActivity)
			   );
            
			await Task.Run(() => true);
		}

        public async void Register(Microsoft.WindowsAzure.MobileServices.Push push, IEnumerable<string> tags)
		{
			try
			{
				const string templateBodyGCM = "{\"data\":{\"message\":\"$(messageParam)\"}} ";
				JObject templates = new JObject();
				templates["genericMessage"] = new JObject
						{
						{"body", templateBodyGCM}
						};
				await push.RegisterAsync(RegistrationID, templates);
				Log.Info("Push Installation Id", push.InstallationId.ToString());
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				Debugger.Break();
			}
		}

		protected override void OnRegistered(Context context, string registrationId)
		{
			Log.Verbose("PushHandlerBroadcastReceiver", "GCM Registered: " + registrationId);
			RegistrationID = registrationId;
			var push = client.GetPush();
			
            MainActivity.CurrentActivity.RunOnUiThread(() => Register(push, null));
		}

        protected override void OnMessage(Context context, Intent intent)         {
            Log.Info("PushHandlerBroadcastReceiver", "GCM Message Received!");

            var msg = new StringBuilder();              if (intent != null && intent.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                    msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());             }              //Store the message             var prefs = GetSharedPreferences(context.PackageName, FileCreationMode.Private);             var edit = prefs.Edit();             edit.PutString("last_msg", msg.ToString());             edit.Commit();              string message = intent.Extras.GetString("message");              if (!string.IsNullOrEmpty(message))             {               CreateNotification("Traveling", message);               return;             }

            string msg2 = intent.Extras.GetString("msg");
            if (!string.IsNullOrEmpty(msg2))             { 
                CreateNotification("Traveling", msg2);
                return;             } 
            CreateNotification("Unknown message details", msg.ToString());
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Error("PushHandlerBroadcastReceiver", "Unregistered RegisterationId : " + registrationId);
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error("PushHandlerBroadcastReceiver", "GCM Error: " + errorId);         }

        private void CreateNotification(string title, string desc)
        {
            //Create notification
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            //Create an intent to show ui
            var uiIntent = new Intent(this, typeof(MainActivity));              //Use Notification Builder
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this);              //Create the notification
            //we use the pending intent, passing our ui intent over which will get called
            //when the notification is tapped.
            var notification = builder.SetContentIntent(PendingIntent.GetActivity(this, 0, uiIntent, 0))
                                      .SetSmallIcon(Android.Resource.Drawable.SymActionEmail)
                                      .SetTicker(title)
                                      .SetContentTitle(title)
                                      .SetContentText(desc)

                                      //Set the notification sound
                                      .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))

                                      //Auto cancel will remove the notification once the user touches it
                                      .SetAutoCancel(true).Build();              //Show the notification             notificationManager.Notify(1, notification);        } 
    }
}
