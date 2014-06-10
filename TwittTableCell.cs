using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog.Utilities;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace TwitterBot
{
	public class TwittTableCell : UITableViewCell, IImageUpdated
	{
		Twitt _twitt;
		UILabel _timeLabel;
		CGImage _mask;

		public TwittTableCell (UITableViewCellStyle style, string reuseIdentifier):base(style, reuseIdentifier)
		{
			BackgroundColor = UIColor.FromRGB (235, 235, 235);
			_timeLabel = new UILabel ();
			_timeLabel.TextColor = UIColor.FromRGB (159,159,159);
			_timeLabel.Font = UIFont.SystemFontOfSize (10);
			this.AddSubview (_timeLabel);
			TextLabel.Font = UIFont.BoldSystemFontOfSize (15);
			DetailTextLabel.TextColor = UIColor.FromRGB (159,159,159);

			var maskImg = UIImage.FromFile ("mask_avatar_mini.png").CGImage;
			_mask = CGImage.CreateMask(maskImg.Width, maskImg.Height,maskImg.BitsPerComponent,maskImg.BitsPerPixel, maskImg.BytesPerRow, maskImg.DataProvider, null, true);
		}

		public void UpdatedImage (Uri uri)
		{
			if (uri.OriginalString == _twitt.user.profile_image_url) 
			{
				var imageCG = ImageLoader.DefaultRequestImage (new Uri(_twitt.user.profile_image_url), this).CGImage;

				CGImage imgWithMaskCG = imageCG.WithMask (_mask);
				ImageView.Image = new UIImage (imgWithMaskCG);

			}
		}

		public void SetTwitt(Twitt twitt)
		{
			_twitt = twitt;
			TextLabel.Text = _twitt.user.name;

			DetailTextLabel.Text = _twitt.text;

			CGImage imageCG;
			var image = ImageLoader.DefaultRequestImage (new Uri (_twitt.user.profile_image_url), this);
			if (image != null)
				imageCG = image.CGImage;
			else
				imageCG = UIImage.FromFile ("avatar.png").CGImage;

			CGImage imgWithMaskCG = imageCG.WithMask (_mask);
			ImageView.Image = new UIImage (imgWithMaskCG);

			_timeLabel.Text = _twitt.GetAge ();
			_timeLabel.SizeToFit ();
			var size = _timeLabel.Frame.Size;
			_timeLabel.Frame = new RectangleF (new System.Drawing.PointF (this.Frame.Width - 25, this.Frame.Height - 25), size);


		}


	}
}

