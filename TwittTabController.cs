using System;
using MonoTouch.UIKit;
using System.Drawing;

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
			tab1.TabBarItem.Image = UIImage.FromFile (ImagePathes.IconTwitter);

			var tab2 = new TwittsGetByTagController ("Dribbble");
			tab2.TabBarItem = new UITabBarItem ();
			tab2.TabBarItem.Title = "Dribbble";
			tab2.TabBarItem.Image = UIImage.FromFile (ImagePathes.IconDribble);

			var tab3 = new TwittsGetByTagController ("Apple");
			tab3.TabBarItem = new UITabBarItem ();
			tab3.TabBarItem.Title = "Apple";
			tab3.TabBarItem.Image = UIImage.FromFile (ImagePathes.IconApple);

			var tab4 = new TwittsGetByTagController ("GitHub");
			tab4.TabBarItem = new UITabBarItem ();
			tab4.TabBarItem.Title = "GitHub";
			tab4.TabBarItem.Image = UIImage.FromFile (ImagePathes.IconGithub);

			_tabs = new TwittsGetByTagController[] {
				tab1, tab2, tab3, tab4
			};

			//???SetViewControllers(_tabs,true);
			//			SelectedIndex = 1;
			ViewControllerSelected += OnSelected;


			//Title = "#Twitter";


		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();
			//http://stackoverflow.com/questions/21725010/tabbarcontroller-first-view-is-offset-by-20px
			if (View != null && View.Subviews != null && View.Subviews.Length > 0 && View.Subviews [0].Subviews != null && View.Subviews [0].Subviews.Length > 0) 
			{
				var wrapperView = View.Subviews [0].Subviews [0];
				if (wrapperView.Frame.Y == 20) {
					var frame = wrapperView.Frame;
					frame.Y = 0;
					frame.Height += 20;
					wrapperView.Frame = frame;
				}
			}
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			if(ViewControllers == null)
				SetViewControllers(_tabs,true);
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

