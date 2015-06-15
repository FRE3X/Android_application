using System;
using System.IO;
using System.Collections.Generic;

namespace ApplicationCasier
{
	public class FilesAcces
	{
		public System.Xml.Serialization.XmlSerializer writer,reader;
		string path_mdp; 
		string path_option; 
		public FilesAcces ()
		{
			//chemin d'accés de mes documents : 
			var documents = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments);		
			path_option = System.IO.Path.Combine(documents, "options.xml");
			path_mdp = System.IO.Path.Combine (documents, "mots_de_passe.xml"); 

			// si le fichier n'existe pas on le crée (pour l'option):
			if (!File.Exists (path_option)) {	
				//nouvel instance de la classe et initialisation des valeurs par defaut : 
				Option option = new Option ();
				option.ip = "192.168.1.99";
				option.port="4790";  

				//crée un nouveau filestream pour écrire dans le fichier : 
				writer = new System.Xml.Serialization.XmlSerializer(typeof(Option)); 
				TextWriter WriteFileStream = new StreamWriter (path_option);
				writer.Serialize (WriteFileStream, option);
				//ferme le stream : 
				WriteFileStream.Close(); 

			}
			// si le fichier n'existe pas on le crée (pour les mots de passe):
			if(!File.Exists (path_mdp)){
				//crée une nouvel instance de la classe : 
				Gestion_Casiers casiers = new Gestion_Casiers();
				//crée 3 casier 
				casiers.List_Lockers.Add ("");//casier1
				casiers.List_Lockers.Add ("");//casier2
				casiers.List_Lockers.Add ("");//casier3

				//crée un nouveau filestream pour écrire dans le fichier : 
				writer = new System.Xml.Serialization.XmlSerializer(typeof(Gestion_Casiers)); 
				TextWriter WriteFileStream = new StreamWriter (path_mdp);
				writer.Serialize (WriteFileStream, casiers); 
				//ferme le stream : 
				WriteFileStream.Close (); 

			}
		}

		//retourne l'IP contenue dans le fichier de configuration : 
		public string read_IP(){
			//on adapte le reader a la classe Gestion_Casiers : 
			reader = new System.Xml.Serialization.XmlSerializer (typeof(Option)); 
			//crée un nouveau filestream pour lire dans le fichier : 
			FileStream ReadFileStream = new FileStream(path_option, FileMode.Open, FileAccess.Read, FileShare.Read);
			//on deserialise la classe : 
			Option options = (Option)reader.Deserialize (ReadFileStream); 
			//on ferme le stream : 
			ReadFileStream.Close(); 
			//on retourne l'addresse ip 
			return options.ip; 
		}

		//retourne le port contenue dans le fichier de configuration : 
		public string read_port(){
			//on adapte le reader a la classe Gestion_Casiers : 
			reader = new System.Xml.Serialization.XmlSerializer (typeof(Option)); 
			//crée un nouveau filestream pour lire dans le fichier : 
			FileStream ReadFileStream = new FileStream(path_option, FileMode.Open, FileAccess.Read, FileShare.Read);
			//on deserialise la classe : 
			Option options = (Option)reader.Deserialize (ReadFileStream); 
			//on ferme le stream : 
			ReadFileStream.Close(); 
			//on retourne l'addresse ip 
			return options.port; 
		}
		//modifie le fichier l'ip et le port dans le fichier xml
		public void modify_options(string p_ip,string p_port){
			Option options = new Option (); 
			options.ip = p_ip; 
			options.port = p_port; 
			//crée un nouveau filestream pour écrire dans le fichier : 
			writer = new System.Xml.Serialization.XmlSerializer(typeof(Option)); 
			TextWriter WriteFileStream = new StreamWriter (path_option);
			writer.Serialize (WriteFileStream, options); 
			//ferme le stream : 
			WriteFileStream.Close (); 

		}
		//méthode pour écrire le numéro de casier et le mots de passe : 
		public bool write_casier(string mdp,string n_casier){
			//retourne false si la valeur n'a pas bien était écris :
			bool write = false; 
			//on adapte le reader a la classe Gestion_Casiers : 
			reader = new System.Xml.Serialization.XmlSerializer (typeof(Gestion_Casiers)); 
			//crée un nouveau filestream pour lire dans le fichier : 
			FileStream ReadFileStream = new FileStream(path_mdp, FileMode.Open, FileAccess.Read, FileShare.Read);
			//on deserialise la classe : 
			Gestion_Casiers casiers = (Gestion_Casiers)reader.Deserialize (ReadFileStream); 
			//on ferme le stream : 
			ReadFileStream.Close(); 

			//on boucle la liste casier 
			for (int i = 0; i < casiers.List_Lockers.Count; i++) {
				//si un string de List_casier et vide alors :
				if (casiers.List_Lockers[i] == "") {
					//on copie le mdp et le n_casier dedans :
					casiers.List_Lockers[i]= mdp + "|" + n_casier + "|";
					write = true;//pour confirmer que l'écriture a bien était réaliser 
					break;//on sort de la boucle 
				}

			}

			//crée un nouveau filestream pour écrire dans le fichier : 
			writer = new System.Xml.Serialization.XmlSerializer(typeof(Gestion_Casiers)); 
			TextWriter WriteFileStream = new StreamWriter (path_mdp);
			writer.Serialize (WriteFileStream, casiers); 
			//ferme le stream : 
			WriteFileStream.Close (); 

			return write; 
		}
		//méthode pour vider le fichier mdp 
		public void clear_locker(){
			//crée une nouvel instance de la classe : 
			Gestion_Casiers casiers = new Gestion_Casiers();
			//crée 3 casier 
			casiers.List_Lockers.Add ("");//casier1
			casiers.List_Lockers.Add ("");//casier2
			casiers.List_Lockers.Add ("");//casier3

			//crée un nouveau filestream pour écrire dans le fichier : 
			writer = new System.Xml.Serialization.XmlSerializer(typeof(Gestion_Casiers)); 
			TextWriter WriteFileStream = new StreamWriter (path_mdp);
			writer.Serialize (WriteFileStream, casiers); 
			//ferme le stream : 
			WriteFileStream.Close (); 
		
		
		}
		//méthode pour lire tous les numéro de casiers et mots de passe : 
		public string read_casiers(){
			string recuperation_casier = null;
			//on adapte le reader a la classe Gestion_Casiers : 
			reader = new System.Xml.Serialization.XmlSerializer (typeof(Gestion_Casiers)); 
			//crée un nouveau filestream pour lire dans le fichier : 
			FileStream ReadFileStream = new FileStream(path_mdp, FileMode.Open, FileAccess.Read, FileShare.Read);
			//on deserialise la classe : 
			Gestion_Casiers casiers = (Gestion_Casiers)reader.Deserialize (ReadFileStream);
			//on ferme le stream : 
			ReadFileStream.Close(); 
			foreach (string casier in casiers.List_Lockers) {
				recuperation_casier += casier; 
			}

			return recuperation_casier; 
		}

	}
	//Classe "Buffer" pour le writer xml 
	public class Option{
		public string ip; 
		public string port; 
	}
	//Classe "Buffer" pour le writer xml 
	public class Gestion_Casiers{
		private List<string> list_lockers = new List<string>();
		public List<string> List_Lockers
		{
			get {return list_lockers; }
			set {list_lockers = value;}
		}
	}

}

