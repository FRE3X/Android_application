///////////////////////////////////////////////////////////
//  User_Application.cs
//  Implementation of the Class User_Application
//  Generated by Enterprise Architect
//  Created on:      01-f��vrier.-2015 15:45:19
//  Original author: Callot Antoine
///////////////////////////////////////////////////////////
using System; 
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
 




public class User_Application {


	/// Contient l'adresse MAC du t��l��phone
	//private string AddrMAC;

	/// Contient le mot de passe du casier
	private short  MdpCasier;

	/// Contient le num��ro du casier
	private short NumCasier;

	string ip_server;
	int Port; 
	Socket s; 

	public User_Application(string ip,int port){
		ip_server = ip; 
		Port = port; 
	}

	~User_Application(){

	}

	public virtual void Dispose(){

	}

	public int Actualiser(){
		EnvoiAddrMAC (); 

//		Byte[] message = new byte[5];   
//
//		s.Receive (message, SocketFlags.None);
//
//		s.Close (); 
//
//		int m = Convert.ToInt16(message); 
//
		return 0;
	}

	public char[] DemandeCasier(){

		return null;
	}

	public string ObtenirAddrMAC(){
			NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
			String sMacAddress = string.Empty;
			
			foreach (NetworkInterface adapter in nics)
			{
			if  (sMacAddress == String.Empty)// r��cup��rer l'adresse mac de la permi��re carte   
				{
				sMacAddress = adapter.GetPhysicalAddress().ToString();
				}
			}

		return sMacAddress; 
			
		 
	}


	public char[] ValidationCasier(){

		return null;
	}

	public void EnvoiAddrMAC(){
		System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding(); 

		//Recuperation de l'adresse mac : 
		string mac = ObtenirAddrMAC (); 

		//Convertion addresse Mac 
		Byte[] message = Encoding.ASCII.GetBytes (mac); 

		connection (); 

		//envoie du message
		s.Send (message, message.Length, 0); 

		//fermeture du soket 
		s.Close (); 
	}

	public void connection(){
		s = null;
		IPHostEntry hostEntry = null;
		//Byte[] message = Encoding.ASCII.GetBytes("coucou");  

		// Get host related information.
		hostEntry = Dns.GetHostEntry(ip_server);

		// Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
		// an exception that occurs when the host IP Address is not compatible with the address family
		// (typical in the IPv6 case).
		foreach(IPAddress address in hostEntry.AddressList)
		{
			IPEndPoint ipe = new IPEndPoint(address,Port);
			Socket tempSocket = 
				new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


			tempSocket.Connect(ipe);


			if(tempSocket.Connected)
			{
				s = tempSocket;
				break;
			}
			else
			{
				continue;
			}
		}

		//s.Send (message, message.Length, 0);
	}

}//end User_Application