using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ApplicationCasier
{
	public class Dialog_Reservation : DialogFragment
	{
		TextView numero;
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
			//ajout du numero casier
			numero.Text = "le numero du casier est :" + numero_casier; 
			//Creation de l'alerte mots de passe : 
			var alert_mdp = new AlertDialog.Builder(view.Context);
			alert_mdp.SetTitle("Mots de passe");
			alert_mdp.SetCancelable(false);
			//au clic du bouton d'alerte 
			alert_mdp.SetNeutralButton ("ok",(object sender,DialogClickEventArgs e)=>{
				base.OnDestroyView(); 
			});
					

			//au clique de validation : 
			validate.Click += delegate {
				//recuperation du mots de passe :
				char[] numero_casier = Userapp.recuperation_mdp(); 
				string num_casier = new string(numero_casier); 
				//affichage du mots de passe : 
				view.Visibility = ViewStates.Invisible; 
				alert_mdp.SetMessage("Votre mot de passe est : " + num_casier);
				alert_mdp.Show(); 
				 
			};

			//refuser la validation
			hide.Click += delegate {
				base.OnDestroyView(); 
			};	

			//retourne la vue :
			return view; 
		}
		// mutateur pour modifier le numero du casier
		public void modifier_num_casier(string numero){
			numero_casier = numero; 
		}
		public void GetUserApplication(User_Application App){
			Userapp = App;
		}
	}
}

