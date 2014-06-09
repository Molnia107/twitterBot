using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class TwittController : UIViewController
	{
		TwittView _view;
		Twitt _twitt;

		public TwittController ()
		{

		}

		public override void LoadView ()
		{
			_view = new TwittView ();
			View = _view;

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			_view.ShowTwitt (_twitt);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}

		public void RefreshTwitt(Twitt twitt)
		{
			_twitt = twitt;

		}

	}
}

