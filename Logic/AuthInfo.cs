using System;
using System.Collections.Generic;

namespace TwitterBot
{
	public class AuthInfo
	{


		public string ConsumerKey = "WkLxKrBPzrxbiWVLKhLoPhsOM";
		public string ConsumerSecret = "0dbCsRUF74Wu29RPFEg8ktP3EU7uyELZ2UyTiD6Pz9vNU64YEL";
		public string AuthUrl = "https://api.twitter.com";
		public string AuthRequestTokenUrl = "oauth/request_token";
		public string OauthToken;
		public string OauthTokenSecret;
		public string AuthorizeUrl = "oauth/authorize";
		public string AuthAccessTokenUrl = "oauth/access_token";
		public string CallbackUrl = "https://www.yandex.ru";
		public string OauthVerifier;
		public string UserId;
		public string ScreenName;

		public AuthInfo ()
		{


		}
	}
}

