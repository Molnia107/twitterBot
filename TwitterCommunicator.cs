using System;
using RestSharp;


namespace TwitterBot
{
	public class TwitterCommunicator
	{
		AuthInfo _authInfo;
		AuthParser _authParser;

		public TwitterCommunicator ()
		{
			_authInfo = new AuthInfo ();
			_authParser = new AuthParser (_authInfo);

		}


		public string Authenticate()
		{

			//get request_token
			var client = new RestClient(_authInfo.AuthUrl);
			client.Authenticator = RestSharp.Authenticators.OAuth1Authenticator.ForRequestToken (_authInfo.ConsumerKey, _authInfo.ConsumerSecret, _authInfo.CallbackUrl);
			var request = new RestRequest(_authInfo.AuthRequestTokenUrl, Method.POST);

			var response =  client.Execute (request).Content;
			if (response.Contains ("oauth_token")) 
			{
				_authParser.ParseResponse (response);
				return "https://api.twitter.com/oauth/authenticate?oauth_token=" + _authInfo.OauthToken;
			}
			return null;
		
		}

		public bool Authorize(string query)
		{
			//authorize user
			if (query.Contains ("oauth_verifier")) {
				_authParser.ParseResponse (query);

				var client = new RestClient (_authInfo.AuthUrl);
				client.Authenticator = RestSharp.Authenticators.OAuth1Authenticator.ForAccessToken (_authInfo.ConsumerKey, _authInfo.ConsumerSecret, _authInfo.OauthToken, 
					_authInfo.OauthTokenSecret, _authInfo.OauthVerifier);
				var request = new RestRequest (_authInfo.AuthAccessTokenUrl, Method.POST);

				IRestResponse iRestResponse = client.Execute (request);
				var response = iRestResponse.Content;

				if (response.Contains ("auth_token")) 
				{
					_authParser.ParseResponse (response);
					GetData ();

					return true;
				}
			} 
			//msg
			return false;
		}

		public void GetData()
		{
			//get data
			var client = new RestClient(_authInfo.AuthUrl);
			client.Authenticator = RestSharp.Authenticators.OAuth1Authenticator.ForProtectedResource(_authInfo.ConsumerKey, _authInfo.ConsumerSecret, _authInfo.OauthToken, _authInfo.OauthTokenSecret);
			var request = new RestRequest("1.1/search/tweets.json?q=putin", Method.GET);

			var response =  client.Execute (request).Content;

		}




	}
}

