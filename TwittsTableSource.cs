using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace TwitterBot
{
	public class TwittsTableSource : UITableViewSource
	{

		List<Twitt> tableItems;
		protected string cellIdentifier = "TableCell";
		ViewSelectedTwittDelegate _viewSelectedTwittDelegate;

		public event Action cl;
		public delegate void ViewSelectedTwittDelegate(Twitt twitt);


		public TwittsTableSource (ViewSelectedTwittDelegate viewSelectedTwittDelegate)
		{
			tableItems = new List<Twitt> ();
			_viewSelectedTwittDelegate = viewSelectedTwittDelegate;
		}
		public override int RowsInSection (UITableView tableview, int section)
		{

			return tableItems.Count;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			if (_viewSelectedTwittDelegate != null)
				_viewSelectedTwittDelegate (tableItems [indexPath.Row]);
			tableView.DeselectRow (indexPath, true); 
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Row > 11) {
			}
			// request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);

			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new TwittTableCell (UITableViewCellStyle.Subtitle, cellIdentifier);
			(cell as TwittTableCell).SetTwitt (tableItems[indexPath.Row]);
			return cell;
		}



		public void SetSource(List<Twitt> twitts)
		{
			tableItems = twitts;

		}

		public void AddNewTwittsToSource(List<Twitt> twitts)
		{
			tableItems = tableItems.Concat (twitts).ToList ();

		}

		public Twitt GetTwitt(NSIndexPath indexPath)
		{
			return tableItems [indexPath.Row];
		}

//		
	}
}

