using System;

using Xamarin.Forms;

namespace NWVDNUGMeetup
{
	public class App : Application
	{
		public App ()
		{
			//var serverData = new MeetupData ();
			//var info = serverData.GetMeetingData ();

			// The root page of your application
			MainPage = new Views.MeetingList ();
			//MainPage.BindingContext = info;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
			var serverData = new MeetupData ();
			var info = serverData.GetMeetingData ();

			// The root page of your application
			MainPage.BindingContext = info;
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

