using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets; 

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;


namespace ApplicationCasier
{
	public class Dialog_Reservation : DialogFragment
	{
		TextView numero;
		TextView textview_timer; 
		ImageButton validate;
		ImageButton hide; 
		RelativeLayout layout;
		//variable récupérer : 
		User_Application Userapp; 
		string numero_casier;

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState); 
			var view = inflater.Inflate (Resource.Layout.Dialog_Frame, container, false); 

			//declaration des objet de la forme : 
			layout = (RelativeLayout)view.FindViewById (Resource.Id.RelativeLayout_fragment); 
			numero = (TextView)view.FindViewById (Resource.Id.textview_numero);
			validate = (ImageButton)view.FindViewById (Resource.Id.imageButtonKeyFree1); 
			hide = (ImageButton)view.FindViewById (Resource.Id.imageButtonKeyFree2);  
			textview_timer = (TextView)view.FindViewById (Resource.Id.textview_timer); 

			//ajout du numero casier
			numero.Text = numero_casier; 
						

			//au clique de validation : 
			validate.Click += delegate {
				Affichage_mdp(view); //passe en parametre la vue actuel 
			};

			//refuser la validation
			hide.Click += delegate {
				base.OnDestroyView(); 
			};	

			//retourne la vue :
			return view; 
		}
		public override void OnCreate (Bundle savedInstanceState)
		{
			 base.OnCreate (savedInstanceState);
			 RunUpdateLoop ();

		}
		//Compteur asynchone : 
		private async void RunUpdateLoop()
		{
			int durer = 15;//variable de durer
			while (true)//on boucle a l'infini 
			{
				await Task.Delay(1000);//intervale d'une seconde entre la décrementation
				textview_timer.Text = "il vous reste " + durer.ToString() + " seconde pour effectuer un choix.";
				if (durer == 0)
					base.OnDestroyView ();//arriver a 0 seconde on détruit le fragment  
				durer--;//decrementation 
			}
		}




		// mutateur pour modifier le numero du casier
		public void modifier_num_casier(string numero){
			numero_casier = numero; 
		}
		// mutateur pour récupérer le UserApp
		public void GetUserApplication(User_Application App){
			Userapp = App;
		}
		private void Affichage_mdp(View v){
			//Affichage du message d'attente : 
			var LoadingDialog = ProgressDialog.Show (v.Context, "Connexion en cours", "veuillez patienter.", true);
			try{
				//recuperation du mots de passe :
				char[] numero_casier = Userapp.recuperation_mdp(); 
				string num_casier = new string(numero_casier); 
				LoadingDialog.Dismiss(); 
				//Creation de l'alerte mots de passe : 
				var alert_mdp = new AlertDialog.Builder(v.Context);
				alert_mdp.SetTitle("Mots de passe");
				alert_mdp.SetCancelable(false);
				//au clic du bouton d'alerte 
				alert_mdp.SetNeutralButton ("ok",(object sender,DialogClickEventArgs e)=>{
					base.OnDestroyView();//destruction de la vue  
				});

				//disparition de la vue 
				v.Visibility = ViewStates.Invisible;

				//affichage du mots de passe : 
				alert_mdp.SetMessage("Votre mot de passe est : " + num_casier);
				alert_mdp.Show(); 


				 
			}catch(SocketException i){
				

				//affichage du message d'erreur : 
				Toast toast = Toast.MakeText (v.Context, i.Message, ToastLength.Short);
				toast.SetGravity (GravityFlags.Center, 0, 0);
				toast.Show ();
			
			}
		}

	}

}

