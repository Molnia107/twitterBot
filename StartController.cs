using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class StartController : UIViewController, IAuthorizationRecepient
	{
		StartView _view;
		UIBarButtonItem _backButton;

		public StartController ()
		{
			_backButton = new UIBarButtonItem (Strings.Back, UIBarButtonItemStyle.Plain, 
				(sender, args) => {
					ToTwitts();
				});
		}

		public override void LoadView ()
		{
			_view = new StartView (Login, Logout);
			View = _view;

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			if (ShyBot.IsAuthorized) 
			{
				_view.HideLogin ();
				NavigationItem.SetRightBarButtonItem (_backButton, true);

			} 
			else 
			{
				_view.HideLogout ();
				NavigationItem.SetRightBarButtonItem (null, false);
			}
		}

		public void Login()
		{
			if (!ShyBot.IsAuthorized) 
			{
				BTProgressHUDProvider.ShowBTProgressHUD ();
				ShyBot.Authontificate (this);
			}
			else
				ToTwitts ();
		}

		public void Logout()
		{
			_view.DisplayAuthWebView (null, null);
		}

		public void ToTwitts()
		{
			if (NavigationController is ShyBotNavigationController) 
			{
				(NavigationController as ShyBotNavigationController).StartController_Login ();
			}
		}

		public void SetNetworkError()
		{

			BeginInvokeOnMainThread (delegate {
				var av = new UIAlertView (Strings.NetworkProblems,
					Strings.NetworkProblemsMessage,
					null,
					Strings.TryAgain,
					null);

				av.Show ();
				av.Clicked += NetworkErrorMessage_Clicked;
			});
		}

		void NetworkErrorMessage_Clicked(object sender, UIButtonEventArgs args)
		{

		}

		public void Authontificate (string url)
		{
			//authorize user
			BeginInvokeOnMainThread (delegate {
				_view.DisplayAuthWebView (url, ContinueAuthorization);
			});
		}

		public void SetAuthorizationRezult(bool rezult)
		{
			if (rezult) 
			{
				BeginInvokeOnMainThread (delegate {
					_view.FinishAuthorization ();
					ToTwitts();
				});
			}
		}

		private void ContinueAuthorization(string query)
		{
			ShyBot.Authorize (query, this);

			//else msg
		}
	}
}

