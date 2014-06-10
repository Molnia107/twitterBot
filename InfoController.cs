using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

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
			_view = new InfoView (Call, Email);
			View = _view;
		}

		public override void ViewDidLoad ()
		{

			base.ViewDidLoad ();
			_view.ShowInfo (_info);
		}

		public void Call()
		{
			var url = new NSUrl ("tel:" + "+79310028990");
			if (!UIApplication.SharedApplication.OpenUrl (url)) 
			{
				var av = new UIAlertView ("Not supported",
					"Scheme 'tel:' is not supported on this device",
					null,
					"OK",
					null);

				av.Show ();
			}
		}

		public void Email()
		{
			var url = new NSUrl("mailto:you@gmail.com?subject=" + "hello@touchin.ru");
			if (!UIApplication.SharedApplication.OpenUrl (url)) 
			{
				var av = new UIAlertView ("Not supported",
					"Scheme 'mailto:' is not supported on this device",
					null,
					"OK",
					null);

				av.Show ();
			}
		}
	}
}

