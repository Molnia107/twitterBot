using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog.Utilities;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace TwitterBot
{
	public class TwittView: UIView, IImageUpdated
	{
		UIImageView _backgroundImage;
		UIImageViewCornersRounded _userImage;
		UIImageView _userMaskImage;
		UILabel _userNameLabel;
		UILabel _viaLabel;
		UILabel _twittTextLabel;
		UIImageView _lineImage;
		UILabel _twittDateLabel;
		UILabel _twittUrlLabel;
		Twitt _twitt;
		CGImage _mask;


		public TwittView ()
		{
			_backgroundImage = new UIImageView ();
			_userImage = new UIImageViewCornersRounded ();

			_userNameLabel = new UILabel ();
			_userMaskImage = new UIImageView ();
			_viaLabel = new UILabel ();
			_twittTextLabel = new UILabel ();
			_lineImage = new UIImageView ();
			_twittDateLabel = new UILabel ();
			_twittUrlLabel = new UILabel ();

			AddSubview (_backgroundImage);
			//AddSubview (_userImage);
			//AddSubview (_userMaskImage);
			AddSubview (_userNameLabel);
			AddSubview (_viaLabel);
			AddSubview (_twittTextLabel);
			AddSubview (_lineImage);
			AddSubview (_twittDateLabel);
			AddSubview (_twittUrlLabel);
			AddSubview (_userMaskImage);

			_backgroundImage.Image = UIImage.FromFile ("bg.png");

			_userNameLabel.Font = UIFont.BoldSystemFontOfSize (14);
			_userNameLabel.TextColor = UIColor.FromRGB (69, 99, 141);

			_viaLabel.Font = UIFont.BoldSystemFontOfSize (12);

			_twittTextLabel.Font = UIFont.SystemFontOfSize (12);

			_twittTextLabel.LineBreakMode = UILineBreakMode.WordWrap;
			_twittTextLabel.Lines = 0;

			_lineImage.Image = UIImage.FromFile ("line.png");
			_lineImage.SizeToFit ();

			_twittDateLabel.Font = UIFont.SystemFontOfSize (10);
			_twittDateLabel.TextColor = UIColor.FromRGB (123,123,123);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			_backgroundImage.Frame = Bounds;

			//var size = _userImage.Frame.Size;
			//_userImage.Frame = new RectangleF (new PointF (30, 100), size);

			var size = _userMaskImage.Frame.Size;
			_userMaskImage.Frame = new RectangleF (new PointF (30, 100), size);

			size = _userNameLabel.Frame.Size;
			_userNameLabel.Frame = new RectangleF( new PointF(30 + _userMaskImage.Frame.Width + 20, 110), size);


			size = _viaLabel.Frame.Size;
			_viaLabel.Frame = new RectangleF(new PointF(30 + _userMaskImage.Frame.Width + 20,_userNameLabel.Frame.Top+_userNameLabel.Frame.Height+5), size);
		
		
			_twittTextLabel.Frame = new RectangleF(new PointF(10, _userMaskImage.Frame.Top + _userMaskImage.Frame.Height + 20), 
				new SizeF(Frame.Width - 20,0));
			_twittTextLabel.SizeToFit ();

			_lineImage.Frame = new RectangleF (new PointF (10, _twittTextLabel.Frame.Top + _twittTextLabel.Frame.Height + 20),
				_lineImage.Frame.Size);

			_twittDateLabel.Frame = new RectangleF (new PointF (10, _lineImage.Frame.Top + _lineImage.Frame.Height + 10), 
				_twittDateLabel.Frame.Size);
		}

		public void ShowTwitt(Twitt twitt)
		{
			_twitt = twitt;

			var image = ImageLoader.DefaultRequestImage (new Uri (_twitt.user.profile_image_url), this);
			if (image != null)
				_mask = image.CGImage;
			//_userImage.Image = image;
			else
				_mask = UIImage.FromFile ("avatar.png").CGImage;


			//_mask = CGImage.FromPNG (new CGDataProvider ("avatar.png"), null, true, new CGColorRenderingIntent ());
			var mask = CGImage.CreateMask(50,50,_mask.BitsPerComponent,_mask.BitsPerPixel, _mask.BytesPerRow,new CGDataProvider ("mask_avatar@2x.png"), null, true);
			_mask.WithMask (mask);
			_userMaskImage.Image = new UIImage(_mask);
			_userMaskImage.SizeToFit ();
			//_userMaskImage.Bounds = new RectangleF (0, 0, 50, 50);


			_userNameLabel.Text = _twitt.user.name;
			_userNameLabel.SizeToFit ();

			_viaLabel.Text = "via "+_twitt.source.Substring (_twitt.source.IndexOf ('>')+1, _twitt.source.LastIndexOf ('<') - _twitt.source.IndexOf ('>') - 1);
			_viaLabel.SizeToFit ();

			//"<a href=\"http://www.retweet-xl.com/\" rel=\"nofollow\">أقوى رتويت</a>"

			_twittTextLabel.Text = _twitt.text;

			_twittDateLabel.Text = _twitt.GetDate ();
			_twittDateLabel.SizeToFit ();

		}

		public void UpdatedImage (Uri uri)
		{
			if (uri.OriginalString == _twitt.user.profile_image_url) 
			{
				var image = ImageLoader.DefaultRequestImage (new Uri(_twitt.user.profile_image_url), this);
				_mask = image.CGImage;
				_mask.WithMask (CGImage.FromPNG (new CGDataProvider ("mask_avatar.png"), null, true, new CGColorRenderingIntent ()));
				_userMaskImage.Image = new UIImage(_mask);
				_userMaskImage.SizeToFit ();
				/*
				_userImage.Image = image;
				_userImage.SizeToFit ();
				_userImage.SetCorners (UIRectCorner.AllCorners, 2);*/
			}
		}
	}
}

