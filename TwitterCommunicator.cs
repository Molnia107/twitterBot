using System;
using RestSharp;


namespace TwitterBot
{
	public class TwitterCommunicator
	{
		AuthInfo authInfo = new AuthInfo ();

		public TwitterCommunicator ()
		{
			Authenticate ();
		}


		public string Authenticate()
		{

			//get request_token
			var client = new RestClient(authInfo.AuthUrl);
			client.Authenticator = RestSharp.Authenticators.OAuth1Authenticator.ForRequestToken (authInfo.ConsumerKey, authInfo.ConsumerSecret, authInfo.CallbackUrl);
			var request = new RestRequest(authInfo.AuthRequestTokenUrl, Method.POST);

			var response =  client.Execute (request).Content;
			ReadAuthenticationResponse (response);

			return "https://api.twitter.com/oauth/authenticate?oauth_token=" + authInfo.OauthToken;
		}

		public void Authorize()
		{
			var client = new RestClient(authInfo.AuthUrl);
			client.Authenticator = RestSharp.Authenticators.OAuth1Authenticator.ForAccessToken (authInfo.ConsumerKey, authInfo.ConsumerSecret, authInfo.OauthToken, authInfo.OauthTokenSecret);
			//client.Timeout = 150000;
			var request = new RestRequest (authInfo.AuthAccessTokenUrl, Method.POST);
			//client.Authenticator.Authenticate (client, request);
			var response =  client.Execute (request).Content;


		}

		void ReadAuthenticationResponse(string response)
		{

			if (response != null && response.Contains("oauth_callback_confirmed=true")) 
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
							authInfo.OauthToken = valueMass [1];
							break;
						case ("oauth_token_secret"):
							authInfo.OauthTokenSecret = valueMass [1];
							break;
						}
					}
				}
			}
		}
	}
}

