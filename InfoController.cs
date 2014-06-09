using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class InfoController : UIViewController
	{
		InfoView _view;
		Info _info;

		public InfoController ()
		{
			_info = new Info ();
		}

		public override void LoadView ()
		{
			_view = new InfoView ();
			View = _view;
		}

		public override void ViewDidLoad ()
		{

			base.ViewDidLoad ();
			_view.ShowInfo (_info);
		}
	}
}

