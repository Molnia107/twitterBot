using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace TwitterBot
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
		UIViewController _rootControler;
		UINavigationController _navigationController;
		InfoController _infoController;
		TwittController _twittController;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);


			_navigationController = new ShyBotNavigationController ();



			//_navigationController.NavigationBar.Items [0].SetRightBarButtonItem (btn, true);

			_rootControler = _navigationController;
			window.RootViewController = _rootControler;


			window.MakeKeyAndVisible ();


			
			return true;
		}



	}
}

