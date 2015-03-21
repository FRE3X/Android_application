using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4;
using Android.Support.V4.Widget;
using System.Collections.Generic; 


namespace testFragmentWithDrawerLayout
{
	[Activity (Label = "testFragmentWithDrawerLayout", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		DrawerLayout mDrawerLayout;
		List<string> listItem = new List<string>();
		ArrayAdapter mAdapter; 
		ListView mDrawer;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.dialog_frame);

			mDrawerLayout = FindViewById<DrawerLayout> (Resource.Id.drawer_layout); 
			mDrawer = FindViewById<ListView> (Resource.Id.lvDrawer);
			Button button = FindViewById<Button> (Resource.Id.myButton);

			listItem.Add ("machin"); 
			listItem.Add ("Truc"); 

			mAdapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1,  listItem); 
			mDrawer.Adapter = mAdapter; 

			button.Click += delegate {

			};
		}
	}
}


