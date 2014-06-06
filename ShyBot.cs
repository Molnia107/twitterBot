using System;
using MonoTouch.UIKit;
using System.Collections.Generic;


namespace TwitterBot
{
	public class ShyBot
	{
		TwitterCommunicator communicator = new TwitterCommunicator();
		public bool IsAuthorized;

		public ShyBot ()
		{
			IsAuthorized = false;
		}

		public delegate void AuthontificateView(string authUrl);

		public void Authontificate(AuthontificateView authontificateView)
		{
			var url = communicator.Authenticate ();
			if (url != null)
				authontificateView (url);
			//msg

		}

		public void Authorize(string query)
		{
			IsAuthorized = communicator.Authorize (query);

		}

		public List<Twitt> GetTwitts(string tag)
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

