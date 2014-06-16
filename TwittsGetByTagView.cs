using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;
using System.Drawing;
using BigTed;
using MonoTouch.ObjCRuntime;

namespace TwitterBot
{
	public class TwittsGetByTagView: UIView
	{

		UITableView _tableView ;
		TwittsTableSource _tableSource;
		UIWebView _webView;
		UIView _footerView;
		UIButton _buttonLoadTwitts;
		GetMoreTwittsDelegate _getMoreTwittsDelegate;
		ViewTwittDelegate _viewTwittDelegate;


		public delegate void GetMoreTwittsDelegate ();
		public delegate void ViewTwittDelegate(Twitt twitt);

		public TwittsGetByTagView ()
		{
			BackgroundColor = UIColor.White;
			_tableView = new UITableView ();

			_tableSource = new TwittsTableSource (TableSource_RowSelected);
			_buttonLoadTwitts  = UIButton.FromType (UIButtonType.System);
			_footerView = new UIView ();

			AddSubview (_tableView);
			InitTableViewFooter ();

			if(_tableView.RespondsToSelector(new Selector("setSeparatorInset:")))
				_tableView.SeparatorInset = new UIEdgeInsets (0, 0, 0, 0);

			_tableView.Source = _tableSource;

		}



		void ButtonLoadTwitts_TouchUpInside (object sender, EventArgs ea) 
		{
			_buttonLoadTwitts.Enabled = false;
			_getMoreTwittsDelegate ();

		}
			

		public void TableSource_RowSelected (Twitt twitt)
		{
			if (_viewTwittDelegate != null) 
			{
				_viewTwittDelegate(twitt);
			}
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			var tableOffset = _tableView.ContentOffset;

			_tableView.Frame = Bounds;
			_tableView.ContentOffset = tableOffset;


			_tableView.ContentInset = new UIEdgeInsets (ViewInfo.NavigationBarHeight, 0, ViewInfo.TabBarHeight+50, 0);
			_tableView.ScrollIndicatorInsets = new UIEdgeInsets (ViewInfo.NavigationBarHeight , 0, ViewInfo.TabBarHeight, 0);


			_tableView.TableFooterView.Frame = new System.Drawing.RectangleF (_tableView.TableFooterView.Frame.Location,new SizeF(Bounds.Width,50));
			_footerView.Frame = new RectangleF(new PointF(0,0),_tableView.TableFooterView.Bounds.Size);
			_buttonLoadTwitts.Center = _footerView.Center;


		}

		public void DisplayTwitts(List<Twitt> twitts, GetMoreTwittsDelegate getMoreTwitts, ViewTwittDelegate viewTwittDelegate)
		{
			_tableSource.SetSource (twitts);
			_tableView.ReloadData ();
			_getMoreTwittsDelegate = getMoreTwitts;
			_viewTwittDelegate = viewTwittDelegate;
			BTProgressHUDProvider.HideBTProgressHUD ();
			_tableView.TableFooterView.Hidden = false;
		}

		public void DisplayNewTwitts(List<Twitt> newTwittList)
		{
			_tableSource.AddNewTwittsToSource(newTwittList);
			List<NSIndexPath> tmpList = new List<NSIndexPath>(); 
			var rowsInSection = _tableView.NumberOfRowsInSection (0);
			for(int i = rowsInSection; i < rowsInSection  + newTwittList.Count; i++) 
			{ 
				NSIndexPath tmpIndexPath = NSIndexPath.FromRowSection(i,0); 
				tmpList.Add(tmpIndexPath);	
			} 

			_tableView.InsertRows(tmpList.ToArray(), UITableViewRowAnimation.None);
			BTProgressHUDProvider.HideBTProgressHUD ();
			_buttonLoadTwitts.Enabled = true;
		}


		public void InitTableViewFooter()
		{
			_footerView.ClipsToBounds = true;
			_tableView.TableFooterView = new UIView ();
			_tableView.TableFooterView.Hidden = true;
			_tableView.TableFooterView.AddSubview(_footerView);

			_buttonLoadTwitts.SetTitle (Strings.ShowMore, UIControlState.Normal);

			_buttonLoadTwitts.SizeToFit ();
			_buttonLoadTwitts.TouchUpInside += ButtonLoadTwitts_TouchUpInside;

			_footerView.AddSubview (_buttonLoadTwitts);
		}

		public void ScrollToTop()
		{
			_tableView.ContentOffset = new PointF (0, -1*ViewInfo.NavigationBarHeight);
		}






	}
}

