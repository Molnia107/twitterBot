using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class TwittTabController : UITabBarController
	{

		TwittsGetByTagController[] _tabs;

		public TwittTabController ()
		{
			var tab1 = new TwittsGetByTagController ("Twitter");
			tab1.TabBarItem = new UITabBarItem ();
			tab1.TabBarItem.Title = "Twitter";
			tab1.TabBarItem.Image = UIImage.FromFile ("icon_twitter.png");

			var tab2 = new TwittsGetByTagController ("Dribbble");
			tab2.TabBarItem = new UITabBarItem ();
			tab2.TabBarItem.Title = "Dribbble";
			tab2.TabBarItem.Image = UIImage.FromFile ("icon_dribbble.png");

			var tab3 = new TwittsGetByTagController ("Apple");
			tab3.TabBarItem = new UITabBarItem ();
			tab3.TabBarItem.Title = "Apple";
			tab3.TabBarItem.Image = UIImage.FromFile ("icon_apple.png");

			var tab4 = new TwittsGetByTagController ("GitHub");
			tab4.TabBarItem = new UITabBarItem ();
			tab4.TabBarItem.Title = "GitHub";
			tab4.TabBarItem.Image = UIImage.FromFile ("icon_github.png");

			_tabs = new TwittsGetByTagController[] {
				tab1, tab2, tab3, tab4
			};

			SetViewControllers(_tabs,true);
//			SelectedIndex = 1;
			SelectedViewController = tab1;

			Title = "#Twitter";

			ViewControllerSelected += OnSelected;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		void OnSelected (object sender, UITabBarSelectionEventArgs e)
		{
			Title = "#"+_tabs [SelectedIndex].TabBarItem.Title;
		}


	}
}

