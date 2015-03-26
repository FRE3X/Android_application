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
using Android.Support.V4;

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
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			//mise en place de l'ip et du port : 
			Ap = new User_Application(GetString(Resource.String.ip),int.Parse(GetString(Resource.String.port))); 
			var newFragment = new Myfragment (); 
			mDrawerLayout = FindViewById<DrawerLayout> (Resource.Id.myDrawer); 
			mDrawer = FindViewById<ListView> (Resource.Id.ListView); 
			var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
			SetActionBar (toolbar); 

			toolbar.SetNavigationIcon (Resource.Drawable.ic_launcher); 
			toolbar.InflateMenu (Resource.Menu.menu); 
			//on donne le nom des item : 
			listItem.Add ("Actualiser");
			listItem.Add ("Mot de passe"); 
			listItem.Add ("Réservation");

			mAdapter = new ArrayAdapter (this,Resource.Layout.SingleRow,Resource.Id.tvTitle, listItem); 
			mDrawer.Adapter = mAdapter;

			//var ft = FragmentManager.BeginTransaction ();
			//ft.Add (Resource.Id.fragment_container, newFragment);
			//ft.Commit ();

			mDrawer.ItemClick += listView_ItemClick;

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
			// En cas d'appui sur le boutton actualiser
			case "Actualiser":

				//handler est associé a MainActivity :
				Handler handler = new Handler (); 

				//Creation du thread :
				new Thread (delegate(){

					SocketException return_Exception = new SocketException (); 
					 	
					try {
						//Appelle de la méthode actualiser : 
						Ap.EnvoiAddrMAC ();

					
					} catch (SocketException j) {//Capture de SocketException
					
					// Quand le handler est appellé il géle MainActiviy le temps d'afficher le toast : 
						handler.Post(()=> {
							Toast toast = Toast.MakeText (this, j.Message, ToastLength.Short);
							toast.SetGravity (GravityFlags.Center, 0, 0);
							toast.Show ();

						});
					}				
				}).Start();

			

					
				break;
			// En cas d'appui sur le bouton mots de passe
			case "Mot de passe": 
				Toast.MakeText (this, "coucou", ToastLength.Short).Show ();
				break; 
			// En cas d'appui sur le bouton réservation
			case "Réservation": 

				Android.App.FragmentTransaction transaction = FragmentManager.BeginTransaction(); 
				FragmentReservation reservation = new FragmentReservation(); 
				reservation.Show(transaction, "FragmentReservation"); 

				break; 
			}


		}


	}
}


