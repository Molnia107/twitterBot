using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.Linq;


namespace TwitterBot
{
	public class TwittsGetByTagController : UIViewController, IRecepient
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
			ShyBot.Authorize (query, this);

			//else msg
		}

		void GetTwitts()
		{
			_view.ShowBTProgressHUD ();
			ShyBot.GetTwitts (_tag, this);

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
				ShyBot.Authontificate (this);
			else 
			{
				GetTwitts ();
			}
		}



		public void GetMoreTwitts()
		{
			_view.ShowBTProgressHUD ();
			ShyBot.GetMoreTwitts (_tag,this,_twittList );
		}


		public void PushTwittToNavigator(Twitt twitt)
		{
			if (NavigationController is ShyBotNavigationController) 
			{
				(NavigationController as ShyBotNavigationController).TwittTableSource_SelectedRow (twitt);
			}
		}

		public void SetTwitts(List<Twitt> newTwittList)
		{
			if (_twittList == null) 
			{
				_twittList = newTwittList;
				BeginInvokeOnMainThread(delegate{_view.DisplayTwitts (_twittList, GetMoreTwitts, PushTwittToNavigator);});
			} 
			else 
			{
				if(newTwittList.Count > 0)
					newTwittList.RemoveAt (0);
				_twittList = _twittList.Concat (newTwittList).ToList();
				BeginInvokeOnMainThread(delegate{_view.DisplayNewTwitts (newTwittList);});
			}
		}

		public void SetNetworkError()
		{

			BeginInvokeOnMainThread (delegate {
				_view.HideBTProgressHUD ();
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
			ShyBot.TryAgain (this,_tag, _twittList);
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
					GetTwitts ();
				});
			}
		}
	}
}

