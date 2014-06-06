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

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			UITabBarController tabBarController = new UITabBarController ();
			tabBarController.AddChildViewController (new MyController ("Twitter"));
			tabBarController.AddChildViewController (new MyController ("Dribbble"));
			tabBarController.AddChildViewController (new MyController ("Apple"));
			tabBarController.AddChildViewController (new MyController ("GitHub"));

			_rootControler = tabBarController;
			window.RootViewController = _rootControler;

			window.MakeKeyAndVisible ();


			
			return true;
		}
	}
}

