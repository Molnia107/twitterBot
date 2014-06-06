using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;
using System.Drawing;

namespace TwitterBot
{
	public class MainView: UIView
	{
		UITableView _tableView ;
		TableSource _tableSource;
		UIWebView _webView;
		UIView _footerView;
		UIButton _buttonLoadTwitts;

		public MainView ()
		{
			BackgroundColor = UIColor.White;
			_tableView = new UITableView ();
			AddSubview (_tableView);
			_tableSource = new TableSource ();
			_tableView.Source = _tableSource;
			_footerView = new UIView ();
			_footerView.ClipsToBounds = true;
			_buttonLoadTwitts  = UIButton.FromType (UIButtonType.System);
			InitTableViewFooter ();
		}


		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			_tableView.Frame = Bounds;
			_tableView.ContentInset = new UIEdgeInsets (0, 0, 80, 0);
			_tableView.ScrollIndicatorInsets = new UIEdgeInsets (0, 0, 80, 0);
			if (_webView != null)
				_webView.Frame = Bounds;

			_footerView.Frame = new System.Drawing.RectangleF (_footerView.Frame.Location,new SizeF(Bounds.Width,50));
			var buttonSize = _buttonLoadTwitts.Frame.Size;
			_buttonLoadTwitts.Frame = new RectangleF (new PointF ((_footerView.Bounds.Width - buttonSize.Width) / 2, 0), buttonSize);

		}

		public delegate List<Twitt> GetMoreTwitts ();
		GetMoreTwitts _getMoreTwitts;

		public void DisplayTwitts(List<Twitt> twitts, GetMoreTwitts getMoreTwitts)
		{
			_tableSource.SetSource (twitts);
			_tableView.ReloadData ();
			_getMoreTwitts = getMoreTwitts;

		}

		public delegate void ContinueAuthorization(string query);
		ContinueAuthorization _continueAthorization;

		public void DisplayAuthWebView(string authUrl, ContinueAuthorization continueAuthorization)
		{
			_continueAthorization = continueAuthorization;
			_webView = new UIWebView ();
			_webView.ShouldStartLoad += WebViewAuth_ShouldStartLoad;
			//_webView.Frame = UIScreen.MainScreen.Bounds;
			AddSubview (_webView);
			_webView.LoadRequest(new NSUrlRequest(new NSUrl(authUrl)));
		}

		bool WebViewAuth_ShouldStartLoad (UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType)
		{
			if (request.Url.Host == "www.yandex.ru") 
			{
				_continueAthorization (request.Url.Query);
			}
			return true;
		}

		public void FinishAuthorization()
		{
			_webView.RemoveFromSuperview ();
			_webView = null;
		}

		public void InitTableViewFooter()
		{
			_tableView.TableFooterView = _footerView;
			_footerView.Frame = new System.Drawing.RectangleF (0,0,320,50);


			_buttonLoadTwitts.SetTitle ("Показать еще", UIControlState.Normal);

			_buttonLoadTwitts.SizeToFit ();
			_buttonLoadTwitts.TouchUpInside += ButtonLoadTwitts_TouchUpInside;
			//buttonLoadTwitts.AddTarget (null, new Selector("sender:event:"), UIControlEvent.TouchUpInside);
			_footerView.AddSubview (_buttonLoadTwitts);
		}

		void ButtonLoadTwitts_TouchUpInside (object sender, EventArgs ea) 
		{
			var newTwittList = _getMoreTwitts ();
			_tableSource.AddNewTwittsToSource(newTwittList);
			List<NSIndexPath> tmpList = new List<NSIndexPath>(); 
			var rowsInSection = _tableView.NumberOfRowsInSection (0);
			for(int i = rowsInSection; i < rowsInSection  + newTwittList.Count; i++) 
			{ 
				NSIndexPath tmpIndexPath = NSIndexPath.FromRowSection(i,0); 
				tmpList.Add(tmpIndexPath);	
			} 
			//			NSIndexPath
			//var pathes = NSIndexPath.FromRowSection(_tableView. , 15);
			_tableView.InsertRows(tmpList.ToArray(), UITableViewRowAnimation.None);
		}

	}
}

