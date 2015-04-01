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

namespace ApplicationCasier
{
	[Activity (Label = "Safe Locker", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppBaseTheme")]
	public class MainActivity : Activity 
	{
		DrawerLayout mDrawerLayout; 
		List<string> listItem = new List<string>();
		ArrayAdapter mAdapter; 
		ListView mDrawer;
		User_Application Ap;
		RelativeLayout loading; 
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			//mise en place de l'ip et du port : 
			Ap = new User_Application(GetString(Resource.String.ip),int.Parse(GetString(Resource.String.port))); 

			mDrawerLayout = FindViewById<DrawerLayout> (Resource.Id.myDrawer); 
			mDrawer = FindViewById<ListView> (Resource.Id.ListView); 
			var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
			//loading = FindViewById<RelativeLayout> (Resource.Id.loading_layout); 
			SetActionBar (toolbar);

			toolbar.SetNavigationIcon (Resource.Drawable.ic_launcher); 
			toolbar.InflateMenu (Resource.Menu.menu); 
			//on donne le nom des item : 
			listItem.Add ("Actualiser");
			listItem.Add ("Mot de passe"); 
			listItem.Add ("Réservation");

			mAdapter = new ArrayAdapter (this,Resource.Layout.SingleRow,Resource.Id.tvTitle, listItem); 
			mDrawer.Adapter = mAdapter; 

			mDrawer.ItemClick += listView_ItemClick;
			//loading.Visibility = ViewStates.Invisible; 

		}

		//Creation de bouton dans la toolbar 
		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.menu, menu);
			return base.OnCreateOptionsMenu (menu);
		}

		void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			//récupération de l'objet séléctionner dans la listView 
			var item = this.mDrawer.GetItemAtPosition(e.Position);

			switch (item.ToString()) { 
			case "Actualiser":
				//handler est associé a MainActivity :
				Handler handler = new Handler (); 

				//Creation du thread :
				new Thread (delegate(){

						 	
					try {
						//Appelle de la méthode actualiser : 
						Ap.Actualiser ();

					
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
				
				break; 
			case "Réservation": 

				new Thread (delegate () {
					char[] numero_casier = Ap.DemandeCasier (); 

					FragmentTransaction transaction = FragmentManager.BeginTransaction (); 
					Dialog_Reservation dialog = new Dialog_Reservation (); 
					dialog.Show (transaction, "dialog_Reservation"); 

					 	
				
				}).Start (); 

				break;
			}

		}


	}
}


