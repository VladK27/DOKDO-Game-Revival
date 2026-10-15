using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class UM_Location : Singleton<UM_Location>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<UM_LocaleInfo> OnLocaleLoaded__BackingField = delegate
	{
	};

	public static event Action<UM_LocaleInfo> OnLocaleLoaded
	{
		add
		{
			Action<UM_LocaleInfo> action = OnLocaleLoaded__BackingField;
			Action<UM_LocaleInfo> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLocaleLoaded__BackingField, (Action<UM_LocaleInfo>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_LocaleInfo> action = OnLocaleLoaded__BackingField;
			Action<UM_LocaleInfo> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLocaleLoaded__BackingField, (Action<UM_LocaleInfo>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public void GetLocale()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			IOSNativeUtility.OnLocaleLoaded += HandleOnLocaleLoaded_IOS;
			Singleton<IOSNativeUtility>.Instance.GetLocale();
			break;
		case RuntimePlatform.Android:
			AndroidNativeUtility.LocaleInfoLoaded += HandleLocaleInfoLoaded_Android;
			Singleton<AndroidNativeUtility>.Instance.LoadLocaleInfo();
			break;
		}
	}

	private void HandleLocaleInfoLoaded_Android(AN_Locale locale)
	{
		AndroidNativeUtility.LocaleInfoLoaded -= HandleLocaleInfoLoaded_Android;
		OnLocaleLoaded__BackingField(new UM_LocaleInfo(locale));
	}

	private void HandleOnLocaleLoaded_IOS(ISN_Locale locale)
	{
		IOSNativeUtility.OnLocaleLoaded -= HandleOnLocaleLoaded_IOS;
		OnLocaleLoaded__BackingField(new UM_LocaleInfo(locale));
	}
}
