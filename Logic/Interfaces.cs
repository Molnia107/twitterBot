using System;
using System.Collections.Generic;

namespace TwitterBot
{
	public interface IRecepient
	{
		void SetNetworkError ();
		void SetAuthorizationRezult (bool rezult);
	}

	public interface ITwittsRecepient : IRecepient
	{
		void SetTwitts(List<Twitt> twittList);
	}

	public interface IAuthorizationRecepient : IRecepient
	{
		void Authontificate (string url);
	}
}

