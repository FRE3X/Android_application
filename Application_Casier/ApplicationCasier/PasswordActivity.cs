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
		
		//TextView mdp1,mdp2,mdp3; 
		//TextView c1,c2,c3;
		List<string> list_mdp = new List<string>();  
		List<string> list_locker = new List<string>();  
		ArrayAdapter mAdaptater_mdp;
		ArrayAdapter mAdaptateur_locker; 
		ListView listview_mdp;
		ListView listview_locker; 
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ActivityPassword); 

			listview_mdp = FindViewById<ListView> (Resource.Id.ListView_mdp); 
			listview_locker = FindViewById<ListView> (Resource.Id.ListView_casier); 

			// Modification du menu de l'action bar :
			ActionBar.SetDisplayHomeAsUpEnabled (true); 
			ActionBar.SetHomeButtonEnabled (true); 
			recuperation (); 
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
		public void recuperation(){
			FilesAcces files = new FilesAcces ();
			files.clear_locker (); 
			string recup = files.read_casiers (); 


			string[] split = recup.Split ('|'); 
			//parcours le tableau "split"
			for(int i = 0; i < split.Length; i++) {
				//si le string dispose de 4 caractére alors c'est le mdp
				if (split [i].Length == 4) {
					list_mdp.Add("mot de passe : " + split[i]); 
				}
				//si le string ne fais que de 2 caractére alors il s'agit
				//du numéro de casier
				if (split [i].Length == 2) {
					list_locker.Add ("n° de casier : " + split [i]); 
				}
			
			}
			// l'adaptateur pour la listview des mots de passe :
			mAdaptater_mdp = new ArrayAdapter (this, 
										   Resource.Layout.LockerRow, 
										   Resource.Id.textview_1, 
										   list_mdp); 
			mAdaptateur_locker = new ArrayAdapter (this, 
										   Resource.Layout.LockerRow,
						 				   Resource.Id.textview_1,
										   list_locker); 
			listview_mdp.Adapter = mAdaptater_mdp; 
			listview_locker.Adapter = mAdaptateur_locker;
		}
	}
}

