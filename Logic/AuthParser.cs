using System;

namespace TwitterBot
{
	public class AuthParser
	{
		AuthInfo _authInfo;

		public AuthParser (AuthInfo authInfo)
		{
			_authInfo = authInfo;
		}

		public void ParseResponse(string response)
		{
			var mass = response.Split ('&');
			foreach (string value in mass) 
			{
				var valueMass = value.Split ('=');
				if (valueMass.Length == 2) 
				{
					switch (valueMass [0]) 
					{
					case ("oauth_token"):
						_authInfo.OauthToken = valueMass [1];
						break;
					case ("oauth_token_secret"):
						_authInfo.OauthTokenSecret = valueMass [1];
						break;
					case ("oauth_verifier"):
						_authInfo.OauthVerifier = valueMass [1];
						break;
					case("user_id"):
						_authInfo.UserId = valueMass [1];
						break;
					case("screen_name"):
						_authInfo.ScreenName = valueMass [1];
						break;
					}
				}
			}
		}




	}
}

