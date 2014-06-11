using System;
using System.Collections.Generic;

namespace TwitterBot
{
	public interface IRecepient
	{
		void SetTwitts(List<Twitt> twittList);
		void SetNetworkError ();
		void Authontificate (string url);
		void SetAuthorizationRezult (bool rezult);
	}
}

