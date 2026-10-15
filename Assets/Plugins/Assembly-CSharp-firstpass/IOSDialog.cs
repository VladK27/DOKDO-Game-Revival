using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class IOSDialog : BaseIOSPopup
{
	public string yes;

	public string no;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<IOSDialogResult> OnComplete__BackingField = delegate
	{
	};

	public event Action<IOSDialogResult> OnComplete
	{
		add
		{
			Action<IOSDialogResult> action = OnComplete__BackingField;
			Action<IOSDialogResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnComplete__BackingField, (Action<IOSDialogResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<IOSDialogResult> action = OnComplete__BackingField;
			Action<IOSDialogResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnComplete__BackingField, (Action<IOSDialogResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static IOSDialog Create(string title, string message)
	{
		return Create(title, message, "Yes", "No");
	}

	public static IOSDialog Create(string title, string message, string yes, string no)
	{
		IOSDialog iOSDialog = new GameObject("IOSPopUp").AddComponent<IOSDialog>();
		iOSDialog.title = title;
		iOSDialog.message = message;
		iOSDialog.yes = yes;
		iOSDialog.no = no;
		iOSDialog.init();
		return iOSDialog;
	}

	public void init()
	{
		IOSNativePopUpManager.showDialog(title, message, yes, no);
	}

	public void onPopUpCallBack(string buttonIndex)
	{
		switch ((int)Convert.ToInt16(buttonIndex))
		{
		case 0:
			OnComplete__BackingField(IOSDialogResult.YES);
			break;
		case 1:
			OnComplete__BackingField(IOSDialogResult.NO);
			break;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
