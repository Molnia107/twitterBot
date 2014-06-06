using System;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace TwitterBot
{
	public class MainView: UIView
	{
		UITableView tableView ;
		TableSource tableSource;

		public MainView ()
		{
			BackgroundColor = UIColor.Blue;
			tableView = new UITableView ();
			AddSubview (tableView);
			tableSource = new TableSource ();
			tableView.Source = tableSource;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			tableView.Frame = Bounds;

		}

		public void DisplayTwitts(List<Twitt> twitts)
		{
			tableSource.SetSource (twitts);
			tableView.ReloadData ();

		}


	}
}

