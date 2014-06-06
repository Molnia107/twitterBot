using System;
using MonoTouch.UIKit;
using System.Collections.Generic;


namespace TwitterBot
{
	static public class ShyBot
	{
		static TwitterCommunicator communicator = new TwitterCommunicator();
		static public bool IsAuthorized = false;


		public delegate void AuthontificateView(string authUrl);

		static public void Authontificate(AuthontificateView authontificateView)
		{
			var url = communicator.Authenticate ();
			if (url != null)
				authontificateView (url);
			//msg

		}

		static public void Authorize(string query)
		{
			IsAuthorized = communicator.Authorize (query);

		}

		static public List<Twitt> GetTwitts(string tag)
		{
			if (IsAuthorized) 
			{
				return communicator.GetTwittsByTag (tag);
			} else 
			{
				return new List<Twitt> ();
			}
		}

	}
}

