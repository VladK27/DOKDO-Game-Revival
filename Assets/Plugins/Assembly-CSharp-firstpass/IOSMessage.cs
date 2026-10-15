using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class IOSMessage : BaseIOSPopup
{
	public string ok;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnComplete__BackingField = () =>
	{
	};

	public event Action OnComplete
	{
		add
		{
			Action action = OnComplete__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnComplete__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnComplete__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnComplete__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static IOSMessage Create(string title, string message)
	{
		return Create(title, message, "Ok");
	}

	public static IOSMessage Create(string title, string message, string ok)
	{
		IOSMessage iOSMessage = new GameObject("IOSPopUp").AddComponent<IOSMessage>();
		iOSMessage.title = title;
		iOSMessage.message = message;
		iOSMessage.ok = ok;
		iOSMessage.init();
		return iOSMessage;
	}

	public void init()
	{
		IOSNativePopUpManager.showMessage(title, message, ok);
	}

	public void onPopUpCallBack(string buttonIndex)
	{
		OnComplete__BackingField();
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
