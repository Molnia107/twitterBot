using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	static public class ViewFactory
	{
		static ViewFactory ()
		{


		}

		public static UILabel CreateUILabel()
		{
			UILabel label = new UILabel ();
			label.BackgroundColor = UIColor.Clear;
			return label;
		}



	}
}

