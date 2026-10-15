using System;
using UnityEngine;

public class AndroidDialog : BaseAndroidPopup
{
	public string yes;

	public string no;

	public static AndroidDialog Create(string title, string message)
	{
		return Create(title, message, "Yes", "No");
	}

	public static AndroidDialog Create(string title, string message, AndroidDialogTheme theme)
	{
		return Create(title, message, "Yes", "No", theme);
	}

	public static AndroidDialog Create(string title, string message, string yes, string no)
	{
		AndroidDialog androidDialog = new GameObject("AndroidPopUp").AddComponent<AndroidDialog>();
		androidDialog.title = title;
		androidDialog.message = message;
		androidDialog.yes = yes;
		androidDialog.no = no;
		androidDialog.init();
		return androidDialog;
	}

	public static AndroidDialog Create(string title, string message, string yes, string no, AndroidDialogTheme theme)
	{
		AndroidDialog androidDialog = new GameObject("AndroidPopUp").AddComponent<AndroidDialog>();
		androidDialog.title = title;
		androidDialog.message = message;
		androidDialog.yes = yes;
		androidDialog.no = no;
		androidDialog.init(theme);
		return androidDialog;
	}

	public void init()
	{
		AN_PoupsProxy.showDialog(title, message, yes, no, AndroidNativeSettings.Instance.DialogTheme);
	}

	public void init(AndroidDialogTheme theme)
	{
		AN_PoupsProxy.showDialog(title, message, yes, no, theme);
	}

	public void onPopUpCallBack(string buttonIndex)
	{
		switch ((int)Convert.ToInt16(buttonIndex))
		{
		case 0:
			DispatchAction(AndroidDialogResult.YES);
			break;
		case 1:
			DispatchAction(AndroidDialogResult.NO);
			break;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
