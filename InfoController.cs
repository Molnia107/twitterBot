using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace TwitterBot
{
	public class InfoController : UIViewController
	{
		InfoView _view;
		Info _info;
		NSUrl _callUrl = new NSUrl (Strings.CallUrl);

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
			if (UIApplication.SharedApplication.CanOpenUrl (_callUrl)) {
				var av = new UIAlertView (Strings.Warning,
					Strings.CallMessage,
					null,
					Strings.CommonButtons.Cancel,
					new string[]{ Strings.CommonButtons.OK });

				av.Show ();
				av.Clicked += CallMessage_Clicked;
			}
			else
			{
				var av = new UIAlertView (Strings.Information,
					Strings.CallNotAvailable,
					null,
					Strings.CommonButtons.OK,
					null);

				av.Show ();
			}

		}

		void CallMessage_Clicked(object sender, UIButtonEventArgs args)
		{
			if (args.ButtonIndex == 1) //OK
			{
				UIApplication.SharedApplication.OpenUrl (_callUrl);
			}
		}

		public void Email()
		{
			var url = new NSUrl(Strings.MailUrl);
			if (!UIApplication.SharedApplication.OpenUrl (url)) 
			{
				var av = new UIAlertView (Strings.Information,
					Strings.MailNotAvailable,
					null,
					Strings.CommonButtons.OK,
					null);

				av.Show ();
			}
		}
	}
}

