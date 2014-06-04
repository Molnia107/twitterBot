using System;
using System.Collections.Generic;

namespace TwitterBot
{
	public class AuthInfo
	{


		public string ConsumerKey = "c8jhYhKfPy3GbbFObAV2rGfY5";
		public string ConsumerSecret = "yFejZuDOlis5XlwRN6bDvqD4R0908baG874NtrDsHcUjrxTA1V";
		public string AuthUrl = "https://api.twitter.com";
		public string AuthRequestTokenUrl = "oauth/request_token";
		public string OauthToken;
		public string OauthTokenSecret;
		public string AuthorizeUrl = "oauth/authorize";
		public string AuthAccessTokenUrl = "oauth/access_token";
		public string CallbackUrl = "https://www.yandex.ru";

		public AuthInfo ()
		{


		}
	}
}

