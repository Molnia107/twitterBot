using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class TwittTabController : UITabBarController
	{
		public TwittTabController ()
		{
			var tab1 = new MyController ("Twitter");
			tab1.TabBarItem = new UITabBarItem ();
			tab1.TabBarItem.Title = "Twitter";
			tab1.TabBarItem.Image = UIImage.FromFile ("icon_twitter.png");

			var tab2 = new MyController ("Dribbble");
			tab2.TabBarItem = new UITabBarItem ();
			tab2.TabBarItem.Title = "Dribbble";
			tab2.TabBarItem.Image = UIImage.FromFile ("icon_dribbble.png");

			var tab3 = new MyController ("Apple");
			tab3.TabBarItem = new UITabBarItem ();
			tab3.TabBarItem.Title = "Apple";
			tab3.TabBarItem.Image = UIImage.FromFile ("icon_apple.png");

			var tab4 = new MyController ("GitHub");
			tab4.TabBarItem = new UITabBarItem ();
			tab4.TabBarItem.Title = "GitHub";
			tab4.TabBarItem.Image = UIImage.FromFile ("icon_github.png");

			var tabs = new UIViewController[] {
				tab1, tab2, tab3, tab4
			};

			SetViewControllers(tabs,true);

		}
	}
}

