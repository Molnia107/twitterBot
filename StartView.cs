using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;
using BigTed;

namespace TwitterBot
{
	public class StartView: UIView
	{
		UILabel _welcomeLabel;
		UIButton _loginButton;
		UIButton _logoutButton;
		LoginDelegate _loginDelegate;
		LogoutDelegate _logoutDelegate;
		ContinueAuthorizationDelegate _continueAthorizationDelegate;
		UIWebView _webView;

		public delegate void LoginDelegate();
		public delegate void LogoutDelegate();
		public delegate void ContinueAuthorizationDelegate(string query);

		public StartView (LoginDelegate loginDelegate, LogoutDelegate logoutDelegate)
		{
			_loginDelegate = loginDelegate;
			_logoutDelegate = logoutDelegate;

			_welcomeLabel = new UILabel ();
			_loginButton = UIButton.FromType (UIButtonType.System);
			_logoutButton = UIButton.FromType (UIButtonType.System);

			_webView = new UIWebView ();
			_webView.ShouldStartLoad += WebViewAuth_ShouldStartLoad;


			_welcomeLabel.Text = Strings.Welcome;
			_welcomeLabel.SizeToFit ();
			_loginButton.SetTitle (Strings.Login, UIControlState.Normal);
			_loginButton.SizeToFit ();
			_logoutButton.SetTitle (Strings.Logout, UIControlState.Normal);
			_logoutButton.SizeToFit ();

			AddSubview (_welcomeLabel);
			AddSubview (_loginButton);
			AddSubview (_logoutButton);

			BackgroundColor = UIColor.White;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			var welcomeSize = _welcomeLabel.Frame.Size;
			_welcomeLabel.Frame = new RectangleF (new PointF ((Frame.Width - welcomeSize.Width) / 2, ViewInfo.NavigationBarHeight + 40), welcomeSize);

			var loginSize = _loginButton.Frame.Size;
			_loginButton.Frame = new RectangleF(new PointF((Frame.Width - loginSize.Width) / 2, _welcomeLabel.Frame.Top + welcomeSize.Height + 100), loginSize);

			var logoutSize = _logoutButton.Frame.Size;
			_logoutButton.Frame = new RectangleF(new PointF((Frame.Width - logoutSize.Width) / 2, _welcomeLabel.Frame.Top + welcomeSize.Height + 100), logoutSize);
		
			_loginButton.TouchUpInside += LoginButton_TouchUpInside;
			_logoutButton.TouchUpInside += LogoutButton_TouchUpInside;

			_webView.Frame = new RectangleF (new PointF (0, ViewInfo.NavigationBarHeight), new SizeF (Frame.Width, Frame.Height - ViewInfo.NavigationBarHeight));
		
		}

		public void HideLogin()
		{
			_loginButton.Hidden = true;
			_logoutButton.Hidden = false;
			_webView.RemoveFromSuperview ();
		}

		public void HideLogout()
		{
			_loginButton.Hidden = false;
			_logoutButton.Hidden = true;
			_webView.RemoveFromSuperview ();
		}

		void LoginButton_TouchUpInside (object sender, EventArgs ea) 
		{
			if (_loginDelegate != null)
				_loginDelegate ();
		}

		void LogoutButton_TouchUpInside (object sender, EventArgs ea) 
		{
			if (_logoutDelegate != null)
				_logoutDelegate ();
		}

		bool WebViewAuth_ShouldStartLoad (UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType)
		{
			if (request.Url.Host == "www.yandex.ru") 
			{
				_continueAthorizationDelegate (request.Url.Query);
			}
			return true;
		}

		public void DisplayAuthWebView(string authUrl, ContinueAuthorizationDelegate continueAuthorization)
		{
			BTProgressHUDProvider.HideBTProgressHUD ();
			if (authUrl != null) {
				_continueAthorizationDelegate = continueAuthorization;
				_webView.LoadRequest (new NSUrlRequest (new NSUrl (authUrl)));
			}
			AddSubview (_webView);

		}
			

		public void FinishAuthorization()
		{
			_webView.RemoveFromSuperview ();
		}


	}
}

