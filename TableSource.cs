﻿using System;
using MonoTouch.UIKit;
using System.Collections.Generic;


namespace TwitterBot
{
	public class TableSource : UITableViewSource
	{
		protected List<Twitt> tableItems;
		protected string cellIdentifier = "TableCell";
		public TableSource (List<Twitt> items)
		{
			tableItems = items;
		}
		public override int RowsInSection (UITableView tableview, int section)
		{
			return tableItems.Count;
		}
		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new TwittTableCell (UITableViewCellStyle.Subtitle, cellIdentifier,tableItems [indexPath.Row]);
			return cell;
		}


	}
}

