using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;

namespace ApplicationCasier
{
	[Activity (Label = "Options")]			
	public class OptionsActivity : Activity
	{
		EditText ip_text; 
		EditText port_text;
		Button button_save; 
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ActivityOption); 

			ip_text = FindViewById<EditText> (Resource.Id.ip_EditText); 
			port_text = FindViewById<EditText> (Resource.Id.port_EditText);
			button_save = FindViewById<Button> (Resource.Id.button_save); 
	
			// Modification du menu de l'action bar :
			ActionBar.SetDisplayHomeAsUpEnabled (true); 
			ActionBar.SetHomeButtonEnabled (true);

			button_save.Click += delegate {
				FilesAcces file = new FilesAcces(); 
				file.modify_options(ip_text.Text,port_text.Text);

			};
		}

		//recuperation du click dans la barre du menu 
		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			//quand l'item est selectionner, renvoi a home : 
			switch (item.ItemId)
			{
			case Android.Resource.Id.Home:
				Finish();
				return true;

			default:
				return base.OnOptionsItemSelected(item);
			}
		}

	}





}



