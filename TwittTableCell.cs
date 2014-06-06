using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog.Utilities;

namespace TwitterBot
{
	public class TwittTableCell : UITableViewCell, IImageUpdated
	{
		Twitt _twitt;
		public TwittTableCell (UITableViewCellStyle style, string reuseIdentifier):base(style, reuseIdentifier)
		{

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
			ImageView.SizeToFit ();

			UILabel timeLabel = new UILabel ();
			timeLabel.Text = _twitt.created_at;
			timeLabel.SizeToFit ();
			var size = timeLabel.Frame.Size;
			timeLabel.Frame = new System.Drawing.RectangleF (new System.Drawing.PointF (this.Frame.Width - 20, this.Frame.Height - 30), size);
			timeLabel.BackgroundColor = UIColor.Red;
			this.AddSubview (timeLabel);

		}
	}
}

