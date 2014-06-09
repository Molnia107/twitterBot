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

		public InfoView ()
		{
			_infoLabel = new UILabel ();
			_infoImage = new UIImageView ();
			_infoPhoneButton = UIButton.FromType (UIButtonType.System);
			_infoEmailButton = UIButton.FromType (UIButtonType.System);

			AddSubview (_infoImage);
			AddSubview (_infoLabel);
			AddSubview (_infoPhoneButton);
			AddSubview (_infoEmailButton);

			BackgroundColor = UIColor.White;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			var imageSize = _infoImage.Frame.Size;
			_infoImage.Frame = new RectangleF (new PointF ((Frame.Width - imageSize.Width) / 2, 75), imageSize);

			var infoSize = _infoLabel.Bounds;
			//_infoLabel.Lines = (int)(_infoLabel.Text.Length / 35);
			_infoLabel.Frame = new RectangleF (new PointF (10, _infoImage.Frame.Top + 30 + imageSize.Height), 
				new SizeF(Bounds.Width - 20,0 ));
			_infoLabel.SizeToFit ();

			var size = _infoPhoneButton.Bounds.Size;
			_infoPhoneButton.Frame = new RectangleF (new PointF (50, _infoLabel.Frame.Top + 30 + _infoLabel.Frame.Height), size);

			size = _infoEmailButton.Bounds.Size;
			_infoEmailButton.Frame = new RectangleF (new PointF (Frame.Width - size.Width - 50, _infoLabel.Frame.Top + 30 + _infoLabel.Frame.Height), size);



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

			_infoPhoneButton.SetBackgroundImage(UIImage.FromFile ("icon_phone.png"),UIControlState.Normal);
			_infoPhoneButton.SizeToFit ();
			_infoEmailButton.SetBackgroundImage(UIImage.FromFile ("icon_mail.png"),UIControlState.Normal);
			_infoEmailButton.SizeToFit ();

		}


	}
}

