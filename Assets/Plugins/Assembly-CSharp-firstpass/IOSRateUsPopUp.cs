using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class IOSRateUsPopUp : BaseIOSPopup
{
	public string rate;

	public string remind;

	public string declined;

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

	public static IOSRateUsPopUp Create()
	{
		return Create("Like the Game?", "Rate Us");
	}

	public static IOSRateUsPopUp Create(string title, string message)
	{
		return Create(title, message, "Rate Now", "Ask me later", "No, thanks");
	}

	public static IOSRateUsPopUp Create(string title, string message, string rate, string remind, string declined)
	{
		IOSRateUsPopUp iOSRateUsPopUp = new GameObject("IOSRateUsPopUp").AddComponent<IOSRateUsPopUp>();
		iOSRateUsPopUp.title = title;
		iOSRateUsPopUp.message = message;
		iOSRateUsPopUp.rate = rate;
		iOSRateUsPopUp.remind = remind;
		iOSRateUsPopUp.declined = declined;
		iOSRateUsPopUp.init();
		return iOSRateUsPopUp;
	}

	public void init()
	{
		IOSNativePopUpManager.showRateUsPopUp(title, message, rate, remind, declined);
	}

	public void onPopUpCallBack(string buttonIndex)
	{
		switch ((int)Convert.ToInt16(buttonIndex))
		{
		case 0:
			IOSNativeUtility.RedirectToAppStoreRatingPage();
			OnComplete__BackingField(IOSDialogResult.RATED);
			break;
		case 1:
			OnComplete__BackingField(IOSDialogResult.REMIND);
			break;
		case 2:
			OnComplete__BackingField(IOSDialogResult.DECLINED);
			break;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
