using System;
using System.IO;

namespace ApplicationCasier
{
	public class FilesAcces
	{
		public System.Xml.Serialization.XmlSerializer writer;
		public System.Xml.Serialization.XmlSerializer reader; 

		public FilesAcces ()
		{
			//chemin d'accés : 
			var documents = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments);		
			var path_option = System.IO.Path.Combine(documents, "options.csv");
			var path_mdp = System.IO.Path.Combine (documents, "mots_de_passe.csv"); 

			// si le fichier n'existe pas on le crée (pour l'option):
			if (!File.Exists (path_option)) {
				FileStream file =  File.Create (path_option); 
				//et on initialise les valeur par defaut : 
				Option option = new Option ();
				option.ip = "192.168.1.99";
				option.port="4790";  
				writer = new System.Xml.Serialization.XmlSerializer (typeof(Option)); 
				writer.Serialize (file, option); 
			}
			// si le fichier n'existe pas on le crée (pour les mots de passe):
			if(!File.Exists (path_mdp)){
				FileStream file =  File.Create (path_mdp);
				mdp file_mdp = new mdp ();
				file_mdp.mot_de_pass = " "; 
				file_mdp.numero_casier = " ";  
				writer = new System.Xml.Serialization.XmlSerializer (typeof(mdp)); 
				writer.Serialize (file, file_mdp); 
			}
		}

		//retourne l'IP contenue dans le fichier de configuration : 
		public string read_IP(){
			//chemin d'accés : 
			var documents = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments);		
			var path = System.IO.Path.Combine(documents, "options.csv");

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
			var path = System.IO.Path.Combine(documents, "options.csv");

			System.IO.StreamReader file = new System.IO.StreamReader(path); 
			//initialise l'objet "tampon" appeller option : 
			Option option = new Option ();


			reader = new System.Xml.Serialization.XmlSerializer (typeof(Option)); 
			//deserialise le fichier pour le coptier dans l'objet tampon :
			option = (Option)reader.Deserialize (file); 

			//retourne l'ip dans l'objet tampon : 
			return (option.port);
			//modifie le fichier l'ip dans le fichier xml
		}

		public void modify_options(string p_ip,string p_port){
			//chemin d'accés : 
			var documents = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments);		
			var path = System.IO.Path.Combine(documents, "options.csv");
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
	//Classe "Buffer" pour le writer xml 
	public class mdp{
		public string mot_de_pass;
		public string numero_casier; 
	}

}

