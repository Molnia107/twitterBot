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
			_view.DisplayAuthWebView (authUrl, ContinueAuthorization);

		}


		private void ContinueAuthorization(string query)
		{
			ShyBot.Authorize (query);
			_view.FinishAuthorization ();

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

