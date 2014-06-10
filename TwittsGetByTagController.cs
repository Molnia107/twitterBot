using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.Linq;


namespace TwitterBot
{
	public class TwittsGetByTagController : UIViewController
	{
		string _tag;
		TwittsGetByTagView _view;
		List<Twitt> _twittList;
		bool _firstTimeView = true;

		public TwittsGetByTagController (string tag)
		{
			_tag = tag;

		}
		/* example
		[Export("sender:event:")]
		private void OnClick(NSObject sender, UIEvent ev)
		{
			int i = 25;

		}
		*/
		private void ContinueAuthorization(string query)
		{
			ShyBot.Authorize (query);
			_view.FinishAuthorization ();

			GetTwitts ();

			//else msg
		}

		void GetTwitts()
		{
			_view.ShowBTProgressHUD ();
			_twittList = ShyBot.GetTwitts (_tag);
			_view.DisplayTwitts (_twittList, GetMoreTwitts, PushTwittToNavigator);
			_view.HideBTProgressHUD ();
		}

		public override void LoadView ()
		{
			_view = new TwittsGetByTagView ();
			View = _view;

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			if(_firstTimeView)
				_view.ScrollToTop ();
			_firstTimeView = false;
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

		public List<Twitt> GetMoreTwitts()
		{
			_view.ShowBTProgressHUD ();
			var newTwitts = ShyBot.GetMoreTwitts (_tag,_twittList );
			if(newTwitts.Count > 0)
				newTwitts.RemoveAt (0);
			_twittList = _twittList.Concat (newTwitts).ToList();
			_view.HideBTProgressHUD ();
			return newTwitts;
		}


		public void PushTwittToNavigator(Twitt twitt)
		{
			if (NavigationController is ShyBotNavigationController) 
			{
				(NavigationController as ShyBotNavigationController).TwittTableSource_SelectedRow (twitt);
			}
		}
	}
}

