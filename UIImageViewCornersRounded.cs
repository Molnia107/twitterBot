using System;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using System.Drawing;

namespace TwitterBot
{
	public class UIImageViewCornersRounded : UIImageView
	{

		public UIImageViewCornersRounded ():base()
		{

		}



		public void SetCorners(UIRectCorner corners, float cornerRadius)
		{
			var maskLayer = new CAShapeLayer();
			maskLayer.Frame = Bounds;
			var roundedPath = UIBezierPath.FromRoundedRect(maskLayer.Bounds, corners, new SizeF(cornerRadius, cornerRadius));
			maskLayer.FillColor = UIColor.Black.CGColor;
			maskLayer.BackgroundColor = UIColor.Clear.CGColor;
			maskLayer.Path = roundedPath.CGPath;



			//Don't add masks to layers already in the hierarchy!
			Layer.Mask = maskLayer;
		}

	}
}

