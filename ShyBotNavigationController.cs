using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class ShyBotNavigationController : UINavigationController
	{
		UITabBarController _tabBarController;
		InfoController _infoController;
		TwittController _twittController;

		public ShyBotNavigationController ()
		{
			_tabBarController = new TwittTabController ();
			PushViewController (_tabBarController,true);


			var btn = new UIBarButtonItem ("Инфо", UIBarButtonItemStyle.Plain, 
				(sender, args) => {
					InfoNavigationBar_ButtonClick ();
				});


			_tabBarController.NavigationItem.SetRightBarButtonItem(btn,true);


			//_navigationController.NavigationBar.Items [0].SetRightBarButtonItem (btn, true);

			_infoController = new InfoController ();
			_twittController = new TwittController ();
		}

		void InfoNavigationBar_ButtonClick ()
		{
			PushViewController (_infoController, true);
		}


		public void TwittTableSource_SelectedRow(Twitt twitt)
		{
			_twittController.RefreshTwitt (twitt);
			PushViewController (_twittController, true);
		}
	}
}

