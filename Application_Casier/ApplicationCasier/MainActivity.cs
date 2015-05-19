using System;
using System.Threading; 
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Support.V4.Widget;
using System.Net.Sockets;

using Android.Support.V4.App;
 

namespace ApplicationCasier
{
	[Activity (Label = "Safe Locker", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppBaseTheme")]
	public class MainActivity : Activity 
	{
		DrawerLayout mDrawerLayout; 
		List<string> listItem = new List<string>();
		ArrayAdapter mAdapter; 
		ListView mDrawer;
		User_Application client;
		RelativeLayout loading; 
		ActionBarDrawerToggle DrawerToggle; 
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);

			//mise en place de l'ip et du port : 
			client = new User_Application(); 

			mDrawerLayout = FindViewById<DrawerLayout> (Resource.Id.myDrawer); 
			mDrawer = FindViewById<ListView> (Resource.Id.ListView); 
			loading = FindViewById<RelativeLayout> (Resource.Id.loading_layout); 

			//on donne le nom des item : 
			listItem.Add ("Actualiser");
			listItem.Add ("Mot de passe"); 
			listItem.Add ("Réservation");

			DrawerToggle = new ActionBarDrawerToggle (this, mDrawerLayout, Resource.Drawable.menu, Resource.String.open_drawer, Resource.String.close_drawer); 
			mAdapter = new ArrayAdapter (this,Resource.Layout.SingleRow,Resource.Id.tvTitle, listItem); 

			mDrawerLayout.SetDrawerListener (DrawerToggle); 

			//mise en place de l'adaptateur pour la ListView : 
			mDrawer.Adapter = mAdapter; 

			//recupération du click dans le mdrawer : 
			mDrawer.ItemClick += listView_ItemClick;
			loading.Visibility = ViewStates.Gone;

			//Modification du menu de ActionBar : 
			ActionBar.SetDisplayHomeAsUpEnabled (true); 
			ActionBar.SetHomeButtonEnabled (true); 


		}

		//mise en place du boutton pour l'activation du DrawerLayout : 
		protected override void OnPostCreate (Bundle savedInstance){
			base.OnPostCreate (savedInstance); 
			DrawerToggle.SyncState (); 
		
		}

		//Creation de bouton dans la toolbar 
		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.menu, menu);
			return base.OnCreateOptionsMenu (menu);
		}

		// recuperation du click dans la barre du  menu 
		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			base.OnOptionsItemSelected (item); 

			if(DrawerToggle.OnOptionsItemSelected(item)){
				return true; 
			}

			switch (item.ItemId)
			{
			case Resource.Id.action_settings: 
				Intent intent = new Intent (this, typeof(OptionsActivity));
				this.StartActivity(intent); 
				break; 

			}

			return true; 		
		}

		// Si un objet est cliquer dans le drawer layout
		void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var item = this.mDrawer.GetItemAtPosition(e.Position);//récupération de l'objet séléctionner dans la listView 
			switch (item.ToString()) { 
			case "Actualiser":
				//ferme le mdDrawerLayout : 
				mDrawerLayout.CloseDrawer (mDrawer); 
				//handler est associé a MainActivity :
				Handler handler = new Handler (); 

				//Creation du thread :
				new Thread (delegate(){
					try {
						//Appelle de la méthode actualiser : 
						client.connection(); 
					} catch (SocketException j) {//Capture de SocketException
					// Quand le handler est appellé il géle MainActiviy le temps d'afficher le toast : 
						handler.Post(()=> {
							Toast toast = Toast.MakeText (this, j.Message, ToastLength.Short);
							toast.SetGravity (GravityFlags.Center, 0, 0);
							toast.Show();

						});
					}				
				}).Start();
				break;

			case "Mot de passe": 
				mDrawerLayout.CloseDrawer (mDrawer);//ferme le mdDrawerLayout : 	
				break; 
			
			case "Réservation": 
				mDrawerLayout.CloseDrawer (mDrawer);//ferme le mdDrawerLayout : 
					
				Handler handler_reservation = new Handler ();
				Thread thread_reservation = new Thread (delegate () {

					try {
						char[] numero_casier = client.DemandeCasier ();
						string numero = new string (numero_casier);
						// pour debug : 
						//string numero = "12"; 
						//Affichage du dialog frame : 
						Android.App.FragmentTransaction transaction = FragmentManager.BeginTransaction (); 
						Dialog_Reservation dialog = new Dialog_Reservation ();
						//transmission  du numero de casier : 
						dialog.modifier_num_casier (numero); 
						//transmission du client : 
						dialog.GetUserApplication(client); 
						//Affichage de la fenêtre de dialog : 
						dialog.Show (transaction, "dialog_Reservation");
					 						
					}catch (SocketException i) {//Capture de SocketException
						// Quand le handler est appellé il géle MainActiviy le temps d'afficher le toast : 
						handler_reservation.Post (() => {
							Toast toast = Toast.MakeText (this, i.Message, ToastLength.Short);
							toast.SetGravity (GravityFlags.Center, 0, 0);
							toast.Show ();

						});
					}
				});
				thread_reservation.Start (); 

				break;
			}

		}


	}
}


