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
			var path_option = System.IO.Path.Combine(documents, "options.xml");
			var path_mdp = System.IO.Path.Combine (documents, "mots_de_passe.xml"); 

			// si le fichier n'existe pas on le crée (pour l'option):
			if (!File.Exists (path_option)) {
				FileStream file =  File.Create (path_option); 
				//et on initialise les valeur par defaut : 
				Option option = new Option ();
				option.ip = "192.168.1.99";
				option.port="4790";  
				writer.Serialize (file, option);
				file.Close();//ferme le stream pour éviter les erreurs 
			}
			// si le fichier n'existe pas on le crée (pour les mots de passe):
			if(!File.Exists (path_mdp)){
				FileStream file =  File.Create (path_mdp);
				mdp file_mdp = new mdp ();
				file_mdp.mot_de_pass = " "; 
				file_mdp.numero_casier = " ";  
				writer = new System.Xml.Serialization.XmlSerializer (typeof(mdp)); 
				writer.Serialize (file, file_mdp);
				//ferme le stream et le writer pour éviter les erreurs 
				file.Close();

			}
		}

		//retourne l'IP contenue dans le fichier de configuration : 
		public string read_IP(){
			//chemin d'accés : 
			var documents = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments);		
			var path = System.IO.Path.Combine(documents, "options.xml");

			System.IO.StreamReader file = new System.IO.StreamReader(path); 
			//initialise l'objet "tampon" appeller option : 
			Option option = new Option ();


			reader = new System.Xml.Serialization.XmlSerializer (typeof(Option)); 
			//deserialise le fichier pour le coptier dans l'objet tampon :
			option = (Option)reader.Deserialize (file); 
			//ferme le stream pour éviter les erreurs
			file.Close(); 
			//retourne l'ip dans l'objet tampon : 
			return (option.ip);
		}

		//retourne le port contenue dans le fichier de configuration : 
		public string read_port(){
			//chemin d'accés : 
			var documents = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments);		
			var path = System.IO.Path.Combine(documents, "options.xml");

			System.IO.StreamReader file = new System.IO.StreamReader(path); 
			//initialise l'objet "tampon" appeller option : 
			Option option = new Option ();


			reader = new System.Xml.Serialization.XmlSerializer (typeof(Option)); 
			//deserialise le fichier pour le coptier dans l'objet tampon :
			option = (Option)reader.Deserialize (file); 
			//ferme le stream pour éviter les erreurs
			file.Close();

			//retourne l'ip dans l'objet tampon : 
			return (option.port);

		}
		//modifie le fichier l'ip et le port dans le fichier xml
		public void modify_options(string p_ip,string p_port){
			//chemin d'accés : 
			var documents = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments);		
			var path = System.IO.Path.Combine(documents, "options.xml");
			//Creation du FileStream
			FileStream file = new FileStream(path,FileMode.Open); 

			Option option = new Option ();
			//ecriture dans l'objet "tampon" appeller option : 
			option.ip = p_ip; 
			option.port = p_port;

			//ecriture avec l'objet tampon dans le fichier xml : 
			writer = new System.Xml.Serialization.XmlSerializer (typeof(Option)); 
			writer.Serialize (file, option); 

			//ferme le stream pour éviter les erreurs
			file.Close();

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

