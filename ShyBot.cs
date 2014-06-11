using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;

namespace TwitterBot
{
	static public class ShyBot
	{
		enum eStatus
		{
			Authontification, 
			Authorization,
			Work
		}

		static eStatus _status = new eStatus();

		static TwitterCommunicator communicator = new TwitterCommunicator();

		static public bool IsAuthorized
		{

			get{ return communicator.IsAuthorized;}
		}


		public delegate void AuthontificateView(string authUrl);


		static public void Authontificate(IRecepient recepient)
		{
			_status = eStatus.Authontification;
			communicator.Authenticate (recepient);
			//msg

		}

		static public void Authorize(string query, IRecepient recepient)
		{
			_status = eStatus.Authorization;
			communicator.Authorize (query, recepient);

		}

		static public void GetTwitts(string tag, IRecepient recepient)
		{
			_status = eStatus.Work;
			if (IsAuthorized) 
			{
				communicator.GetTwittsByTag (tag,recepient);
			} else 
			{
				//error
			}
		}


		static public void GetMoreTwitts(string tag,IRecepient recepient, List<Twitt> twittList)
		{
			if (IsAuthorized) 
			{
				communicator.GetTwittsByTag (tag,recepient, twittList.LastOrDefault());
			} 
			else 
			{
				//error
			}
		}

		static public void TryAgain(IRecepient recepient)
		{
			switch (_status) 
			{
			case(eStatus.Authontification):
			case(eStatus.Authorization):
				Authontificate (recepient);
				break;
			case(eStatus.Work):
				break;
			}
		}
	}
}

