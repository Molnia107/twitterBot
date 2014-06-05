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
			webView.LoadFinished += delegate 
			{
				var url = webView.Request.Url.AbsoluteString;
			};
			webView.ShouldStartLoad += ShouldStartLoad;

			webView.Frame = UIScreen.MainScreen.Bounds;

			View.AddSubview (webView);

			webView.LoadRequest(new NSUrlRequest(new NSUrl(authUrl)));

		}

		bool ShouldStartLoad (UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType)
		{
			if (request.Url.Host == "www.yandex.ru") 
			{
				bot.Authorize (request.Url.Query);
			}
			return true;
		}
	}
}

