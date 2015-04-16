
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
	public class Dialog_Option : DialogFragment
	{
		EditText edit ;
		Button button_confirmer; 
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState); 
			var view = inflater.Inflate (Resource.Layout.Dialog_Option, container, false); 

			edit = (EditText)view.FindViewById (Resource.Id.editText1); 
			button_confirmer = (Button)view.FindViewById (Resource.Id.button_confirmer); 

			//modification de l'adresse IP du serveur
			button_confirmer.Click += delegate {
				
				base.OnDestroyView(); 
			};
			return view; 
		}
	}
}

