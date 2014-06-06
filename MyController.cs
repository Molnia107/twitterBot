using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace TwitterBot
{
	public class MyController : UIViewController
	{
		string _tag;
		MainView _view;

		public MyController (string tag)
		{
			_tag = tag;

		}



		public override void LoadView ()
		{
			_view = new MainView ();
			View = _view;
		}



		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			if (!ShyBot.IsAuthorized)
				ShyBot.Authontificate (ViewAuth);
			else 
			{
				GetTwitts ();
			}


		}


		public void ViewAuth( string authUrl)
		{
			//authorize user
			UIWebView webView = new UIWebView ();

			webView.ShouldStartLoad += ShouldStartLoad;

			webView.Frame = UIScreen.MainScreen.Bounds;

			View.AddSubview (webView);

			webView.LoadRequest(new NSUrlRequest(new NSUrl(authUrl)));

		}

		bool ShouldStartLoad (UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType)
		{
			if (request.Url.Host == "www.yandex.ru") 
			{
				ContinueAuthorization (request.Url.Query, webView);
			}
			return true;
		}

		private void ContinueAuthorization(string query, UIWebView webView)
		{
			ShyBot.Authorize (query);
			webView.RemoveFromSuperview ();

			GetTwitts ();

			//else msg
		}

		void GetTwitts()
		{

			var twitts = ShyBot.GetTwitts (_tag);
			_view.DisplayTwitts (twitts);

		}
	}
}

