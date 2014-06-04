using System;
using MonoTouch.UIKit;



namespace TwitterBot
{
	public class ShyBot
	{
		TwitterCommunicator communicator = new TwitterCommunicator();

		public ShyBot ()
		{
		}

		public delegate void AuthontificateView(string authUrl);

		public void Authontificate(AuthontificateView authontificateView)
		{
			var url = communicator.Authenticate ();
			authontificateView (url);
			communicator.Authorize ();
		}



	}
}

