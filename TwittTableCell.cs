using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog.Utilities;
using System.Drawing;

namespace TwitterBot
{
	public class TwittTableCell : UITableViewCell, IImageUpdated
	{
		Twitt _twitt;
		UILabel _timeLabel;

		public TwittTableCell (UITableViewCellStyle style, string reuseIdentifier):base(style, reuseIdentifier)
		{
			BackgroundColor = UIColor.FromRGB (235, 235, 235);
			_timeLabel = new UILabel ();
			_timeLabel.TextColor = UIColor.FromRGB (159,159,159);
			_timeLabel.Font = UIFont.SystemFontOfSize (10);
			this.AddSubview (_timeLabel);
			TextLabel.Font = UIFont.BoldSystemFontOfSize (15);
			DetailTextLabel.TextColor = UIColor.FromRGB (159,159,159);

		}

		public void UpdatedImage (Uri uri)
		{
			if (uri.OriginalString == _twitt.user.profile_image_url) 
			{
				var image = ImageLoader.DefaultRequestImage (new Uri(_twitt.user.profile_image_url), this);
				ImageView.Image = image;
			}
		}

		public void SetTwitt(Twitt twitt)
		{
			_twitt = twitt;
			TextLabel.Text = _twitt.user.name;

			DetailTextLabel.Text = _twitt.text;

			var image = ImageLoader.DefaultRequestImage (new Uri (_twitt.user.profile_image_url), this);
			if (image != null)
				ImageView.Image = image;
			else
				ImageView.Image = UIImage.FromFile ("avatar.png");
			//ImageView.Frame = new RectangleF(10,10,10,10);

			_timeLabel.Text = _twitt.GetAge ();
			_timeLabel.SizeToFit ();
			var size = _timeLabel.Frame.Size;
			_timeLabel.Frame = new RectangleF (new System.Drawing.PointF (this.Frame.Width - 25, this.Frame.Height - 25), size);


		}


	}
}

