using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace TwitterBot
{
	public class InfoController : UIViewController
	{
		InfoView _view;
		Info _info;
		NSUrl _callUrl = new NSUrl ("tel:+79310028990");

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
				var av = new UIAlertView ("Предупреждение",
					         "Произвести звонок на номер +7 931 00 28 990?",
					         null,
					         "Отмена",
					         new string[]{ "ОК" });

				av.Show ();
				av.Clicked += CallMessage_Clicked;
			}
			else
			{
				var av = new UIAlertView ("Информация",
					"Звонок с данного устройства не возможен",
					null,
					"ОК",
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
			var url = new NSUrl("mailto:hello@touchin.ru?subject=mail");
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

