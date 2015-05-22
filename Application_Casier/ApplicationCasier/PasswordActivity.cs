
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ApplicationCasier
{
	[Activity (Label = "Vos mots de passe :")]			
	public class PasswordActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ActivityPassword); 

			// Modification du menu de l'action bar :
			ActionBar.SetDisplayHomeAsUpEnabled (true); 
			ActionBar.SetHomeButtonEnabled (true); 
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

