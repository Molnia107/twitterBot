using System;
using RestSharp;

namespace TwitterBot
{
	public class ShyBotRestRequest : RestRequest
	{
		public IRecepient Recepient;

		public ShyBotRestRequest ():base()
		{
		}

		public ShyBotRestRequest(string resource, Method method, IRecepient recepient):base(resource, method)
		{
			Recepient = recepient;
		}
	}
}

