using System;

using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Graphics;

namespace WearNotifications
{
	[Activity (Label = "WearNotifications", MainLauncher = true, Icon = "@drawable/ic_launcher")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var intent = new Intent (this, typeof(MainActivity));
			var contentIntent = PendingIntent.GetActivity (this, 0, intent, PendingIntentFlags.CancelCurrent);
			var manager = NotificationManagerCompat.From (this);

		

			var title = "I <3 C#";
			var message = "Seattle Code Camp rocks!";
			var messageLong = "Seattle Code Camp is awesome, I can't wait to see the Xamarin.Forms Demo later! From what I hear it is going to be the best thing ever!";


		


			//Standard Notification
			FindViewById<Button>(Resource.Id.notification).Click += (sender, e) => {

				var style = new NotificationCompat.BigTextStyle();
				style.BigText(messageLong);

				//Generate a notification with just short text and small icon
				var builder = new NotificationCompat.Builder(this)
					.SetContentIntent(contentIntent)
					.SetSmallIcon(Resource.Drawable.ic_stat_hexagon_blue)
					.SetContentTitle(title)
					.SetContentText(message)
					.SetStyle(style)
					.SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
					.SetAutoCancel(true);


				var notification = builder.Build();
				manager.Notify(0, notification);
			};

			//Standard action with action
			FindViewById<Button>(Resource.Id.notification_action).Click += (sender, e) => {
					
					
				NotificationCompat.Action reply =
					new NotificationCompat.Action.Builder(Resource.Drawable.ic_stat_social_reply,
						"Reply", contentIntent)
						.Build();

				var style = new NotificationCompat.BigTextStyle();
				style.BigText(messageLong);

				//Generate a notification with just short text and small icon
				var builder = new NotificationCompat.Builder(this)
					.SetContentIntent(contentIntent)
					.SetSmallIcon(Resource.Drawable.ic_stat_hexagon_blue)
					.SetContentTitle(title)
					.SetContentText(message)
					.SetStyle(style)
					.SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
					.SetAutoCancel(true)
					.AddAction(reply);


				var notification = builder.Build();
				manager.Notify(0, notification);
			};


			//Special Button for Wear
			FindViewById<Button>(Resource.Id.notification_wear_action).Click += (sender, e) => {

				NotificationCompat.Action favorite =
					new NotificationCompat.Action.Builder(Resource.Drawable.ic_stat_rating_favorite,
						"Favorite", contentIntent)
						.Build();

				NotificationCompat.Action reply =
					new NotificationCompat.Action.Builder(Resource.Drawable.ic_stat_social_reply,
						"Reply", contentIntent)
						.Build();


				var style = new NotificationCompat.BigTextStyle();
				style.BigText(messageLong);

				//Generate a notification with just short text and small icon
				var builder = new NotificationCompat.Builder(this)
					.SetContentIntent(contentIntent)
					.SetSmallIcon(Resource.Drawable.ic_stat_hexagon_blue)
					.SetContentTitle(title)
					.SetContentText(message)
					.SetStyle(style)
					.SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
					.SetAutoCancel(true)
					.AddAction(reply)
					.Extend(new NotificationCompat.WearableExtender().AddActions(new []{reply, favorite}));


				var notification = builder.Build();
				manager.Notify(0, notification);

			};

			//Custom Background
			FindViewById<Button>(Resource.Id.notification_wear_background).Click += (sender, e) => {


				var action = new NotificationCompat.Action.Builder(Resource.Drawable.ic_stat_rating_favorite,
						"Favorite", contentIntent)
						.Build();

				var wearableExtender = new NotificationCompat.WearableExtender()
					.SetHintHideIcon(true)
						.SetBackground(BitmapFactory.DecodeResource(Resources, Resource.Drawable.ic_background))
						.AddAction(action)
					;

				var style = new NotificationCompat.BigTextStyle();
				style.BigText(messageLong);

				//Generate a notification with just short text and small icon
				var builder = new NotificationCompat.Builder(this)
					.SetContentIntent(contentIntent)
					.SetSmallIcon(Resource.Drawable.ic_stat_hexagon_blue)
					.SetContentTitle(title)
					.SetContentText(message)
					.SetStyle(style)
					.SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
					.SetAutoCancel(true)
					.AddAction(Resource.Drawable.ic_stat_social_reply,
						"Reply", contentIntent)
					.Extend(wearableExtender);


				var notification = builder.Build();
				manager.Notify(0, notification);
			};

			//Pages
			FindViewById<Button>(Resource.Id.notification_wear_pages).Click += (sender, e) => {
				// Create builder for the main notification
				var notificationBuilder =
					new NotificationCompat.Builder(this)
						.SetSmallIcon(Resource.Drawable.ic_stat_hexagon_blue)
						.SetContentTitle("Page 1")
						.SetContentText("Short message")
						.SetContentIntent(contentIntent);

				// Create a big text style for the second page
				var secondPageStyle = new NotificationCompat.BigTextStyle();
				secondPageStyle.SetBigContentTitle("Page 2")
					.BigText(messageLong);

				// Create second page notification
				var secondPageNotification = new NotificationCompat.Builder(this)
						.SetStyle(secondPageStyle)
						.Build();

				// Add second page with wearable extender and extend the main notification
				var twoPageNotification =
					new NotificationCompat.WearableExtender()
						.AddPage(secondPageNotification)
						.Extend(notificationBuilder)
						.Build();

				// Issue the notification
				manager.Notify(0, twoPageNotification);
			};

			//Stacked Notifications
			FindViewById<Button>(Resource.Id.notification_wear_stacked).Click += (sender, e) => {

				var wearableExtender =
					new NotificationCompat.WearableExtender()
						.SetBackground(BitmapFactory.DecodeResource(Resources, Resource.Drawable.ic_background));

				// Build the notification, setting the group appropriately
				var notificaiton1 = new NotificationCompat.Builder(this)
					.SetContentTitle("New mail from James")
					.SetContentText(message)
					.SetSmallIcon(Resource.Drawable.ic_stat_hexagon_blue)
					.SetGroup("group_1")
					.Extend(wearableExtender)
					.Build();


				var notification2 = new NotificationCompat.Builder(this)
					.SetContentTitle("New mail from ChrisNTR")
					.SetContentText("this is subject #1")
					.SetSmallIcon(Resource.Drawable.ic_stat_hexagon_blue)
					.SetGroup("group_1")
					.Extend(wearableExtender)
					.Build();

				manager.Notify(0, notificaiton1);
				manager.Notify(1, notification2);
			};

			//Voice Input
			FindViewById<Button>(Resource.Id.notification_wear_voice).Click += (sender, e) => {
			

				var remoteInput = new Android.Support.V4.App.RemoteInput.Builder(ExtraVoiceReply)
					.SetLabel("Reply")
					.SetChoices(new []{"Yes", "No", "OK you win"})
					.Build();

				NotificationCompat.Action reply =
					new NotificationCompat.Action.Builder(Resource.Drawable.ic_stat_social_reply,
						"Reply", contentIntent)
						.AddRemoteInput(remoteInput)

						.Build();


				var wearableExtender = new NotificationCompat.WearableExtender();
				wearableExtender.AddAction(reply);


				//Generate a notification with just short text and small icon
				var builder = new NotificationCompat.Builder(this)
					.SetContentIntent(contentIntent)
					.SetSmallIcon(Resource.Drawable.ic_stat_hexagon_blue)
					.SetContentTitle(title)
					.SetContentText(message)
					.SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
					.SetAutoCancel(true)
					.AddAction(reply)
					.Extend(wearableExtender);


				var notification = builder.Build();
				manager.Notify(0, notification);
			};

			var voiceReply = GetMessageText (Intent);
			if (!string.IsNullOrWhiteSpace (voiceReply))
				FindViewById<TextView> (Resource.Id.textView1).Text = voiceReply;
		}

		private const string ExtraVoiceReply = "voice_reply";


		private string GetMessageText(Intent intent) {
			Bundle remoteInput = Android.Support.V4.App.RemoteInput.GetResultsFromIntent(intent);
			if (remoteInput != null) {
				return remoteInput.GetCharSequence(ExtraVoiceReply);
			}
			return null;
		}
	}
}


