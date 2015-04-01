using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Support.V4; 
using Android.Widget;
using Android.OS;

namespace testfragement
{
	[Activity (Label = "testfragement", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				FragmentTransaction transaction = FragmentManager.BeginTransaction(); 
				Dialog dialog = new Dialog(); 
				dialog.Show(transaction, "Dialog_Frame"); 
			};
		}
	}
}


