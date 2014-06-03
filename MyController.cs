using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class MyController : UIViewController
	{
		public MyController ()
		{
		}

		public override void LoadView ()
		{
			View = new MainView ();
		}
	}
}

