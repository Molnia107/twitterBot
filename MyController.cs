using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace TwitterBot
{
	public class MyController : UIViewController
	{
		public MyController ()
		{


		}

		public override void LoadView ()
		{
			View = new MainView ();
		}

		ShyBot bot;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			bot = new ShyBot ();
			bot.Authontificate (ViewAuth);


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
			bot.Authorize (query);
			webView.RemoveFromSuperview ();

			UITableView tableView = new UITableView (View.Bounds);
			var twitts = bot.GetTwitts ("cute");
			tableView.Source = new TableSource (twitts);
			View.AddSubview (tableView);
			//else msg
		}
	}
}

