using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class IOSNativeUtility : Singleton<IOSNativeUtility>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<ISN_Locale> OnLocaleLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<bool> GuidedAccessSessionRequestResult__BackingField = delegate
	{
	};

	public bool IsGuidedAccessEnabled
	{
		get
		{
			return false;
		}
	}

	public static bool IsRunningTestFlightBeta
	{
		get
		{
			return true;
		}
	}

	public static event Action<ISN_Locale> OnLocaleLoaded
	{
		add
		{
			Action<ISN_Locale> action = OnLocaleLoaded__BackingField;
			Action<ISN_Locale> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLocaleLoaded__BackingField, (Action<ISN_Locale>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<ISN_Locale> action = OnLocaleLoaded__BackingField;
			Action<ISN_Locale> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnLocaleLoaded__BackingField, (Action<ISN_Locale>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<bool> GuidedAccessSessionRequestResult
	{
		add
		{
			Action<bool> action = GuidedAccessSessionRequestResult__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref GuidedAccessSessionRequestResult__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = GuidedAccessSessionRequestResult__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref GuidedAccessSessionRequestResult__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void GetLocale()
	{
	}

	public static void RedirectToAppStoreRatingPage()
	{
		RedirectToAppStoreRatingPage(IOSNativeSettings.Instance.AppleId);
	}

	public static void RedirectToAppStoreRatingPage(string appleId)
	{
	}

	public static void SetApplicationBagesNumber(int count)
	{
	}

	public static void ShowPreloader()
	{
	}

	public static void HidePreloader()
	{
	}

	public void RequestGuidedAccessSession(bool enabled)
	{
	}

	private void OnGuidedAccessSessionRequestResult(string data)
	{
		bool obj = Convert.ToBoolean(data);
		GuidedAccessSessionRequestResult__BackingField(obj);
	}

	private void OnLocaleLoadedHandler(string data)
	{
		string[] array = data.Split('|');
		string countryCode = array[0];
		string contryName = array[1];
		string languageCode = array[2];
		string languageName = array[3];
		ISN_Locale obj = new ISN_Locale(countryCode, contryName, languageCode, languageName);
		OnLocaleLoaded__BackingField(obj);
	}
}
