using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class GoogleMobileAd
{
	public static GoogleMobileAdInterface controller;

	private static bool _IsInited = false;

	private static bool _IsInterstitialReady = false;

	private static bool _IsRewardedVideoReady = false;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnInterstitialLoaded__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnInterstitialFailedLoading__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnInterstitialOpened__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnInterstitialClosed__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnInterstitialLeftApplication__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> OnAdInAppRequest__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string, int> OnRewarded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnRewardedVideoAdClosed__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<int> OnRewardedVideoAdFailedToLoad__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnRewardedVideoAdLeftApplication__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnRewardedVideoLoaded__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnRewardedVideoAdOpened__BackingField = () =>
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnRewardedVideoStarted__BackingField = () =>
	{
	};

	public static bool IsInited
	{
		get
		{
			return _IsInited;
		}
	}

	public static string BannersUunitId
	{
		get
		{
			return controller.BannersUunitId;
		}
	}

	public static string InterstisialUnitId
	{
		get
		{
			return controller.InterstisialUnitId;
		}
	}

	public static bool IsInterstitialReady
	{
		get
		{
			return _IsInterstitialReady;
		}
	}

	public static string RewardedVideoUnitId
	{
		get
		{
			return controller.RewardedVideoAdUnitId;
		}
	}

	public static bool IsRewardedVideoReady
	{
		get
		{
			return _IsRewardedVideoReady;
		}
	}

	public bool IsEditorTestingEnabled
	{
		get
		{
			return SA_EditorTesting.IsInsideEditor && GoogleMobileAdSettings.Instance.IsEditorTestingEnabled;
		}
	}

	public static event Action OnInterstitialLoaded
	{
		add
		{
			Action action = OnInterstitialLoaded__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialLoaded__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnInterstitialLoaded__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialLoaded__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnInterstitialFailedLoading
	{
		add
		{
			Action action = OnInterstitialFailedLoading__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialFailedLoading__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnInterstitialFailedLoading__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialFailedLoading__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnInterstitialOpened
	{
		add
		{
			Action action = OnInterstitialOpened__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialOpened__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnInterstitialOpened__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialOpened__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnInterstitialClosed
	{
		add
		{
			Action action = OnInterstitialClosed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialClosed__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnInterstitialClosed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialClosed__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnInterstitialLeftApplication
	{
		add
		{
			Action action = OnInterstitialLeftApplication__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialLeftApplication__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnInterstitialLeftApplication__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInterstitialLeftApplication__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string> OnAdInAppRequest
	{
		add
		{
			Action<string> action = OnAdInAppRequest__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAdInAppRequest__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = OnAdInAppRequest__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAdInAppRequest__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<string, int> OnRewarded
	{
		add
		{
			Action<string, int> action = OnRewarded__BackingField;
			Action<string, int> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewarded__BackingField, (Action<string, int>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string, int> action = OnRewarded__BackingField;
			Action<string, int> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewarded__BackingField, (Action<string, int>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnRewardedVideoAdClosed
	{
		add
		{
			Action action = OnRewardedVideoAdClosed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoAdClosed__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnRewardedVideoAdClosed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoAdClosed__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<int> OnRewardedVideoAdFailedToLoad
	{
		add
		{
			Action<int> action = OnRewardedVideoAdFailedToLoad__BackingField;
			Action<int> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoAdFailedToLoad__BackingField, (Action<int>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<int> action = OnRewardedVideoAdFailedToLoad__BackingField;
			Action<int> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoAdFailedToLoad__BackingField, (Action<int>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnRewardedVideoAdLeftApplication
	{
		add
		{
			Action action = OnRewardedVideoAdLeftApplication__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoAdLeftApplication__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnRewardedVideoAdLeftApplication__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoAdLeftApplication__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnRewardedVideoLoaded
	{
		add
		{
			Action action = OnRewardedVideoLoaded__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoLoaded__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnRewardedVideoLoaded__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoLoaded__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnRewardedVideoAdOpened
	{
		add
		{
			Action action = OnRewardedVideoAdOpened__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoAdOpened__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnRewardedVideoAdOpened__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoAdOpened__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action OnRewardedVideoStarted
	{
		add
		{
			Action action = OnRewardedVideoStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoStarted__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnRewardedVideoStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRewardedVideoStarted__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static void Init()
	{
		RuntimePlatform platform = Application.platform;
		if (platform == RuntimePlatform.IPhonePlayer)
		{
			controller = Singleton<IOSAdMobController>.Instance;
			controller.Init(GoogleMobileAdSettings.Instance.IOS_BannersUnitId);
			if (!GoogleMobileAdSettings.Instance.IOS_InterstisialsUnitId.Equals(string.Empty))
			{
				controller.SetInterstisialsUnitID(GoogleMobileAdSettings.Instance.IOS_InterstisialsUnitId);
			}
			if (!GoogleMobileAdSettings.Instance.IOS_RewardedVideoAdUnitId.Equals(string.Empty))
			{
				controller.SetRewardedVideoAdUnitID(GoogleMobileAdSettings.Instance.IOS_RewardedVideoAdUnitId);
			}
		}
		else
		{
			controller = Singleton<AndroidAdMobController>.Instance;
			controller.Init(GoogleMobileAdSettings.Instance.Android_BannersUnitId);
			if (!GoogleMobileAdSettings.Instance.Android_InterstisialsUnitId.Equals(string.Empty))
			{
				controller.SetInterstisialsUnitID(GoogleMobileAdSettings.Instance.Android_InterstisialsUnitId);
			}
			if (!GoogleMobileAdSettings.Instance.Android_RewardedVideoAdUnitId.Equals(string.Empty))
			{
				controller.SetRewardedVideoAdUnitID(GoogleMobileAdSettings.Instance.Android_RewardedVideoAdUnitId);
			}
		}
		controller.InitEditorTesting(GoogleMobileAdSettings.Instance.IsEditorTestingEnabled, GoogleMobileAdSettings.Instance.EditorFillRate);
		controller.OnInterstitialLoaded += OnInterstitialLoadedListner;
		controller.OnInterstitialFailedLoading += OnInterstitialFailedLoadingListner;
		controller.OnInterstitialOpened += OnInterstitialOpenedListner;
		controller.OnInterstitialClosed += OnInterstitialClosedListner;
		controller.OnInterstitialLeftApplication += OnInterstitialLeftApplicationListner;
		controller.OnAdInAppRequest += OnAdInAppRequestListner;
		controller.OnRewarded += OnRewardedListner;
		controller.OnRewardedVideoAdClosed += OnRewardedVideoAdClosedListner;
		controller.OnRewardedVideoAdFailedToLoad += OnRewardedVideoAdFailedToLoadListner;
		controller.OnRewardedVideoAdLeftApplication += OnRewardedVideoAdLeftApplicationListner;
		controller.OnRewardedVideoLoaded += OnRewardedVideoLoadedListner;
		controller.OnRewardedVideoAdOpened += OnRewardedVideoAdOpenedListner;
		controller.OnRewardedVideoStarted += OnRewardedVideoStartedListner;
		_IsInited = true;
		if (GoogleMobileAdSettings.Instance.testDevices.Count > 0)
		{
			List<string> list = new List<string>();
			foreach (GADTestDevice testDevice in GoogleMobileAdSettings.Instance.testDevices)
			{
				list.Add(testDevice.ID);
			}
			AddTestDevices(list.ToArray());
		}
		TagForChildDirectedTreatment(GoogleMobileAdSettings.Instance.TagForChildDirectedTreatment);
		foreach (string defaultKeyword in GoogleMobileAdSettings.Instance.DefaultKeywords)
		{
			AddKeyword(defaultKeyword);
		}
	}

	public static void SetBannersUnitID(string android_unit_id, string ios_unit_id, string wp8_unit_id)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("ChangeBannersUnitID shoudl be called only after Init function. Call ignored");
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			controller.SetBannersUnitID(ios_unit_id);
			break;
		case RuntimePlatform.Android:
			controller.SetBannersUnitID(android_unit_id);
			break;
		default:
			controller.SetBannersUnitID(wp8_unit_id);
			break;
		}
	}

	public static void SetInterstisialsUnitID(string android_unit_id, string ios_unit_id, string wp8_unit_id)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("ChangeInterstisialsUnitID shoudl be called only after Init function. Call ignored");
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			controller.SetInterstisialsUnitID(ios_unit_id);
			break;
		case RuntimePlatform.Android:
			controller.SetInterstisialsUnitID(android_unit_id);
			break;
		default:
			controller.SetInterstisialsUnitID(wp8_unit_id);
			break;
		}
	}

	public static GoogleMobileAdBanner CreateAdBanner(TextAnchor anchor, GADBannerSize size)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("CreateBannerAd shoudl be called only after Init function. Call ignored");
			return null;
		}
		return controller.CreateAdBanner(anchor, size);
	}

	public static GoogleMobileAdBanner CreateAdBanner(int x, int y, GADBannerSize size)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("CreateBannerAd shoudl be called only after Init function. Call ignored");
			return null;
		}
		return controller.CreateAdBanner(x, y, size);
	}

	public static GoogleMobileAdBanner GetBanner(int id)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("GetBanner shoudl be called only after Init function. Call ignored");
			return null;
		}
		return controller.GetBanner(id);
	}

	public static void DestroyBanner(int id)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("DestroyCurrentBanner shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.DestroyBanner(id);
		}
	}

	public static void AddKeyword(string keyword)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("AddKeyword shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.AddKeyword(keyword);
		}
	}

	public static void SetBirthday(int year, AndroidMonth month, int day)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("SetBirthday shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.SetBirthday(year, month, day);
		}
	}

	public static void TagForChildDirectedTreatment(bool tagForChildDirectedTreatment)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("TagForChildDirectedTreatment shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.TagForChildDirectedTreatment(tagForChildDirectedTreatment);
		}
	}

	public static void AddTestDevice(string deviceId)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("AddTestDevice shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.AddTestDevice(deviceId);
		}
	}

	public static void AddTestDevices(params string[] ids)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("AddTestDevice shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.AddTestDevices(ids);
		}
	}

	public static void SetGender(GoogleGender gender)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("SetGender shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.SetGender(gender);
		}
	}

	public static void StartInterstitialAd()
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("StartInterstitialAd shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.StartInterstitialAd();
		}
	}

	public static void LoadInterstitialAd()
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("LoadInterstitialAd shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.LoadInterstitialAd();
		}
	}

	public static void ShowInterstitialAd()
	{
		if (_IsInterstitialReady)
		{
			_IsInterstitialReady = false;
			if (!_IsInited)
			{
				UnityEngine.Debug.LogWarning("ShowInterstitialAd shoudl be called only after Init function. Call ignored");
			}
			else
			{
				controller.ShowInterstitialAd();
			}
		}
		else
		{
			UnityEngine.Debug.LogWarning("ShowInterstitialAd shoudl be called only what  Interstitial Ad is Ready ");
		}
	}

	public static void RecordInAppResolution(GADInAppResolution resolution)
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("RecordInAppResolution shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.RecordInAppResolution(resolution);
		}
	}

	public static void StartRewardedVideo()
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("StartRewardedVideo shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.StartRewardedVideo();
		}
	}

	public static void LoadRewardedVideo()
	{
		if (!_IsInited)
		{
			UnityEngine.Debug.LogWarning("LoadRewardedVideo shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.LoadRewardedVideo();
		}
	}

	public static void ShowRewardedVideo()
	{
		if (_IsRewardedVideoReady)
		{
			_IsRewardedVideoReady = false;
			if (!_IsInited)
			{
				UnityEngine.Debug.LogWarning("ShowRewardedVideo shoudl be called only after Init function. Call ignored");
			}
			else
			{
				controller.ShowRewardedVideo();
			}
		}
		else
		{
			UnityEngine.Debug.LogWarning("ShowRewardedVideo shoudl be called only what  Interstitial Ad is Ready ");
		}
	}

	private static void OnInterstitialLoadedListner()
	{
		_IsInterstitialReady = true;
		OnInterstitialLoaded__BackingField();
	}

	private static void OnInterstitialFailedLoadingListner()
	{
		_IsInterstitialReady = false;
		OnInterstitialFailedLoading__BackingField();
	}

	private static void OnInterstitialOpenedListner()
	{
		_IsInterstitialReady = false;
		OnInterstitialOpened__BackingField();
	}

	private static void OnInterstitialClosedListner()
	{
		_IsInterstitialReady = false;
		OnInterstitialClosed__BackingField();
	}

	private static void OnInterstitialLeftApplicationListner()
	{
		OnInterstitialLeftApplication__BackingField();
	}

	private static void OnAdInAppRequestListner(string itemId)
	{
		OnAdInAppRequest__BackingField(itemId);
	}

	private static void OnRewardedVideoLoadedListner()
	{
		_IsRewardedVideoReady = true;
		OnRewardedVideoLoaded__BackingField();
	}

	private static void OnRewardedVideoAdFailedToLoadListner(int errorCode)
	{
		_IsRewardedVideoReady = false;
		OnRewardedVideoAdFailedToLoad__BackingField(errorCode);
	}

	private static void OnRewardedVideoStartedListner()
	{
		_IsRewardedVideoReady = false;
		OnRewardedVideoStarted__BackingField();
	}

	private static void OnRewardedVideoAdOpenedListner()
	{
		_IsRewardedVideoReady = false;
		OnRewardedVideoAdOpened__BackingField();
	}

	private static void OnRewardedVideoAdClosedListner()
	{
		_IsRewardedVideoReady = false;
		OnRewardedVideoAdClosed__BackingField();
	}

	private static void OnRewardedVideoAdLeftApplicationListner()
	{
		_IsRewardedVideoReady = false;
		OnRewardedVideoAdLeftApplication__BackingField();
	}

	private static void OnRewardedListner(string itemId, int count)
	{
		_IsRewardedVideoReady = false;
		OnRewarded__BackingField(itemId, count);
	}
}
