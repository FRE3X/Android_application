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
		string numero_casier; 
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState); 
			var view = inflater.Inflate (Resource.Layout.Dialog_Frame, container, false); 

			//declaration des objet de la forme : 
			numero = (TextView)view.FindViewById (Resource.Id.textview_numero);
			validate = (ImageButton)view.FindViewById (Resource.Id.imageButtonKeyFree1); 
			hide = (ImageButton)view.FindViewById (Resource.Id.imageButtonKeyFree2);  
			//ajout du numero casier
			numero.Text = "les numero du casier est :" + numero_casier; 

			//au clique de validation : 
			validate.Click += delegate {
				base.OnDestroyView(); 
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
	}
}

