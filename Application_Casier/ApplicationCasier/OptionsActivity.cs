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
		Button button_save; 
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ActivityOption); 

			ip_text = FindViewById<EditText> (Resource.Id.ip_EditText); 
			port_text = FindViewById<EditText> (Resource.Id.port_EditText);
			button_save = FindViewById<Button> (Resource.Id.button_save); 
	
			// Modification du menu de l'action bar :
			ActionBar.SetDisplayHomeAsUpEnabled (true); 
			ActionBar.SetHomeButtonEnabled (true);

			button_save.Click += delegate {
				
			};
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

/// 
/// //////////////////////////////////////////////////////////////
/// //////////////////////////////////////////////////////////////
/// 

	//classe pour changer et recuperer les options de l'appication : 
	public class ChangeOptions{
		public System.Xml.Serialization.XmlSerializer writer;
		public System.Xml.Serialization.XmlSerializer reader; 
		//constructeur qui initialise les variable : 
		public ChangeOptions(){
			//chemin d'accés : 
			var documents = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments);		
			var path = System.IO.Path.Combine(documents, "test_option.csv");

			// si le fichier n'existe pas on le crée :
			if (!File.Exists (path)) {
				FileStream file =  File.Create (path); 
				//et on initialise les valeur par defaut : 
				Option option = new Option ();
				option.ip = "192.168.1.99";
				option.port="4790";  
				writer = new System.Xml.Serialization.XmlSerializer (typeof(Option)); 
				writer.Serialize (file, option); 
			}
				
		}
		//retourne l'IP contenue dans le fichier de configuration : 
		public string read_IP(){
			//chemin d'accés : 
			var documents = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments);		
			var path = System.IO.Path.Combine(documents, "test_option.csv");

			System.IO.StreamReader file = new System.IO.StreamReader(path); 
			//initialise l'objet "tampon" appeller option : 
			Option option = new Option ();


			reader = new System.Xml.Serialization.XmlSerializer (typeof(Option)); 
			//deserialise le fichier pour le coptier dans l'objet tampon :
			option = (Option)reader.Deserialize (file); 

			//retourne l'ip dans l'objet tampon : 
			return (option.ip);
		}
		//retourne le port contenue dans le fichier de configuration : 
		public string read_port(){
			//chemin d'accés : 
			var documents = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments);		
			var path = System.IO.Path.Combine(documents, "test_option.csv");

			System.IO.StreamReader file = new System.IO.StreamReader(path); 
			//initialise l'objet "tampon" appeller option : 
			Option option = new Option ();


			reader = new System.Xml.Serialization.XmlSerializer (typeof(Option)); 
			//deserialise le fichier pour le coptier dans l'objet tampon :
			option = (Option)reader.Deserialize (file); 

			//retourne l'ip dans l'objet tampon : 
			return (option.port);
		//modifie le fichier l'ip dans le fichier xml
		}public void modify(string p_ip,string p_port){
			//chemin d'accés : 
			var documents = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments);		
			var path = System.IO.Path.Combine(documents, "test_option.csv");
			//Creation du FileStream
			FileStream file =  File.Create (path);

			Option option = new Option ();
			//ecriture dans l'objet "tampon" appeller option : 
			option.ip = p_ip; 
			option.port = p_port;

			//ecriture avec l'objet tampon dans le fichier xml : 
			writer = new System.Xml.Serialization.XmlSerializer (typeof(Option)); 
			writer.Serialize (file, option); 

		}


	}
	//Classe "Buffer" pour le writer xml 
	public class Option{
		public string ip; 
		public string port; 
	}



}



