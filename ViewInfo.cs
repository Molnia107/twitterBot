using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	static public class ViewInfo
	{
		static ViewInfo ()
		{
		}

		static public int TabBarHeight
		{
			get 
			{
				if (UIDevice.CurrentDevice.SystemVersion.Split ('.') [0] == "6") {
					return 0;
				}
				return 50;
			}
		}

		static public int NavigationBarHeight
		{	
			get 
			{
				if (UIDevice.CurrentDevice.SystemVersion.Split ('.') [0] == "6") {
					return 0;
				}
				return 65;
			}
		}

		static public int StatusBarHeight
		{	
			get 
			{
				if (UIDevice.CurrentDevice.SystemVersion.Split ('.') [0] == "6") {
					return 20;
				}
				return 20;
			}
		}
	}
}

