using System;
using System.Net.NetworkInformation;

namespace getAddMac
{
	class MainClass
	{
		public static void ObtenirAddrMAC()
		{
			NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
			String sMacAddress = string.Empty;

			foreach (NetworkInterface adapter in nics)
			{
				if  (sMacAddress == String.Empty)// récupérer l'adresse mac de la permière carte   
				{
					sMacAddress = adapter.GetPhysicalAddress().ToString();

				}
			}
			Console.WriteLine ("mac addresse : " + sMacAddress);

		



		}

		public static string GetAddrWireless()
		{
			NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
			String sMacAddress = string.Empty;

			foreach(NetworkInterface adptater in nics)
			{
				if (adptater.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
				{

					return adptater.GetPhysicalAddress ().ToString (); 
				
				}

			}
			return "none"; 
		
		}

		public static PhysicalAddress GetMacAddress()
		{
			foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
					nic.OperationalStatus == OperationalStatus.Up)
				{
					return nic.GetPhysicalAddress();
				}
			}
			return null;
		}

		public static void Main (string[] args)
		{

			ObtenirAddrMAC (); 
			Console.WriteLine (GetAddrWireless()); 
		}



	}
}
