using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;

namespace TwitterBot
{
	public class MainView: UIView
	{
		UITableView _tableView ;
		TableSource _tableSource;
		UIWebView _webView;

		public MainView ()
		{
			BackgroundColor = UIColor.Blue;
			_tableView = new UITableView ();
			AddSubview (_tableView);
			_tableSource = new TableSource ();
			_tableView.Source = _tableSource;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			_tableView.Frame = Bounds;
			if (_webView != null)
				_webView.Frame = Bounds;

		}

		public void DisplayTwitts(List<Twitt> twitts)
		{
			_tableSource.SetSource (twitts);
			_tableView.ReloadData ();

		}

		public delegate void ContinueAuthorization(string query);
		ContinueAuthorization _continueAthorization;

		public void DisplayAuthWebView(string authUrl, ContinueAuthorization continueAuthorization)
		{
			_continueAthorization = continueAuthorization;
			_webView = new UIWebView ();
			_webView.ShouldStartLoad += WebViewAuth_ShouldStartLoad;
			//_webView.Frame = UIScreen.MainScreen.Bounds;
			AddSubview (_webView);
			_webView.LoadRequest(new NSUrlRequest(new NSUrl(authUrl)));
		}

		bool WebViewAuth_ShouldStartLoad (UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType)
		{
			if (request.Url.Host == "www.yandex.ru") 
			{
				_continueAthorization (request.Url.Query);
			}
			return true;
		}

		public void FinishAuthorization()
		{
			_webView.RemoveFromSuperview ();
			_webView = null;
		}

	}
}

