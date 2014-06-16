using System;
using BigTed;

namespace TwitterBot
{
	static public class BTProgressHUDProvider
	{
		static public void ShowBTProgressHUD()
		{
			BTProgressHUD.Show(); //shows the spinner
			BTProgressHUD.Show(Strings.DataInLoading); //show spinner + text
		}

		static public void HideBTProgressHUD()
		{
			BTProgressHUD.Dismiss();
		}
	}
}

