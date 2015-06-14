using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Files
{
	[Activity (Label = "FilesAcces", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		FilesAcces files; 
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				files = new FilesAcces ();
				bool b = files.write_casier ("s", "t");
				files.modify_options("192.168.1.1","2344"); 
				string r = files.read_port(); 
				Toast.MakeText(this,r,ToastLength.Short).Show(); 
			};
		}
	}
}


