using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog.Utilities;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace TwitterBot
{
	public class TwittView: UIView, IImageUpdated
	{
		UIImageView _backgroundImageView;
		UIImageViewCornersRounded _userImage;
		UIImageView _userMaskImageView;
		UILabel _userNameLabel;
		UILabel _viaLabel;
		UILabel _twittTextLabel;
		UIImageView _lineImageView;
		UILabel _twittDateLabel;
		UILabel _twittUrlLabel;
		Twitt _twitt;
		CGImage _avatarCG;
		CGImage _mask;


		public TwittView ()
		{
			_backgroundImageView = new UIImageView ();
			_userNameLabel = new UILabel ();
			_userMaskImageView = new UIImageView ();
			_viaLabel = new UILabel ();
			_twittTextLabel = new UILabel ();
			_lineImageView = new UIImageView ();
			_twittDateLabel = new UILabel ();
			_twittUrlLabel = new UILabel ();

			AddSubview (_backgroundImageView);
			AddSubview (_userNameLabel);
			AddSubview (_viaLabel);
			AddSubview (_twittTextLabel);
			AddSubview (_lineImageView);
			AddSubview (_twittDateLabel);
			AddSubview (_twittUrlLabel);
			AddSubview (_userMaskImageView);

			_backgroundImageView.Image = UIImage.FromFile ("bg.png");

			_userNameLabel.Font = UIFont.BoldSystemFontOfSize (14);
			_userNameLabel.TextColor = UIColor.FromRGB (69, 99, 141);

			_viaLabel.Font = UIFont.BoldSystemFontOfSize (12);

			_twittTextLabel.Font = UIFont.SystemFontOfSize (12);

			_twittTextLabel.LineBreakMode = UILineBreakMode.WordWrap;
			_twittTextLabel.Lines = 0;

			_lineImageView.Image = UIImage.FromFile ("line.png");
			_lineImageView.SizeToFit ();

			_twittDateLabel.Font = UIFont.SystemFontOfSize (10);
			_twittDateLabel.TextColor = UIColor.FromRGB (123,123,123);

			var maskImg = UIImage.FromFile ("mask_avatar.png").CGImage;
			_mask = CGImage.CreateMask(maskImg.Width, maskImg.Height,maskImg.BitsPerComponent,maskImg.BitsPerPixel, maskImg.BytesPerRow, maskImg.DataProvider, null, true);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			_backgroundImageView.Frame = Bounds;

			//var size = _userImage.Frame.Size;
			//_userImage.Frame = new RectangleF (new PointF (30, 100), size);

			var size = _userMaskImageView.Frame.Size;
			_userMaskImageView.Frame = new RectangleF (new PointF (30, 100), size);

			size = _userNameLabel.Frame.Size;
			_userNameLabel.Frame = new RectangleF( new PointF(30 + _userMaskImageView.Frame.Width + 20, 110), size);


			size = _viaLabel.Frame.Size;
			_viaLabel.Frame = new RectangleF(new PointF(30 + _userMaskImageView.Frame.Width + 20,_userNameLabel.Frame.Top+_userNameLabel.Frame.Height+5), size);
		
		
			_twittTextLabel.Frame = new RectangleF(new PointF(10, _userMaskImageView.Frame.Top + _userMaskImageView.Frame.Height + 20), 
				new SizeF(Frame.Width - 20,0));
			_twittTextLabel.SizeToFit ();

			_lineImageView.Frame = new RectangleF (new PointF (10, _twittTextLabel.Frame.Top + _twittTextLabel.Frame.Height + 20),
				_lineImageView.Frame.Size);

			_twittDateLabel.Frame = new RectangleF (new PointF (10, _lineImageView.Frame.Top + _lineImageView.Frame.Height + 10), 
				_twittDateLabel.Frame.Size);
		}

		public void ShowTwitt(Twitt twitt)
		{
			_twitt = twitt;

			var image = ImageLoader.DefaultRequestImage (new Uri (_twitt.user.profile_image_url), this);
			if (image != null)
				_avatarCG = image.CGImage;
			else
				_avatarCG = UIImage.FromFile ("avatar.png").CGImage;


			CGImage imgWithMaskCG = _avatarCG.WithMask (_mask);
			_userMaskImageView.Image = new UIImage(imgWithMaskCG);
			_userMaskImageView.SizeToFit ();

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
				_avatarCG = image.CGImage;
				CGImage imgWithMaskCG = _avatarCG.WithMask (_mask);
				_userMaskImageView.Image = new UIImage(imgWithMaskCG);
				_userMaskImageView.SizeToFit ();


			}
		}
	}
}

