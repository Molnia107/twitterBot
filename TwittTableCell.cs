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
			ContentView.BackgroundColor = UIColor.FromRGB (235, 235, 235);

			_timeLabel = ViewFabric.CreateUILabel ();
			_timeLabel.TextColor = UIColor.FromRGB (159,159,159);
			_timeLabel.Font = UIFont.SystemFontOfSize (10);
			ContentView.AddSubview (_timeLabel);

			TextLabel.BackgroundColor = UIColor.Clear;
			TextLabel.Font = UIFont.BoldSystemFontOfSize (15);
			DetailTextLabel.BackgroundColor = UIColor.Clear;
			DetailTextLabel.TextColor = UIColor.FromRGB (159,159,159);


			var maskImg = UIImage.FromFile ("mask_avatar_mini.png").CGImage;
			_mask = CGImage.CreateMask(maskImg.Width, maskImg.Height,maskImg.BitsPerComponent,maskImg.BitsPerPixel, maskImg.BytesPerRow, maskImg.DataProvider, null, true);

			var size = _timeLabel.Frame.Size;
			_timeLabel.Frame = new RectangleF (new System.Drawing.PointF (Bounds.Width - 25, 15), size);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			//Frame = new RectangleF(Bounds.Location, new SizeF(Bounds.Width, 70));
			ImageView.Frame = new RectangleF (5,5,Bounds.Height-10, Bounds.Height-10);


			TextLabel.Frame = new RectangleF (new PointF(ImageView.Bounds.Width + 15, 5), new SizeF( Bounds.Width - ImageView.Bounds.Width - 20,TextLabel.Bounds.Height));
			DetailTextLabel.Frame = new RectangleF (new PointF(ImageView.Bounds.Width + 15, 25), new SizeF(Bounds.Width - ImageView.Bounds.Width - 20,DetailTextLabel.Bounds.Height));


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
			//ImageView.SizeToFit ();



			_timeLabel.Text = _twitt.GetAge ();
			_timeLabel.SizeToFit ();



		}


	}
}

