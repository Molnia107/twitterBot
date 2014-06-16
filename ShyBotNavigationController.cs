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
		StartController _startController;

		public ShyBotNavigationController ()
		{
			_startController = new StartController ();
			_tabBarController = new TwittTabController ();
			_infoController = new InfoController ();
			_twittController = new TwittController ();

			PushViewController (_startController, true);

			var backButton = new UIBarButtonItem (Strings.InfoTabName, UIBarButtonItemStyle.Plain, 
				(sender, args) => {
					InfoNavigationBar_ButtonClick ();
				});
			_tabBarController.NavigationItem.SetRightBarButtonItem(backButton,true);
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

		public void StartController_Login()
		{
			PushViewController (_tabBarController,true);

		}
	}
}

