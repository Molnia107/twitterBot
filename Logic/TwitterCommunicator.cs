using System;
using RestSharp;
using RestSharp.Deserializers;
using System.Collections.Generic;
using System.Net;

namespace TwitterBot
{
	public class TwitterCommunicator
	{
		AuthInfo _authInfo;
		AuthParser _authParser;
		GetTwittsByTag_CallBackDelegate _getTwittsByTag_CallBackDelegate;
		RestClient _client;

		public bool IsAuthorized = false;

		public delegate void GetTwittsByTag_CallBackDelegate(List<Twitt> twittList);

		public TwitterCommunicator ()
		{
			_authInfo = new AuthInfo ();
			_authParser = new AuthParser (_authInfo);
			_client = new RestClient(_authInfo.AuthUrl);
			_client.Timeout = 5 * 1000;
		}

		void Authenticate_CallBack(IRestResponse response,RestRequestAsyncHandle handle)
		{
			if(CheckResponse(response))
			{
				if (response.Content.Contains ("oauth_token")) 
				{
					_authParser.ParseResponse (response.Content);
					if (response.Request is ShyBotRestRequest && (response.Request as ShyBotRestRequest).Recepient != null &&
						(response.Request as ShyBotRestRequest).Recepient is IAuthorizationRecepient) 
					{
						((response.Request as ShyBotRestRequest).Recepient as IAuthorizationRecepient).Authontificate ("https://api.twitter.com/oauth/authenticate?oauth_token=" + _authInfo.OauthToken);
					}
				}
			}
		}

		void Authorize_CallBack(IRestResponse response,RestRequestAsyncHandle handle)
		{
			if (CheckResponse (response)) 
			{
				if (response.Content.Contains ("auth_token")) 
				{
					_authParser.ParseResponse (response.Content);
					IsAuthorized = true;
					if (response.Request is ShyBotRestRequest && (response.Request as ShyBotRestRequest).Recepient != null ) 
					{
						(response.Request as ShyBotRestRequest).Recepient.SetAuthorizationRezult (IsAuthorized);
					}
				}
			}
		}

		void GetTwittsByTag_CallBack(IRestResponse response,RestRequestAsyncHandle handle)
		{
			if (CheckResponse (response)) 
			{
				JsonDeserializer deserializer = new JsonDeserializer ();
				RootObject obj = deserializer.Deserialize<RootObject> (response);

				if (response.Request is ShyBotRestRequest && (response.Request as ShyBotRestRequest).Recepient != null &&
					(response.Request as ShyBotRestRequest).Recepient is ITwittsRecepient) 
				{
					((response.Request as ShyBotRestRequest).Recepient as ITwittsRecepient).SetTwitts (obj.statuses);
				}
			}
			//return obj.statuses;
		}

		bool CheckResponse(IRestResponse response)
		{
			var statusCode = response.StatusCode;
			if (statusCode != HttpStatusCode.OK ) 
			{
				if(response.Request is ShyBotRestRequest && (response.Request as ShyBotRestRequest).Recepient != null)
					(response.Request as ShyBotRestRequest).Recepient.SetNetworkError ();
				return false;
			}
			return true;
		}

		public void Authenticate(IRecepient recepient)
		{
			//get request_token

			_client.Authenticator = RestSharp.Authenticators.OAuth1Authenticator.ForRequestToken (_authInfo.ConsumerKey, _authInfo.ConsumerSecret, _authInfo.CallbackUrl);
			var request = new ShyBotRestRequest(_authInfo.AuthRequestTokenUrl, Method.POST,recepient);

			_client.ExecuteAsync (request, Authenticate_CallBack);
		
		}

		public void Authorize(string query, IRecepient recepient)
		{
			//authorize user
			if (query.Contains ("oauth_verifier")) {
				_authParser.ParseResponse (query);


				_client.Authenticator = RestSharp.Authenticators.OAuth1Authenticator.ForAccessToken (_authInfo.ConsumerKey, _authInfo.ConsumerSecret, _authInfo.OauthToken, 
					_authInfo.OauthTokenSecret, _authInfo.OauthVerifier);
				var request = new ShyBotRestRequest (_authInfo.AuthAccessTokenUrl, Method.POST, recepient);

				_client.ExecuteAsync (request, Authorize_CallBack);
			} 
		}

		public void GetTwittsByTag(string tag, IRecepient recepient, Twitt lastTwitt = null)
		{
			//get data

			_client.Authenticator = RestSharp.Authenticators.OAuth1Authenticator.ForProtectedResource(_authInfo.ConsumerKey, _authInfo.ConsumerSecret, _authInfo.OauthToken, _authInfo.OauthTokenSecret);
			var request = new ShyBotRestRequest("1.1/search/tweets.json", Method.GET,recepient);
			request.AddParameter ("q", tag);
			if (lastTwitt != null) 
			{
				request.AddParameter ("max_id", lastTwitt.id_str);
			}

			_client.ExecuteAsync (request, GetTwittsByTag_CallBack);
		}



	}
}

