using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;
using System.Drawing;
using BigTed;

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
		ContinueAuthorizationDelegate _continueAthorizationDelegate;
		ViewTwittDelegate _viewTwittDelegate;


		public delegate List<Twitt> GetMoreTwittsDelegate ();
		public delegate void ContinueAuthorizationDelegate(string query);
		public delegate void ViewTwittDelegate(Twitt twitt);


		public TwittsGetByTagView ()
		{
			BackgroundColor = UIColor.White;
			_tableView = new UITableView ();
			AddSubview (_tableView);
			_tableSource = new TwittsTableSource (TableSource_RowSelected);
			_tableView.Source = _tableSource;
			_footerView = new UIView ();
			_footerView.ClipsToBounds = true;
			_buttonLoadTwitts  = UIButton.FromType (UIButtonType.System);
			InitTableViewFooter ();


		}



		bool WebViewAuth_ShouldStartLoad (UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType)
		{
			if (request.Url.Host == "www.yandex.ru") 
			{
				_continueAthorizationDelegate (request.Url.Query);
			}
			return true;
		}

		void ButtonLoadTwitts_TouchUpInside (object sender, EventArgs ea) 
		{
			var newTwittList = _getMoreTwittsDelegate ();
			_tableSource.AddNewTwittsToSource(newTwittList);
			List<NSIndexPath> tmpList = new List<NSIndexPath>(); 
			var rowsInSection = _tableView.NumberOfRowsInSection (0);
			for(int i = rowsInSection; i < rowsInSection  + newTwittList.Count; i++) 
			{ 
				NSIndexPath tmpIndexPath = NSIndexPath.FromRowSection(i,0); 
				tmpList.Add(tmpIndexPath);	
			} 

			_tableView.InsertRows(tmpList.ToArray(), UITableViewRowAnimation.None);
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
			_tableView.ContentInset = new UIEdgeInsets (65, 0, 80, 0);
			_tableView.ScrollIndicatorInsets = new UIEdgeInsets (65, 0, 80, 0);

			if (_webView != null)
			{
				_webView.Frame = new RectangleF(0,65,Bounds.Size.Width,Bounds.Size.Height-145);

			}
			_footerView.Frame = new System.Drawing.RectangleF (_footerView.Frame.Location,new SizeF(Bounds.Width,50));
			var buttonSize = _buttonLoadTwitts.Frame.Size;
			_buttonLoadTwitts.Frame = new RectangleF (new PointF ((_footerView.Bounds.Width - buttonSize.Width) / 2, 0), buttonSize);

		}

		public void DisplayTwitts(List<Twitt> twitts, GetMoreTwittsDelegate getMoreTwitts, ViewTwittDelegate viewTwittDelegate)
		{
			_tableSource.SetSource (twitts);
			_tableView.ReloadData ();
			_getMoreTwittsDelegate = getMoreTwitts;
			_viewTwittDelegate = viewTwittDelegate;

		}

		public void DisplayAuthWebView(string authUrl, ContinueAuthorizationDelegate continueAuthorization)
		{
			_continueAthorizationDelegate = continueAuthorization;
			_webView = new UIWebView ();
			_webView.ShouldStartLoad += WebViewAuth_ShouldStartLoad;
			//_webView.Frame = UIScreen.MainScreen.Bounds;
			AddSubview (_webView);
			_webView.LoadRequest(new NSUrlRequest(new NSUrl(authUrl)));
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

		public void ScrollToTop()
		{
			_tableView.ContentOffset = new PointF (0, -65);
		}

		public void ShowBTProgressHUD()
		{
			BTProgressHUD.Show(); //shows the spinner
			BTProgressHUD.Show("Загрузка данных"); //show spinner + text
		}

		public void HideBTProgressHUD()
		{
			BTProgressHUD.Dismiss();
		}
	}
}

