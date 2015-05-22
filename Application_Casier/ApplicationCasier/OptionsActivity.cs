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
		XmlDocument xml;
		XmlNodeList node_list;
		private Context context; 
		protected override void OnCreate (Bundle bundle)
		{
			string content; 
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ActivityOption); 

			ip_text = FindViewById<EditText> (Resource.Id.ip_EditText); 
			port_text = FindViewById<EditText> (Resource.Id.port_EditText);
			//valeur_default (); 

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

		public void valeur_default(){
			xml.Load (Assets.Open ("Options.xml")); 
			node_list = xml.SelectNodes ("/server"); 

			foreach (XmlNode node in node_list) 
			{
				try
				{                    
					ip_text.Text = node.SelectSingleNode("ip").InnerText;
					port_text.Text = node.SelectSingleNode("port").InnerText;

				}
				catch (Exception ex)
				{
					Toast toast = Toast.MakeText (this, "Erreur de lecture du fichier d'options", ToastLength.Short);
					toast.SetGravity (GravityFlags.Center, 0, 0);
					toast.Show();
				}
			}
		}
	}
}



