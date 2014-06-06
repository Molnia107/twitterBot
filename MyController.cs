using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.Linq;

namespace TwitterBot
{
	public class MyController : UIViewController
	{
		string _tag;
		MainView _view;
		List<Twitt> _twittList;

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

		[Export("sender:event:")]
		private void OnClick(NSObject sender, UIEvent ev)
		{
			int i = 25;

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

			_twittList = ShyBot.GetTwitts (_tag);
			_view.DisplayTwitts (_twittList, GetMoreTwitts);

		}

		public List<Twitt> GetMoreTwitts()
		{
			var newTwitts = ShyBot.GetMoreTwitts (_tag,_twittList );
			if(newTwitts.Count > 0)
				newTwitts.RemoveAt (0);
			_twittList = _twittList.Concat (newTwitts).ToList();
			return newTwitts;
		}
	}
}

