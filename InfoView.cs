using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog.Utilities;
using System.Drawing;


namespace TwitterBot
{
	public class InfoView: UIView
	{
		UIImageView _infoImage;
		UILabel _infoLabel;
		UIButton _infoPhoneButton;
		UIButton _infoEmailButton;
		CallDelegate _callDelegate;
		EmailDelegate _emailDelegate;

		public delegate void CallDelegate();
		public delegate void EmailDelegate ();


		public InfoView (CallDelegate callDelegate, EmailDelegate emailDelegate)
		{
			_infoLabel = ViewFactory.CreateUILabel ();
			_infoImage = new UIImageView ();
			_infoPhoneButton = UIButton.FromType (UIButtonType.Custom);
			_infoEmailButton = UIButton.FromType (UIButtonType.Custom);

			AddSubview (_infoImage);
			AddSubview (_infoLabel);
			AddSubview (_infoPhoneButton);
			AddSubview (_infoEmailButton);

			BackgroundColor = UIColor.White;

			_callDelegate = callDelegate;
			_emailDelegate = emailDelegate;

			_infoPhoneButton.TouchUpInside += InfoPhoneButton_TouchUpInside;
			_infoEmailButton.TouchUpInside += InfoEmailButton_TouchUpInside;


		}


		void InfoPhoneButton_TouchUpInside(object sender, EventArgs ea)
		{
			if (_callDelegate != null)
				_callDelegate ();
		}

		void InfoEmailButton_TouchUpInside(object sender, EventArgs ea)
		{
			if (_emailDelegate != null)
				_emailDelegate ();
		}

		public override void LayoutSubviews ()
		{
		

			base.LayoutSubviews ();
			var imageSize = _infoImage.Frame.Size;
			_infoImage.Frame = new RectangleF (new PointF ((Frame.Width - imageSize.Width) / 2, ViewInfo.NavigationBarHeight + 10), imageSize);

			var infoSize = _infoLabel.Bounds;
			//_infoLabel.Lines = (int)(_infoLabel.Text.Length / 35);
			_infoLabel.Frame = new RectangleF (new PointF (10, _infoImage.Frame.Top + 30 + imageSize.Height), 
				new SizeF(Bounds.Width - 20,0 ));
			_infoLabel.SizeToFit ();

			var size = _infoPhoneButton.Bounds.Size;
			_infoPhoneButton.Frame = new RectangleF (new PointF (10, _infoLabel.Frame.Top + 20 + _infoLabel.Frame.Height), 
				new SizeF(100,50));

			size = _infoEmailButton.Bounds.Size;
			_infoEmailButton.Frame = new RectangleF (new PointF (Frame.Width - 110, _infoLabel.Frame.Top + 20 + _infoLabel.Frame.Height), 
				new SizeF(100,50));



		}

		public void ShowInfo(Info info)
		{
			_infoImage.Image = UIImage.FromFile ("logo.png");

			_infoImage.SizeToFit ();


			_infoLabel.LineBreakMode = UILineBreakMode.WordWrap;
			_infoLabel.Lines = 0;
			_infoLabel.Text = info.About;
			_infoLabel.Font = UIFont.SystemFontOfSize (12);
			//_infoLabel.SizeToFit();

			var buttonImg = UIImage.FromFile ("button.png");
			buttonImg = buttonImg.CreateResizableImage(new UIEdgeInsets(8,10,11,32));

			var selectedButtonImg = UIImage.FromFile ("button_pressed.png");
			selectedButtonImg = selectedButtonImg.CreateResizableImage(new UIEdgeInsets(8,10,11,32));


			_infoPhoneButton.SetBackgroundImage(buttonImg,UIControlState.Normal);
			_infoPhoneButton.SetImage(UIImage.FromFile ("icon_phone.png"),UIControlState.Normal);
			_infoPhoneButton.SetBackgroundImage(selectedButtonImg,UIControlState.Highlighted);
			_infoPhoneButton.ImageEdgeInsets = new UIEdgeInsets (-7, 0, 0, 0);

			//_infoPhoneButton.SizeToFit ();

			_infoEmailButton.SetBackgroundImage(buttonImg,UIControlState.Normal);
			_infoEmailButton.SetImage(UIImage.FromFile ("icon_mail.png"),UIControlState.Normal);
			_infoEmailButton.SetBackgroundImage(selectedButtonImg,UIControlState.Highlighted);
			_infoEmailButton.ImageEdgeInsets = new UIEdgeInsets (-7, 0, 0, 0);

			//_infoEmailButton.SizeToFit ();

		}


	}
}

